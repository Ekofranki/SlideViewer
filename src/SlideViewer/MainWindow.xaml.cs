using System;
using System.Windows;
using System.Windows.Threading;
using Application.Command.Image;
using Application.ViewModel;
using DynamicData.Annotations;

namespace SlideViewer
{
    [UsedImplicitly]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var imageListViewModel = new ImageListViewModel(Dispatcher.CurrentDispatcher);

            imageListViewModel.InitializeCollection();

            var mainWindowViewModel = new MainWindowViewModel(imageListViewModel);

            DataContext = mainWindowViewModel;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (!(sender is FrameworkElement w) || !(w.DataContext is MainWindowViewModel dc))
            {
                return;
            }

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            if (e.Data.GetData(DataFormats.FileDrop, true) is string[] droppedFilePaths)
            {
                foreach (var droppedFilePath in droppedFilePaths)
                {
                    dc.DroppedFiles.Add(new ImageViewModel
                    {
                        ImageUrl = new Uri(droppedFilePath, UriKind.RelativeOrAbsolute),
                        DisplayName = Guid.NewGuid().ToString()
                    });
                }
            }

            Commands.ManageDroppedFilesCommand.Execute(dc);
        }
    }
}