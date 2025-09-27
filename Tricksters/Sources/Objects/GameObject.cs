#nullable enable
using System;
using Tricksters.Objects.DataModels;

namespace Tricksters.Objects;

/// <summary>
/// The base class for all the game object.
/// 
/// </summary>
/// <typeparam name="TG"> the template game object</typeparam>
/// <typeparam name="TD"> the template dataModel</typeparam>
// @todo : GameObjects might not be needed to be OVERLY  complicated for this game implementation. GameObject in trickster are more aking to Data container. 
public abstract class GameObject<TG, TD> : IDisposable
  where TG : GameObject<TG, TD>
  where TD : DataModelBase<TD>, IDisposable
{
  
  // todo : implements Components
  /// <summary>
  /// The game object id.
  /// </summary>
  public string Id { get; init; }
  /// <summary>
  /// The game object group id.
  /// </summary>
  public string GroupId { get; init;}
  /// <summary>
  /// the data model of the game object
  /// </summary>
  public DataModelBase<TD>? Data { get; set; }
  
  
  protected GameObject(string id,  DataModelBase<TD>? dataModel, string groupId = "default")
  {
    Id = id;
    GroupId = groupId;
    Data = dataModel;
  }

  public virtual void Dispose()
  {
    Data?.Dispose();
    // TODO release managed resources here
  }
}
