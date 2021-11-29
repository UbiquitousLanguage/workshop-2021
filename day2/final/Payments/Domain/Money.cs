namespace Payments.Domain;

public record Money {
    public float  Amount   { get; }
    public string Currency { get; }

    public Money(float amount, string currency) {
        Amount   = amount;
        Currency = currency;
    }
}