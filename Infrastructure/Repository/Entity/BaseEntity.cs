namespace Inf.Repository.Entity
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }

        public string Name { get; set; }
    }
}
