using Kata.Dahl.Models;

namespace Kata.Dahl.Interfaces
{
    public interface IShoppingBasket
    {
        public void AddBook(Book book);
        
        public decimal GetTotal();
    }
}
