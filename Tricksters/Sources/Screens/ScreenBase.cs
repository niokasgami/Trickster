using System.Diagnostics;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGameGum;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;


namespace Tricksters.Screens;

/// <summary>
/// The super class that handle all the common method and properties for all the screens.
/// </summary>
public abstract class ScreenBase(Game game) : Screen
{
  
  public Game Game { get; } = game;

  public ContentManager Content => Game.Content;
  public GraphicsDevice GraphicsDevice => Game.GraphicsDevice;
  public GameServiceContainer Services => Game.Services;
  protected GumService GumUi => GumService.Default;

  protected GumProjectSave gumProject;

  public override void Initialize()
  {
    gumProject = ObjectFinder.Self.GumProjectSave;
    base.Initialize();
  }

  public override void Draw(GameTime gameTime)
  {
    GraphicsDevice.Clear(Color.CornflowerBlue);
    GumUi.Draw();
  }

  /// <summary>
  /// go to the next screen
  /// </summary>
  /// <param name="screen"></param>
  /// <param name="transition"></param>
  protected void Goto(Screen screen,Transition transition = null)
  {
    var screenManager = Game.Services.GetService<ScreenManager>();
    screenManager.LoadScreen(screen, transition);
  }

  /// <summary>
  /// fetch the screen
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  protected ScreenSave GetScreen(string name)
  {
    return gumProject.Screens.Find((screen) => screen.Name == name);
  }

  /// <summary>
  /// Get an element and return it.
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="name"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  protected T GetElement<T>(GraphicalUiElement obj,string name) where T: FrameworkElement
  {
    return obj.GetFrameworkElementByName<T>(name);
  }
}
