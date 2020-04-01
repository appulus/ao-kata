using System;
using System.Collections.Generic;
using Kata.Dahl.Models;

namespace Kata.Dahl.Comparers
{
    public class BookComparer : IEqualityComparer<Book>, IComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            return x.ISBN == y.ISBN;
        }

        public int GetHashCode(Book obj)
        {
            return obj.ISBN.GetHashCode();
        }

        public int Compare(Book x, Book y)
        {
            if (x.Price > y.Price)
            {
                return 1;
            }

            if (x.Price < y.Price)
            {
                return -1;
            }

            return string.Compare(x.ISBN, y.ISBN, StringComparison.Ordinal);
        }
    }
}
