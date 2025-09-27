using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace Tricksters.Core;

/// <summary>
/// The texture atlas struct that handles texture regions.
/// </summary>
/// <param name="texture"></param>
/// <param name="textureDimensions"></param>
public struct TextureAtlas(Texture2D texture, Area2D textureDimensions) : IDisposable
{

  [JsonProperty("source")]
  public Texture2D texture = texture;
  [JsonProperty("dimension")]
  public Area2D textureDimensions = textureDimensions;
  [JsonProperty("regions")]
  private Dictionary<string, Rectangle> textureRegions = new Dictionary<string, Rectangle>();
  
  /// <summary>
  /// set the region of the texture atlas.
  /// </summary>
  /// <param name="key"></param>
  /// <param name="row"></param>
  /// <param name="column"></param>
  /// <exception cref="Exception"></exception>
  public void SetRegion(string key, int row, int column)
  {
    var region = CalculateRegion(column, row);
    if (textureRegions.ContainsKey(key))
      throw new Exception("Region already exists");

    textureRegions.Add(key, region);
  }

  /// <summary>
  /// Get the texture region
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public Rectangle GetRegion(string key)
  {
    if(!textureRegions.ContainsKey(key))
      throw new Exception("Region does not exist");
    return textureRegions[key];
  }
  
  public Rectangle CalculateRegion(int column, int row)
  {
    var x = column * textureDimensions.Width;
    var y = row * textureDimensions.Height;
    return new Rectangle(x, y, textureDimensions.Width, textureDimensions.Height);
  }

  public void Dispose()
  {
    texture?.Dispose();
  }
}
