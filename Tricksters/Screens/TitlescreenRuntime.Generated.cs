//Code for Titlescreen
using GumRuntime;
using MonoGameGum.GueDeriving;
using Tricksters.Components;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using MonoGameGum.GueDeriving;
namespace Tricksters.Screens;
partial class TitlescreenRuntime : Gum.Wireframe.BindableGue
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Titlescreen", typeof(TitlescreenRuntime));
    }
    public TextRuntime Title { get; protected set; }
    public ContainerRuntime commandsContainer { get; protected set; }
    public ButtonStandardRuntime StartButton { get; protected set; }
    public ButtonStandardRuntime OptionButton { get; protected set; }
    public ButtonStandardRuntime ExitButton { get; protected set; }

    public TitlescreenRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Titlescreen");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        Title = this.GetGraphicalUiElementByName("Title") as TextRuntime;
        commandsContainer = this.GetGraphicalUiElementByName("commandsContainer") as ContainerRuntime;
        StartButton = this.GetGraphicalUiElementByName("StartButton") as ButtonStandardRuntime;
        OptionButton = this.GetGraphicalUiElementByName("OptionButton") as ButtonStandardRuntime;
        ExitButton = this.GetGraphicalUiElementByName("ExitButton") as ButtonStandardRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
