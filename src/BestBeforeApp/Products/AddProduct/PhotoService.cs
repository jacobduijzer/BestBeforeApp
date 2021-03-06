using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;

namespace BestBeforeApp.Products.AddProduct
{
    public class PhotoService
    {
        public async Task<Stream> TakePhoto()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                throw new Exception("Camera not available of not supported");

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "BestBeforeApp",
                SaveToAlbum = false,
                CompressionQuality = 40,
                CustomPhotoSize = 40,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                MaxWidthHeight = 2000,
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear
            }).ConfigureAwait(false);

            if (file == null)
                return null;
           
            var stream = file.GetStream();
            file.Dispose();
            return stream;
        }
    }
}
