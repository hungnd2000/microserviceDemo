namespace Order.API.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public int IdentityId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
