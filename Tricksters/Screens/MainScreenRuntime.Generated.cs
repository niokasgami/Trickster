//Code for MainScreen
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using MonoGameGum.GueDeriving;
namespace Tricksters.Screens
{
    public partial class MainScreenRuntime:Gum.Wireframe.BindableGue
    {

        public MainScreenRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {


        }
        public override void AfterFullCreation()
        {
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
