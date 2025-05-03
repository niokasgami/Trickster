using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tricksters.Core;
// TODO : move this to hallowed? 

/// <summary>
/// The wrapper class that map and bind keyboard and gamepad input into a
/// collection of actions
/// </summary>
/// <typeparam name="T">the enum where the inputs are bound too</typeparam>
public class InputMap<T> : IGameComponent, IUpdateable where T : System.Enum
{
  private Dictionary<T, List<AbstractKey>> actions;
  private GamePadState newGamePadState;
  private KeyboardState newState;
  private GamePadState oldGamePadState;
  private KeyboardState oldState;

  /// <summary>
  /// Access to the Actions Map so you can change them if needed at runtime
  /// </summary>
  /// <example>
  /// Change the actions' bindings at runtime such as remapping the keyboard
  /// </example>
  public Dictionary<T, List<AbstractKey>> Actions => actions;

  public void Initialize()
  {
    actions = new Dictionary<T, List<AbstractKey>>();
  }

  /// <summary>
  /// Update the InputMap
  /// </summary>
  public void Update(GameTime gameTime)
  {
    newState = GetState();
    oldState = newState;
    newGamePadState = GetGamepadState();
    oldGamePadState = newGamePadState;
  }

  public bool Enabled { get; }

  public int UpdateOrder { get; }

  public event EventHandler<EventArgs> EnabledChanged;

  public event EventHandler<EventArgs> UpdateOrderChanged;

