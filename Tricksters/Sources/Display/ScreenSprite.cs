using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using SharpDX.Direct3D9;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;

namespace Tricksters.Display;

public enum FadeState
{
  None,
  FadeIn,
  FadeOut,
}

// inspired by Monogame.Extended
/// <summary>
/// The class that handle the screen fade effect.
/// It is the same implementation as the Transition class in Monogame.Extended.
/// but it was changed to be toggling the fading and fadeout by the user.
/// all credit goes to the original author.
/// </summary>
public class ScreenSprite : IDisposable
{

  public FadeState FadeState { get; set; } = FadeState.None;
  
  public float Value => MathHelper.Clamp(_currentSeconds / _halfDuration, 0f, 1f);

  public event EventHandler Completed;

  private float Duration { get; set; }

  private Color Color { get; set; }

  private readonly float _halfDuration;
  private float _currentSeconds;
  private GraphicsDevice _graphicsDevice;
  private SpriteBatch _spriteBatch;

  public ScreenSprite(GraphicsDevice graphicsDevice, float defaultDuration = 1f)
  {
    this._graphicsDevice = graphicsDevice;
    _spriteBatch = new SpriteBatch(graphicsDevice);
    Duration = defaultDuration;
    _halfDuration = Duration / 2f;
    _currentSeconds = 0;
    Color = Color.Black;
  }

  public void FadeIn(float duration, Color color)
  {
    Duration = duration;
    FadeState = FadeState.FadeIn;
    Color = color;
  }

  public void FadeOut(float duration, Color color)
  {

    Duration = duration;
    FadeState = FadeState.FadeOut;
    Debug.Print(FadeState.ToString());
    Color = color;
  }

  public bool IsFading() => FadeState != FadeState.None;

  public void Update(GameTime gameTime)
  {
    if (!IsFading()) return;
    var elapsedSeconds = gameTime.GetElapsedSeconds();
    if (FadeState == FadeState.FadeIn) ProcessFadeIn(elapsedSeconds);
    if (FadeState == FadeState.FadeOut) ProcessFadeOut(elapsedSeconds);
  }

  private void ProcessFadeIn(float elapsedSeconds)
  {
    _currentSeconds -= elapsedSeconds;
    if ((double)_currentSeconds > 0.0)  return;
    var completed = Completed;
    completed?.Invoke(this, EventArgs.Empty);
    FadeState = FadeState.None;
  }

  private void ProcessFadeOut(float elapsedSeconds)
  {
    _currentSeconds += elapsedSeconds;
    if ((double)_currentSeconds < _halfDuration) return; 
    var completed = Completed;
    completed?.Invoke(this, EventArgs.Empty);
    FadeState = FadeState.None;
  }


  public void Draw(GameTime gameTime)
  {
    _spriteBatch.Begin(samplerState:SamplerState.PointClamp);
    var viewport = _graphicsDevice.Viewport;
    var width = (double)viewport.Width;
    viewport = _graphicsDevice.Viewport;
    var height = (double)viewport.Height;
    var color = Color * Value;
    _spriteBatch.FillRectangle(0.0f,0.0f,(float)width,(float)height,color);
    _spriteBatch.End();
  }

  public void Dispose()
  {
    _spriteBatch.Dispose();
  }

  protected float FadeSpeed()
  {
    return 3f;
  }

  protected float SlowFadeSpeed()
  {
    return 6f;
  }
}
