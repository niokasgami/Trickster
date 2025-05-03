using System.Diagnostics;
using System.Linq;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGameGum;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;

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
    var optionsButton = GetElement<Button>(ui, "OptionsButton");
    optionsButton.Click += (_, _) => OnCommandOptions();
    var quitButton = GetElement<Button>(ui, "QuitButton");
    quitButton.Click += (_, _) => OnCommandQuit();
  }
  
  private void OnCommandNewGame()
  {
    Debug.Print("new game");
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
  }
}
