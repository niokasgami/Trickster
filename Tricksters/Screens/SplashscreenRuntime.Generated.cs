//Code for Splashscreen
using GumRuntime;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace Tricksters.Screens
{
    partial class SplashscreenRuntime:Gum.Wireframe.BindableGue
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Splashscreen", typeof(SplashscreenRuntime));
        }
        public ColoredRectangleRuntime background { get; protected set; }
        public SpriteRuntime Logo { get; protected set; }

        public SplashscreenRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Splashscreen");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            background = this.GetGraphicalUiElementByName("background") as ColoredRectangleRuntime;
            Logo = this.GetGraphicalUiElementByName("Logo") as SpriteRuntime;
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
