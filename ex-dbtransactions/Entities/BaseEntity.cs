namespace ex_dbtransactions.Entities
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; } = default!;
    }
}
