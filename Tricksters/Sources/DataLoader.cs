using System;
using System.IO;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Tricksters;

/// <summary>
/// the static class that allow loading and serializing files into multiple format such as JSON
/// </summary>
public static class DataLoader
{
  public static string RootDirectory { get; set; } = String.Empty;
  
  public static T LoadJson<T>(string filename)
  {
    try
    {
      using var stream = TitleContainer.OpenStream(RootDirectory + "/" + filename);
      using var reader = new StreamReader(stream);
      var jsonString = reader.ReadToEnd();
      var result = JsonConvert.DeserializeObject<T>(jsonString) ?? throw new JsonSerializationException($"Failed to deserialize {filename} to type {typeof(T).Name}");
      if (result == null)
      {
        throw new JsonSerializationException($"Failed to deserialize {filename} to type {typeof(T).Name}");
      }
      return result;
    }
    catch (JsonException ex)
    {
      throw new JsonException($"JSON parsing error in {filename}: {ex.Message}", ex);

    }
    catch (FileNotFoundException)
    {
      throw new FileNotFoundException($"Could not find file: {filename}");
    }
    catch (Exception ex)
    {
      throw new Exception($"Error loading {filename}: {ex.Message}", ex);
    }
  }

  // TODO : implement?
  public static void SaveToJson<T>(string filename, T obj)
  {
    
  }
}
