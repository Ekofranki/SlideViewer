using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Application.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
        private ObservableCollection<ImageViewModel> _droppedFiles;

		public MainWindowViewModel(ImageListViewModel imageListViewModel)
		{
			ImageListViewModel = imageListViewModel;
		}

		public ImageListViewModel ImageListViewModel { get; }

        public ObservableCollection<ImageViewModel> DroppedFiles
		{
            get
            {
                return _droppedFiles ??= new ObservableCollection<ImageViewModel>();
            }
		}

		public Dispatcher CurrentDispatcher { get; set; }
	}
}