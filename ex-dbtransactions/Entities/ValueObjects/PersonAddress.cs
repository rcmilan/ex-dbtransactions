namespace ex_dbtransactions.Entities.ValueObjects
{
    public class PersonAddress : BaseValueObject
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return District;
        }
    }
}
