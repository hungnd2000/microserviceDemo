using System.ComponentModel.DataAnnotations;

namespace Order.API.DTOs
{
    public class UpsertOrder
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AdditionalAddress { get; set; }
        public int IdentityId { get; set; }
    }
}
