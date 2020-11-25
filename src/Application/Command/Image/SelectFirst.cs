using Application.ViewModel;
using System.Windows.Input;

namespace Application.Command.Image
{
    public partial class Commands
    {
        private static ICommand _selectFirstImageCommand;

        public static ICommand SelectFirstImageCommand
        {
            get
            {
                return _selectFirstImageCommand ??= new RelayCommand(SelectFirstImage,
                    CanSelectFirstImage);
            }
        }

        private static void SelectFirstImage(object @object)
        {
            if (@object is ImageListViewModel vm)
            {
                vm.SelectedImage = vm.ImageListCollection[0];
            }
        }

        private static bool CanSelectFirstImage(object @object) =>
            @object is ImageListViewModel vm &&
            vm.ImageListCollection != null &&
            vm.ImageListCollection.Count > 1 &&
            vm.SelectedImage != vm.ImageListCollection[0];
    }
}