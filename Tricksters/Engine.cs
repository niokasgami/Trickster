using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGameGum;
using Tricksters.Display;
using Tricksters.Management;
using Tricksters.Screens;

namespace Tricksters;

public class Engine : Game
{
  public GraphicsDeviceManager gdManager;
  private SpriteBatch _spriteBatch;
  private readonly ScreenManager _screenManager;
  private  GumService Gum => GumService.Default;
  
  public Engine()
  {
    gdManager = new GraphicsDeviceManager(this);
    this.Content.RootDirectory = "Content";
    _screenManager = new ScreenManager();
    Components.Add(_screenManager);
    this.IsMouseVisible = true;
  }

  protected override void Initialize()
  {
    // TODO: Add your initialization logic here
    Graphics.Init(this, 1920, 1080, false);
    Gum.Initialize(this, "GumProjects/TricktersUi.gumx");
    SceneManager.Init(_screenManager);
    // TODO : improve this call ?
    SceneManager.Goto(new SplashScreen(this), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
    base.Initialize();
  }

  protected override void LoadContent()
  {
    _spriteBatch = new SpriteBatch(GraphicsDevice);
    // TODO: use this.Content to load your game content here
  }

  protected override void Update(GameTime gameTime)
  {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();
    // TODO: Add your update logic here
    Gum.Update(gameTime);
    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    base.Draw(gameTime);
  }
}
