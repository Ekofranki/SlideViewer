using System;

namespace Application.ViewModel
{
    public class ImageViewModel : ViewModelBase
	{
        private Uri _imageUrl;

        public Uri ImageUrl
        {
            get
            {
                if (_imageUrl == null)
                {
                    _imageUrl = new Uri(string.Empty, UriKind.RelativeOrAbsolute);
                }

                return _imageUrl;
            }
            set
            {
                _imageUrl = value;

                OnPropertyChanged(nameof(ImageUrl));
            }
        }
	}
}