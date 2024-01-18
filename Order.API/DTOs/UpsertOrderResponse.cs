namespace Order.API.DTOs
{
    public class UpsertOrderResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public UpsertOrderResponse(string message, object data)
        {
            Message = message;
            Data = data;
        }
    }
}
