using BuildingLink.Core.Common.Entities;
using BuildingLink.Core.Database.Attributes;

namespace BuildingLink.Core.Drivers.Entities
{
    public class Driver : BaseEntity
    {
        [DbColumn]
        public string FirstName { get; set; }
        [DbColumn]
        public string LastName { get; set; }
        [DbColumn]
        public string Email { get; set; }
        [DbColumn]
        public string PhoneNumber { get; set; }
    }
}