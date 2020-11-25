using Application.ViewModel;
using System;
using System.Windows.Input;
using System.Windows.Threading;
using Application.Services;

namespace Application.Command.Image
{
    public partial class Commands
    {
        private static ICommand _manageDroppedFilesCommand;

        public static ICommand ManageDroppedFilesCommand
        {
            get
            {
                return _manageDroppedFilesCommand ??= new RelayCommand(ManageDroppedFiles, o => true);
            }
        }

        private static void ManageDroppedFiles(object @object)
        {
            var mwvm = @object as MainWindowViewModel;

            var imageNumber = mwvm.ImageListViewModel.ImageListCollection.Count;

            if (mwvm.DroppedFiles != null)
            {
                foreach (var i in mwvm.DroppedFiles)
                {
                    ImageRepository.AddBlobImage(i);

                    var work = Dispatcher.FromThread(mwvm.ImageListViewModel.CurrentDispatcher.Thread)
                        ?.BeginInvoke(new Action<ImageViewModel>(mwvm.ImageListViewModel.ImageListCollection.Add),
                            DispatcherPriority.ApplicationIdle, i);

                    if (work != null)
                    {
                        work.Completed += delegate
                        {
                            if (mwvm.ImageListViewModel.SelectedImage == null)
                            {
                                mwvm.ImageListViewModel.SelectedImage =
                                    mwvm.ImageListViewModel.ImageListCollection[0];
                            }
                        };
                    }
                }
            }

            if (imageNumber == 0 && mwvm.ImageListViewModel.ImageListCollection.Count > 0)
            {
                mwvm.ImageListViewModel.SelectedImage = mwvm.ImageListViewModel.ImageListCollection[0];
            }

            mwvm.DroppedFiles?.Clear();
        }
    }
}