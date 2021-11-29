using Bookings.Domain;

namespace Bookings.Application; 

public static class DiscountService {
    public static ValueTask<Money> GetDiscount(Money original, string code) {
        var (amount, currency) = original;
        var newAmount   = code == "2021" ? amount / 2 : 10;
        return new ValueTask<Money>(Money.FromCurrency(newAmount, currency));
    }
}
