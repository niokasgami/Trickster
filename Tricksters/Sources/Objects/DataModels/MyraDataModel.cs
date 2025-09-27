using System;

namespace Tricksters.Objects.DataModels;

public class MyraDataModel : DataModelBase<MyraDataModel> , IDisposable
{
  public string Name { get; set; }

  public override void Dispose()
  {
    // implement dispose
  }
}