  /// <summary>
  /// Determines whether the specified action is currently being pressed.
  /// </summary>
  /// <remarks>
  /// It will handle both </remarks>
  /// <param name="name">The action to check.</param>
  /// <returns>True if the action is pressed, otherwise false.</returns>
  /// <exception cref="Exception">Thrown when the specified action does not exist.</exception>
  public bool IsPressed(T name)
  {
    if (!actions.TryGetValue(name, out var action)) throw new Exception($"the action {name} does not exists!");
    foreach (var input in action)
    {
      switch (input.type)
      {
        case InputType.Keyboard:
        {
          var key = (Keys)input.index;
          if (GetState().IsKeyDown(key))
          {
            return true;
          }
          break;
        }
        case InputType.Gamepad:
        {
          var button = (Buttons)input.index;
          if (GetGamepadState().IsButtonDown(button))
          {
            return true;
          }
          break;
        }
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
    return false;
  }

  /// <summary>
  /// Determines whether the specified key is currently being pressed.
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  public bool IsPressed(Keys key)
  {
    return GetState().IsKeyDown(key);
  }


  /// <summary>
  /// Check whether the specified action is just released 
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public bool IsUp(T name)
  {
    if (!actions.TryGetValue(name, out List<AbstractKey> value)) throw new Exception($"the action {name} does not exists!");
    var action = value;
    foreach (var input in action)
    {
      switch (input.type)
      {
        case InputType.Keyboard:
          var key = (Keys)input.index;
          return GetState().IsKeyUp(key);
        case InputType.Gamepad:
          var button = (Buttons)input.index;
          return GetGamepadState().IsButtonUp(button);
      }
    }

    return false;
  }

  /// <summary>
  /// Check whether the specified key is just released 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public bool IsUp(Keys key)
  {
    return GetState().IsKeyUp(key);
  }

  /// <summary>
  /// Check whether the specified action is pressed only once.
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public bool IsTriggered(T name)
  {
    if (!actions.TryGetValue(name, out List<AbstractKey> value)) throw new Exception($"the action {name} does not exists!");
    foreach (var input in value)
    {
      switch (input.type)
      {
        case InputType.Keyboard:
          var key = (Keys)input.index;
          return (GetState().IsKeyDown(key) && oldState.IsKeyUp(key));
        case InputType.Gamepad:
          var button = (Buttons)input.index;
          return (GetGamepadState().IsButtonDown(button) && oldGamePadState.IsButtonUp(button));
      }
    }

    return false;
  }

  /// <summary>
  /// Check whether the specified key is pressed only once.
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  public bool IsTriggered(Keys key)
  {
    return GetState().IsKeyDown(key) && oldState.IsKeyUp(key);
  }

  private static KeyboardState GetState() => PlatformGetState();

  private static GamePadState GetGamepadState() => GamePad.GetState(PlayerIndex.One);

  private static KeyboardState PlatformGetState()
  {
    return Keyboard.GetState();
  }

  #region Binding

  /// <summary>
  /// will bind keys to an action
  /// </summary>
  /// <param name="name">the input action</param>
  /// <param name="key"> the key to bind to the action</param>
  public void BindAction(T name, Keys key)
  {
    var input = new AbstractKey()
    {
      type = InputType.Keyboard,
      index = (int)key
    };

    if (actions.ContainsKey(name))
    {
      actions[name].Add(input);
    }
    else
    {
      var list = new List<AbstractKey>() { input };
      actions.Add(name, list);
    }
  }

  /// <summary>
  /// bind an array of keys  to an action
  /// </summary>
  /// <param name="name">the input action</param>
  /// <param name="keys">the array of keys to bind to the action</param>
  public void BindAction(T name, Keys[] keys)
  {
    if (actions.ContainsKey(name))
    {
      foreach (var t in keys)
      {
        var input = new AbstractKey()
        {
          type = InputType.Keyboard,
          index = (int)t
        };
        actions[name].Add(input);
      }
    }
    else
    {
      var list = ConvertToList(keys, InputType.Keyboard);
      actions.Add(name, list);
    }
  }

  /// <summary>
  /// bind a list of keys to an action
  /// </summary>
  /// <param name="name">the input action</param>
  /// <param name="keys">the list of keys to bind to the action</param>
  public void BindAction(T name, List<Keys> keys)
  {
    if (actions.ContainsKey(name))
    {
      var list = ConvertToList(keys, InputType.Keyboard);
      var result = list.Concat(actions[name]).ToList();
      actions[name] = result;
    }
    else
    {
      var list = ConvertToList(keys, InputType.Keyboard);
      actions.Add(name, list);
    }
  }

  /// <summary>
  /// bind a button to an action
  /// </summary>
  /// <param name="name"> the input action</param>
  /// <param name="button"> the button to bind to the action</param>
  public void BindAction(T name, Buttons button)
  {
    var input = new AbstractKey()
    {
      type = InputType.Gamepad,
      index = (int)button
    };
    if (actions.ContainsKey(name))
    {
      actions[name].Add(input);
    }
    else
    {
      var list = new List<AbstractKey>() { input };
      actions.Add(name, list);
    }
  }

  /// <summary>
  /// bind an array of buttons to an action
  /// </summary>
  /// <param name="name">the input action</param>
  /// <param name="buttons">the array of buttons to bind to the action</param>
  public void BindAction(T name, Buttons[] buttons)
  {
    if (actions.ContainsKey(name))
    {
      foreach (var button in buttons)
      {
        var input = new AbstractKey()
        {
          type = InputType.Gamepad,
          index = (int)button
        };
        actions[name].Add(input);
      }
    }
    else
    {
      var list = ConvertToList(buttons, InputType.Gamepad);
      actions.Add(name, list);
    }
  }

  /// <summary>
  /// bind a list of buttons to an action
  /// </summary>
  /// <param name="name"> the input action</param>
  /// <param name="buttons">the list of buttons to bind to the action</param>
  public void BindAction(T name, List<Buttons> buttons)
  {
    if (actions.TryGetValue(name, out List<AbstractKey> value))
    {
      var list = ConvertToList(buttons, InputType.Gamepad);
      var result = list.Concat(value).ToList();
      actions[name] = result;
    }
    else
    {
      var list = ConvertToList(buttons, InputType.Gamepad);
      actions.Add(name, list);
    }
  }

  #endregion

  #region ListConverter

  private static List<AbstractKey> ConvertToList(List<Keys> keys, InputType type)
  {
    var list = new List<AbstractKey>();
    foreach (var key in keys)
    {
      var input = new AbstractKey()
      {
        type = type,
        index = (int)key
      };
    }

    return list;
  }

  private static List<AbstractKey> ConvertToList(Keys[] keys, InputType type)
  {
    var list = new List<AbstractKey>();
    foreach (var key in keys)
    {
      var input = new AbstractKey()
      {
        type = type,
        index = (int)key
      };
    }

    return list;
  }

  private static List<AbstractKey> ConvertToList(List<Buttons> buttons, InputType type)
  {
    var list = new List<AbstractKey>();
    foreach (var button in buttons)
    {
      var input = new AbstractKey()
      {
        type = type,
        index = (int)button
      };
    }

    return list;
  }

  private static List<AbstractKey> ConvertToList(Buttons[] buttons, InputType type)
  {
    var list = new List<AbstractKey>();
    foreach (var button in buttons)
    {
      var input = new AbstractKey()
      {
        type = type,
        index = (int)button
      };
    }

    return list;
  }

  #endregion

}
/// <summary>
/// the struct that defines an abstract key that can act both for a keyboard and a gamepad 
/// </summary>
public struct AbstractKey
{
  public InputType type;
  public int index;

  public AbstractKey(InputType type)
  {
    this.type = type;
  }
}
/// <summary>
/// The abstract key Input Type which is either keyboard or gamepad
/// </summary>
public enum InputType
{
  Keyboard,
  Gamepad
}
