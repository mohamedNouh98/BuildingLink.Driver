namespace BuildingLink.Core.Database.Attributes
{
    public class DbColumnAttribute : Attribute
    {
        /// <summary>
        /// Set true if implicit conversion is required.
        /// </summary>
        public bool Convert { get; set; }
        /// <summary>
        /// Set true if the property is primary key in the table
        /// </summary>
        public bool IsPrimary { get; set; }
        /// <summary>
        /// denotes if the field is an identity type or not.
        /// </summary>
        public bool IsIdentity { get; set; }
    }
}
