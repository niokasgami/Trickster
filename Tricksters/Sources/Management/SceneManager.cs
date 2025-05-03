using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace Tricksters.Management;

public static class SceneManager
{
  private static ScreenManager _screenManager;
  public static void Init(ScreenManager screenManager)
  {
    _screenManager = screenManager;
  }
  public static void Goto(Screen screen, Transition transition = null)
  {
    _screenManager.LoadScreen(screen, transition);
  }

  public static void Dispose()
  {
    _screenManager = null;
  }
}
