using System.Windows.Input;
using Application.ViewModel;

namespace Application.Command.Image
{
    public partial class Commands
    {
        private static ICommand _selectNextImageCommand;

        public static ICommand SelectNextImageCommand
        {
            get
            {
                return _selectNextImageCommand ??= new RelayCommand(SelectNextImage, CanSelectNextImage);
            }
        }

        private static void SelectNextImage(object @object)
        {
            if (@object is ImageListViewModel vm)
            {
                vm.SelectedImage =
                    vm.ImageListCollection[vm.ImageListCollection.IndexOf(vm.SelectedImage) + 1];
            }
        }

        private static bool CanSelectNextImage(object @object) =>
            @object is ImageListViewModel vm &&
            vm.ImageListCollection.IndexOf(vm.SelectedImage) < vm.ImageListCollection.Count - 1;
    }
}