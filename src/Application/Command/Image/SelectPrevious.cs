using System.Windows.Input;
using Application.ViewModel;

namespace Application.Command.Image
{
    public partial class Commands
    {
        private static ICommand _selectPreviousImageCommand;

        public static ICommand SelectPreviousImageCommand
        {
            get
            {
                return _selectPreviousImageCommand ??= new RelayCommand(SelectPreviousImage, CanSelectPreviousImage);
            }
        }

        private static void SelectPreviousImage(object @object)
        {
            if (@object is ImageListViewModel vm)
            {
                vm.SelectedImage =
                    vm.ImageListCollection[vm.ImageListCollection.IndexOf(vm.SelectedImage) - 1];
            }
        }

        private static bool CanSelectPreviousImage(object @object) =>
            @object is ImageListViewModel vm &&
            vm.ImageListCollection.IndexOf(vm.SelectedImage) > 0;
    }
}