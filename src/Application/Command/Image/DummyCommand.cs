using Application.ViewModel;
using System.Windows.Input;

namespace Application.Command.Image
{
    public partial class Commands
    {
        private static ICommand _dummyCommand;

        public static ICommand DummyCommand
        {
            get
            {
                return _dummyCommand ??= new RelayCommand(o => { }, CanExecute);
            }
        }

        private static bool CanExecute(object @object)
        {
            if (@object != null)
            {
                return @object is ImageListViewModel vm && vm.ImageListCollection != null && vm.ImageListCollection.Count > 0;
            }

            return false;
        }
    }
}