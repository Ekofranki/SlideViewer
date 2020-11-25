using System;
using System.Collections.ObjectModel;

namespace Application.ViewModel
{
    public class ImageCollection : ObservableCollection<ImageViewModel>
    {
        public ImageCollection()
        {
            Add(new ImageViewModel {ImageUrl = new Uri(@"C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg")});
            Add(new ImageViewModel {ImageUrl = new Uri(@"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg")});
            Add(new ImageViewModel {ImageUrl = new Uri(@"C:\Users\Public\Pictures\Sample Pictures\Lighthouse.jpg")});
            Add(new ImageViewModel {ImageUrl = new Uri(@"C:\Users\Public\Pictures\Sample Pictures\Lighthouse.jpg")});
        }
    }
}