using System.Collections.Generic;
using Kata.Dahl.Comparers;

namespace Kata.Dahl.Dictionaries
{
    public class DiscountDictionary : SortedDictionary<decimal, int>
    {
        public DiscountDictionary() : base(new DescendingComparer<decimal>()) { }

        public new void Add(decimal discount, int differentItemCount)
        {
            base.Add(discount, differentItemCount);
        }
    }
}
