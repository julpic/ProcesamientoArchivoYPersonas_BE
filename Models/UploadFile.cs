namespace WebApplication4.Models
{
    public class UploadFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public IFormFile file { get; set; }
    }
}
