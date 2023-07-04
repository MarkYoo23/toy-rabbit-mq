namespace Domain.SeedWorks;

public abstract class ValueObject
{
    // ReSharper disable once MemberCanBePrivate.Global
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }

        return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !(EqualOperator(left, right));
    }
    
    // TODO : (dh) 테스트 해 보기
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
    
    private IEnumerable<object?> GetEqualityComponents()
    {
        return GetType().GetProperties().Select(info => info.GetValue(info));
    }

    public ValueObject GetCopy()
    {
        return (this.MemberwiseClone() as ValueObject)!;
    }
}