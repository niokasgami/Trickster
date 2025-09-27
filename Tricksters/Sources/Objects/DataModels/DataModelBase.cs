using System;

namespace Tricksters.Objects.DataModels;

/// <summary>
/// The abstract class that handle any of  shape the basic shape of data model.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class DataModelBase<T> where T : DataModelBase<T>, IDisposable
{
  /// <summary>
  /// The Data model id. its allow to fetch it in the GameRegistry.
  /// </summary>
  public string Id { get; init; }

  /// <summary>
  /// Load the data model from a file
  /// </summary>
  /// <param name="filePath"></param>
  /// <returns></returns>
  public static T From(string filePath)
  {
    return DataLoader.LoadJson<T>(filePath);
  }

  /// <summary>
  /// Create an deep copy of the data model
  /// </summary>
  /// <returns> a deep copy of the data model</returns>
  public T Clone()
  {
    var clone = (T)MemberwiseClone();
    clone.OnCloned();
    return clone;
  }

  /// <summary>
  /// Create a shallow copy of the data model
  /// </summary>
  /// <returns> a shallow copy of the model</returns>
  public T ShallowClone()
  {
    return (T)MemberwiseClone();
  }
  protected virtual void OnCloned()
  {
    
  }

  public virtual void Dispose()
  {
    
  }
}
