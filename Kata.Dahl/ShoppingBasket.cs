using System.Collections.Generic;
using System.Linq;
using Kata.Dahl.Dictionaries;
using Kata.Dahl.Interfaces;
using Kata.Dahl.Models;

namespace Kata.Dahl
{
    public class ShoppingBasket : IShoppingBasket
    {
        private readonly IDiscountRepository discountRepository;

        private readonly ScannedBookDictionary scannedBooks;

        public ShoppingBasket(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;

            scannedBooks = new ScannedBookDictionary();
        }

        public void AddBook(Book book)
        {
            if (scannedBooks.ContainsKey(book))
            {
                scannedBooks[book]++;
            }
            else
            {
                scannedBooks.Add(book, 1);
            }
        }

        public decimal GetTotal()
        {
            var totalPrice = decimal.Zero;

            static bool HasValue(KeyValuePair<Book, int> x)
            {
                return x.Value > 0;
            }

            var discounts = discountRepository.GetDiscounts();
            foreach (var (discountPercentage, differentItemCount) in discounts)
            {
                while (scannedBooks.Count(HasValue) >= differentItemCount)
                {
                    var firstSetOfDiscountedBooks = 
                        scannedBooks.Where(HasValue)
                            .Take(differentItemCount)
                            .Select(x => x.Key).ToList();
                    var price = firstSetOfDiscountedBooks.Sum(x => x.Price);
                    totalPrice += price - price * discountPercentage;
                    
                    foreach (var book in firstSetOfDiscountedBooks)
                    {
                        var bookCount = scannedBooks[book];
                        scannedBooks.Remove(book);
                        scannedBooks.Add(book, bookCount-1);
                    }
                }
            }

            foreach (var (book, count) in scannedBooks.Where(HasValue))
            {
                totalPrice += count * book.Price;
            }

            return totalPrice;
        }
    }
}
