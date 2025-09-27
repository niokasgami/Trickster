using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGameGum;

namespace Tricksters.Screens;

public class MainScreen(Game game) : ScreenBase(game)
{

  private GraphicalUiElement _ui;
  public override void Initialize()
  {
    base.Initialize();
    _ui = GetScreen("Mainscreen").ToGraphicalUiElement();
    _ui.AddToRoot();
  }
}
