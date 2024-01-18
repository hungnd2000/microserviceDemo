namespace Basket.API.DTOs
{
    public class UpsetCustomerBasketResponseDTO
    {
        public string Message { get; set; }
        public object Data { get; set; }

        public UpsetCustomerBasketResponseDTO(string message, object data)
        {
            Message = message;
            Data = data;
        }
    }
}
