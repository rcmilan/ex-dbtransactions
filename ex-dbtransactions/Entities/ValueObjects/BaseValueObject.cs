namespace ex_dbtransactions.Entities.ValueObjects
{
    public abstract class BaseValueObject
    {
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (BaseValueObject)obj;

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode() => GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);

        protected static bool EqualOperator(BaseValueObject left, BaseValueObject right) => !(left is null ^ right is null) && (ReferenceEquals(left, right) || left.Equals(right));

        protected static bool NotEqualOperator(BaseValueObject left, BaseValueObject right) => !EqualOperator(left, right);

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}