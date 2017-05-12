namespace licensemanager.Models
{
    public class ResponseModel<T>
    {
        public string Status { get; set; }

        public T Data { get; set; }
    }
}