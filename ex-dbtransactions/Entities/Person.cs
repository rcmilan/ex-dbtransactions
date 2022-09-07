using ex_dbtransactions.Entities.ValueObjects;

namespace ex_dbtransactions.Entities
{
    public class Person : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public virtual PersonAddress Address { get; set; }

        public Department Department { get; set; }
    }
}
