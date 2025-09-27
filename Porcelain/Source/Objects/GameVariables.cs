using System;

namespace Porcelain.Objects;

public enum ScopeType
{
  Global, // kinda self-explanatory
  Local // bound to savefiles but can be access throughout the game.
}
/// <summary>
/// Represents a dynamic, type-safe game variable that can store a value of any type.
/// </summary>
[Serializable]
public class GameVariable<T>
{
  public string Name { get; }
  public ScopeType Scope { get; }
  public event Action<T, T> OnValueChanged;
  
  private T _value;
  private Func<T, bool> _validator;
  
  public GameVariable(string name, T initialValue, ScopeType scope = ScopeType.Local)
  {
    Name = name;
    _value = initialValue;
    Scope = scope;
  }

  public GameVariable(string name, T initialValue, ScopeType scope = ScopeType.Local, Func<T, bool> validator = null)
  {
    Name = name;
    _value = initialValue;
    Scope = scope;
    _validator = validator;
  }

  public bool IsType<TCheck>()
  {
    return _value is TCheck;
  }

  public T GetValue()
  {
    return _value;
  }

  public void SetValue(T newValue)
  {
    if (_validator != null && !_validator(newValue))
    {
      throw new ArgumentException($"Value {newValue} is not valid for variable {Name}");
    }
    var oldValue = _value;
    _value = newValue;
    OnValueChanged?.Invoke(oldValue, newValue);
  }

  public void SetValidator(Func<T, bool> validator)
  {
    _validator = validator;
  }
}
