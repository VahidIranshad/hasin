using Domain.Base;

namespace Domain.Entity
{
    public class Book : BaseEntity
    {
        public required string Value { get; set; }
    }
}
