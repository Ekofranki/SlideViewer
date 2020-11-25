using System.Windows.Controls;

namespace SlideViewer.Views
{
    public partial class ImageListView : UserControl
	{
		public ImageListView()
		{
			InitializeComponent();
		}

        private void ImageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                listBox.ScrollIntoView(listBox.SelectedItem);
            }
        }
	}
}
