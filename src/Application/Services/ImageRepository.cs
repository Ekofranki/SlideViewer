using System;
using System.Collections.ObjectModel;
using System.IO;
using Application.Common;
using Application.ViewModel;
using Azure.Storage.Blobs;

namespace Application.Services
{
    public static class ImageRepository
    {
        public static ObservableCollection<ImageViewModel> GetImagesFromBlob()
        {
            var images = new ObservableCollection<ImageViewModel>();

            var containerClient =
                new BlobContainerClient(Environment.GetEnvironmentVariable(CredentialKeys.StorageConnectionKey),
                    CredentialKeys.BlobContainerName);

            foreach (var blobItem in containerClient.GetBlobs())
            {
                var newFileName = Path.Combine(Directory.GetCurrentDirectory(), blobItem.Name);

                var blobFile = containerClient.GetBlobClient(blobItem.Name).Download();

                using (var downloadFileStream = File.OpenWrite(newFileName))
                {
                    blobFile.Value.Content.CopyTo(downloadFileStream);
                    downloadFileStream.Close();
                }

                images.Add(new ImageViewModel
                {
                    DisplayName = blobItem.Name,
                    ImageUrl = new Uri(newFileName)
                });
            }

            return images;
        }

        public static void AddBlobImage(ImageViewModel model)
        {
            using var fileStream = File.OpenRead(model.ImageUrl.AbsolutePath);

            new BlobClient(Environment.GetEnvironmentVariable(CredentialKeys.StorageConnectionKey),
                    CredentialKeys.BlobContainerName, model.DisplayName)
                .Upload(fileStream);
        }

        public static void DeleteBlob(string name) =>
            new BlobClient(Environment.GetEnvironmentVariable(CredentialKeys.StorageConnectionKey),
                    CredentialKeys.BlobContainerName, name)
                .Delete();
    }
}