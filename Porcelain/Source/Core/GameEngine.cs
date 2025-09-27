using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGameGum;

namespace Porcelain.Core;

/// <summary>
/// The entry point class for the game engine, similar to the traditional Game1 class in MonoGame projects.
/// While this class extends MonoGame's Game class, it provides additional engine-specific functionality
/// and can be used as a starting point for your game implementation.
/// </summary>
public class GameEngine : Game
{
  public GraphicsDeviceManager GdManager { get; }

  private readonly ScreenManager screenManager;
  
  private GumService GumService => GumService.Default;
  private readonly Screen startingScreen;
  
  /// <summary>
  /// Initialize the engine
  /// </summary>
  /// <param name="startingScreen"> the starting screen that the game should run.</param>
  public GameEngine(Screen startingScreen)
  {
    GdManager = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    this.startingScreen = startingScreen;
    screenManager = new ScreenManager();
    Components.Add(screenManager);
    this.IsMouseVisible = true;
  }

  protected override void Initialize()
  {
    // TODO : lets make sure we can read an Setting file.
    var windowSettings = new GraphicsSystemOptions()
    {
      FullScreen = false,
      Width = 1920,
      Height = 1080,
      WindowTitle = "Porcelain"
    };
    // TODO : it should be handled by setting files.
    var gumProject = "";
    Graphics.Init(this, windowSettings);
    // TODO : what if they dont want to use Gum? In this case it should be  manageable via a editor setting
    GumService.Initialize(this, gumProject);
    //SceneManager.Init(screenManager);
    //SceneManager.Goto(startingScreen);
    base.Initialize();
  }

  protected override void Update(GameTime gameTime)
  {
    GumService.Update(gameTime); // @TODO : dont update if we dont use it!
    base.Update(gameTime);
  }
}
