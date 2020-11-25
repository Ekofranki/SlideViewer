using System.Linq;
using System.Windows.Input;
using Application.ViewModel;

namespace Application.Command.Image
{
    public partial class Commands
    {
        private static ICommand _selectLastImageCommand;

        public static ICommand SelectLastImageCommand
        {
            get
            {
                return _selectLastImageCommand ??= new RelayCommand(SelectLastImage, CanSelectLastImage);
            }
        }

        private static void SelectLastImage(object @object)
        {
            if (@object is ImageListViewModel vm)
            {
                vm.SelectedImage = vm.ImageListCollection.Last();
            }
        }

        private static bool CanSelectLastImage(object @object) =>
            @object is ImageListViewModel vm &&
            vm.ImageListCollection != null &&
            vm.ImageListCollection.Count > 1 &&
            vm.SelectedImage != vm.ImageListCollection.Last();
    }
}