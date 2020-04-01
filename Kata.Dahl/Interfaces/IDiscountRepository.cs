using Kata.Dahl.Dictionaries;

namespace Kata.Dahl.Interfaces
{
    public interface IDiscountRepository
    {
        public DiscountDictionary GetDiscounts();
    }
}
