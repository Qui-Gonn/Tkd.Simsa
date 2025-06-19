namespace Tkd.Simsa.Domain.PersonManagement;

public record BirthDate(DateOnly Date)
{
    public static readonly BirthDate Empty = FromDateOnly(DateOnly.MinValue);

    public int Age => this.CalculateAge(DateTime.Today);

    public static BirthDate FromDateOnly(DateOnly birthDate)
        => new (birthDate);

    public static BirthDate FromDateTime(DateTime birthDate)
        => new (DateOnly.FromDateTime(birthDate));

    public int CalculateAge(DateTime referenceDate)
        => this.CalculateAge(DateOnly.FromDateTime(referenceDate));

    public int CalculateAge(DateOnly referenceDate)
    {
        var age = referenceDate.Year - this.Date.Year;

        if (referenceDate.Month < this.Date.Month
            || (referenceDate.Month == this.Date.Month
                && referenceDate.Day < this.Date.Day))
        {
            age--;
        }

        return age;
    }

    public DateTime ToDateTime()
        => this.Date.ToDateTime(TimeOnly.MinValue);
}