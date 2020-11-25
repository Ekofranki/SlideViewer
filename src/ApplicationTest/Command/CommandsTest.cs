using System.Linq;
using System.Windows.Threading;
using Application.Command.Image;
using Application.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationTest.Command
{
    [TestClass]
    public class CommandsTest
    {
        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(2, true)]
        public void CanClearImages_DefinedCollection_Checked(int imagesCount, bool canClear)
        {
            var viewModel = new ImageListViewModel(Dispatcher.CurrentDispatcher);

            foreach (var _ in Enumerable.Range(0, imagesCount))
            {
                viewModel.ImageListCollection.Add(new ImageViewModel());
            }

            Assert.AreEqual(canClear, Commands.ClearImageFilesCommand.CanExecute(viewModel));
        }

        [TestMethod]
        public void CanSelectLastImage_AlreadySelected_Denied()
        {
            var selectedImage = new ImageViewModel();
            var viewModel = new ImageListViewModel(Dispatcher.CurrentDispatcher)
            {
                SelectedImage = selectedImage,
                ImageListCollection = {new ImageViewModel(), selectedImage}
            };

            Assert.IsFalse(Commands.SelectLastImageCommand.CanExecute(viewModel));
        }

        [TestMethod]
        public void CanSelectLastImage_FirstSelected_Allowed()
        {
            var selectedImage = new ImageViewModel();
            var viewModel = new ImageListViewModel(Dispatcher.CurrentDispatcher)
            {
                SelectedImage = selectedImage,
                ImageListCollection = {selectedImage, new ImageViewModel()}
            };

            Assert.IsTrue(Commands.SelectLastImageCommand.CanExecute(viewModel));
        }
    }
}