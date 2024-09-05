namespace Api.Helper
{
    public class ImagesUrlComplaintResolver
    {
        public async Task<string> UploadImage(IFormFile imageFile)
        {
            // Ensure the image file is not null
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Image file is null or empty");

            // Validate the file extension to ensure it's an image
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Invalid image file format");

            // Generate a unique prefix for the file name
            var uniquePrefix = Guid.NewGuid().ToString();

            // Combine the unique prefix with the original file name
            var uniqueFileName = uniquePrefix + "_" + imageFile.FileName;

            // Define the directory where the image will be stored
            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "complaintImages");

            // Ensure the directory exists, create it if it doesn't
            if (!Directory.Exists(uploadDirectory))
                Directory.CreateDirectory(uploadDirectory);

            // Define the path where the image will be saved
            var filePath = Path.Combine(uploadDirectory, uniqueFileName);

            // Copy the image file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the URL of the uploaded image
            var imageUrl = "images/complaintImages/" + uniqueFileName;
            return imageUrl;
        }

        internal object UploadImage(string imageUrl)
        {
            throw new NotImplementedException();
        }
    }
}
