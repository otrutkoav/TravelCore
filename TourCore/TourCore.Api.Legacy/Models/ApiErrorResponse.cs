namespace TourCore.Api.Legacy.Models
{
    public class ApiErrorResponse
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public object Details { get; set; }
    }
}