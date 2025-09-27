using System.Diagnostics;
using System.Linq;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens.Transitions;
using MonoGameGum;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using Tricksters.Management;
using Tricksters.Screens;

namespace Tricksters.Screens;

public class TitleScreen(Game game) : ScreenBase(game)
{

  private GraphicalUiElement ui;
  
  public override void Initialize()
  { 
    base.Initialize();
    ui = GetScreen("Titlescreen").ToGraphicalUiElement();
    ui.AddToRoot();

    InitializeButton();
  }

  private void InitializeButton()
  {
    var startButton = GetElement<Button>(ui, "StartButton");
    startButton.Click += (_, _) => OnCommandNewGame();
    var optionsButton = GetElement<Button>(ui, "OptionButton");
    optionsButton.Click += (_, _) => OnCommandOptions();
    var quitButton = GetElement<Button>(ui, "ExitButton");
    quitButton.Click += (_, _) => OnCommandQuit();
  }
  
  private void OnCommandNewGame()
  {
    SceneManager.Goto(new MainScreen(game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
  }

  private void OnCommandOptions()
  {
    Debug.Print("options");
  }

  private void OnCommandQuit()
  {
    this.Game.Exit();
  }
  
  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);
    KeyboardState keyboardState = Keyboard.GetState();
    
    if (keyboardState.IsKeyDown(Keys.Space))
    {
      FadeIn(2f);
    }

  }
}
