using System.ComponentModel.DataAnnotations;

namespace Music340.Validation
{
    public class ItemImgValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            IFormFile file = (IFormFile)value;
            int maxSize = 1024 * 1024 * 5; //5 MB file size
            string[] validExtensions = { ".jpg", ".gif", ".png", ".jpeg" };
            if (file is null)
            {
                ErrorMessage = "Please upload a file";
                return false;
            }
            if (!validExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                ErrorMessage = $"Not an Image File. Please Upload {string.Join(", ", validExtensions)}";
                return false;
            }
            if (file.Length > maxSize)
            {
                ErrorMessage = $"File must be less than 5 MB";
                return false;
            }
            return true;
        }
    }
}
