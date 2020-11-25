using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Application.Services;

namespace Application.ViewModel
{
    public class ImageListViewModel : ViewModelBase
    {
        private ObservableCollection<ImageViewModel> _imageCollection;
        private ImageViewModel _selectedImage;
        private DispatcherTimer _clock;
        private string _status;
        private double _slideShowSpeed;
        private int _selectedImageIndex;

        public ImageListViewModel(Dispatcher currentDispatcher)
        {
            _imageCollection = new ObservableCollection<ImageViewModel>();

            CurrentDispatcher = currentDispatcher;

            SlideShowSpeed = 5;

            Clock.Interval = TimeSpan.FromSeconds(SlideShowSpeed);
            Clock.Tick += Clock_Tick;
        }

        public ObservableCollection<ImageViewModel> ImageListCollection => _imageCollection;

        public ImageViewModel SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
                SelectedImageIndex = ImageListCollection.IndexOf(SelectedImage) + 1;
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public DispatcherTimer Clock => _clock ??= new DispatcherTimer(DispatcherPriority.Normal, CurrentDispatcher);

        public bool IsClockTicking
        {
            get => Clock.IsEnabled;
            set
            {
                Clock.IsEnabled = value;
                OnPropertyChanged(nameof(IsClockTicking));
            }
        }

        public double SlideShowSpeed
        {
            get => _slideShowSpeed;
            set
            {
                _slideShowSpeed = value;
                Clock.Interval = TimeSpan.FromSeconds(SlideShowSpeed);
                OnPropertyChanged(nameof(SlideShowSpeed));
            }
        }

        public Dispatcher CurrentDispatcher { get; }

        public int SelectedImageIndex
        {
            get => _selectedImageIndex;
            set
            {
                _selectedImageIndex = value;
                OnPropertyChanged(nameof(SelectedImageIndex));
            }
        }

        public void InitializeCollection()
        {
            _imageCollection = ImageRepository.GetImagesFromBlob();

            if (_imageCollection.Count > 0)
            {
                _selectedImage = _imageCollection[0];
            }
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            var s = this;

            if (s.ImageListCollection != null && s.ImageListCollection.Count > 1)
            {
                s.SelectedImage =
                    s.ImageListCollection.IndexOf(s.SelectedImage) < s.ImageListCollection.Count - 1
                        ? s.ImageListCollection[s.ImageListCollection.IndexOf(s.SelectedImage) + 1]
                        : s.ImageListCollection[0];
            }
        }
    }
}