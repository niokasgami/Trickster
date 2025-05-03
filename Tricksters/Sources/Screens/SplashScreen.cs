using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens.Transitions;
using MonoGameGum;
using Tricksters.Management;

namespace Tricksters.Screens;

public class SplashScreen(Game game): ScreenBase(game) {

  private GraphicalUiElement ui;
  private float _elapsedTime = 0.0f;
  private const float WaitTime = 2.0f;


  override public void Initialize()
  {
    base.Initialize();
    ui = GetScreen("Splashscreen").ToGraphicalUiElement();
    ui.AddToRoot();
  }

  
  public override void Update(GameTime gameTime)
  {
    
    _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
    if (_elapsedTime > WaitTime)
    {
      SceneManager.Goto(new TitleScreen(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
    }
  }

  public override void Dispose()
  {
    base.Dispose();
    ui.RemoveFromRoot();
  }
}
