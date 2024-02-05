using BuildingLink.Core.Database.Attributes;


namespace BuildingLink.Core.Common.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {

        }

        [DbColumn(IsIdentity = true, IsPrimary = true)]
        public string Id { get; set; }
        [DbColumn]
        public string CreatedAt { get; set; }
        [DbColumn]
        public string CreatedBy { get; set; }
        [DbColumn]
        public string? LastModifiedAt { get; set; }
        [DbColumn]
        public string? ModifiedBy { get; set; }
    }
}