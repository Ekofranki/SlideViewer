using Application.ViewModel;
using System.Windows.Input;
using Application.Services;

namespace Application.Command.Image
{
    public partial class Commands
    {
        private static ICommand _deleteImageFileCommand;

        public static ICommand DeleteImageFileCommand
        {
            get
            {
                return _deleteImageFileCommand ??= new RelayCommand(DeleteFile, CanDeleteFile);
            }
        }

        private static void DeleteFile(object @object)
        {
            var viewModel = (ImageListViewModel)@object;

            var index = viewModel.ImageListCollection.IndexOf(viewModel.SelectedImage);

            if (viewModel.ImageListCollection.Contains(viewModel.SelectedImage))
            {
                ImageRepository.DeleteBlob(viewModel.SelectedImage.DisplayName);

                viewModel.ImageListCollection.Remove(viewModel.SelectedImage);
            }

            if (index + 1 < viewModel.ImageListCollection.Count && viewModel.ImageListCollection[index] != null)
            {
                viewModel.SelectedImage = viewModel.ImageListCollection[index];
            }
        }

        private static bool CanDeleteFile(object @object) =>
            @object is ImageListViewModel vm &&
            vm.SelectedImage != null;
    }
}