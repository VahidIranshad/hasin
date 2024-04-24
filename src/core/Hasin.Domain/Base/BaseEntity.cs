namespace Hasin.Domain.Base
{
    public abstract class BaseEntity<T>
    {
        public required T Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>;
}
