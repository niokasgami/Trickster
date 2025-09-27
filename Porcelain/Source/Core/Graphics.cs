using Microsoft.Xna.Framework;
using Porcelain.Math;

namespace Porcelain.Core;

/// <summary>
/// the static class that manage the screen size and full screen toggling. 
/// </summary>
public static class Graphics
{
  private static GraphicsDeviceManager _graphics;
  private static Area2D _screenSize;
  private static bool _fullScreen;
  
  /// <summary>
  /// Init the Graphics
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="width"></param>
  /// <param name="height"></param>
  /// <param name="fullScreen"></param>
  public static void Init (GameEngine instance, int width = 1920, int height = 1080, bool fullScreen = false){
    _screenSize = new Area2D(width, height);
    _fullScreen = fullScreen;
    _graphics = instance.GdManager;
    OnScreenRefresh();
  }

  /// <summary>
  /// Init the graphics
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="options"></param>
  public static void Init(GameEngine instance, GraphicsSystemOptions options)
  {
    _screenSize = new Area2D(options.Width, options.Height);
    _fullScreen = options.FullScreen;
    _graphics = instance.GdManager;
    OnScreenRefresh();
  }

  /// <summary>
  /// Toggle the game application full screen mode on and off
  /// </summary>
  public static void ToggleFullScreen()
  {
    _fullScreen = !_fullScreen;
    OnScreenRefresh();
  }
  
  private static void OnScreenRefresh()
  {
    _graphics.IsFullScreen = _fullScreen;
    _graphics.PreferredBackBufferWidth = _screenSize.Width;
    _graphics.PreferredBackBufferHeight = _screenSize.Height;
    _graphics.ApplyChanges();
  }
  
  public static GraphicsDeviceManager GdManager => _graphics;
  
  /// <summary>
  /// Return the game screen size
  /// </summary>
  public static Area2D ScreenSize
  {
    get => _screenSize;
    set
    {
      _screenSize = value;
      OnScreenRefresh();
    }
  }

  /// <summary>
  /// Return the actual screen size. (or the backBufferSize)
  /// </summary>
  public static Area2D RealScreenSize
  {
    get
    {
      var width = _graphics.PreferredBackBufferWidth;
      var height = _graphics.PreferredBackBufferHeight;
      return new Area2D(width, height);
    }
  }
  
  public static int ScreenWidth {
    get => _screenSize.Width;
    set
    {
      _screenSize.Width = value; 
      OnScreenRefresh();
    }
  }

  public static int ScreenHeight
  {
    get => _screenSize.Height;
    set
    {
      _screenSize.Height = value;
      OnScreenRefresh();
    }
  }
}

public struct GraphicsSystemOptions
{
  public bool FullScreen;
  public int Width;
  public int Height;
  public string WindowTitle;
}
