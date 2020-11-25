using Application.ViewModel;
using Microsoft.Win32;
using System;
using System.Windows.Input;
using Application.Services;

namespace Application.Command.Image
{
    public partial class Commands
    {
        private static ICommand _openImageFileCommand;

        public static ICommand OpenImageFileCommand
        {
            get
            {
                return _openImageFileCommand ??= new RelayCommand(OpenFile, o => true);
            }
        }

        private static void OpenFile(object @object)
        {
            var viewModel = @object as ImageListViewModel;

            var dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*",
                RestoreDirectory = true,
                Multiselect = true
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            foreach (var file in dialog.FileNames)
            {
                var imageToBeAdded = new ImageViewModel
                {
                    ImageUrl = new Uri(file),
                    DisplayName = Guid.NewGuid().ToString()
                };

                viewModel?.ImageListCollection.Add(imageToBeAdded);

                ImageRepository.AddBlobImage(imageToBeAdded);
            }
        }
    }
}