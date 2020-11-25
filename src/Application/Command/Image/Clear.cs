using Application.ViewModel;
using System.Windows.Input;

namespace Application.Command.Image
{
    public partial class Commands
    {
        private static ICommand _clearImageFilesCommand;

        public static ICommand ClearImageFilesCommand
        {
            get
            {
                return _clearImageFilesCommand ??= new RelayCommand(ClearFiles, CanClearFiles);
            }
        }

        private static void ClearFiles(object @object)
        {
            var vm = @object as ImageListViewModel;

            vm?.ImageListCollection?.Clear();
        }

        static bool CanClearFiles(object @object) =>
            @object is ImageListViewModel vm &&
            vm.ImageListCollection != null &&
            vm.ImageListCollection.Count > 0;
    }
}