using System.Collections.Generic;
using Kata.Dahl.Comparers;
using Kata.Dahl.Models;

namespace Kata.Dahl.Dictionaries
{
    public class ScannedBookDictionary : SortedDictionary<Book, int>
    {
        public ScannedBookDictionary() : base(new BookComparer()) { }

        public new void Add(Book book, int count)
        {
            base.Add(book, count);
        }
    }
}
