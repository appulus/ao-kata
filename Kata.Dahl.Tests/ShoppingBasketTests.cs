using Kata.Dahl.Dictionaries;
using Kata.Dahl.Interfaces;
using Kata.Dahl.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kata.Dahl.Tests
{
    /// <summary>
    /// ShoppingBasket Tests
    /// </summary>
    [TestClass]
    public class ShoppingBasketTests
    {
        private ShoppingBasket shoppingBasket;

        private Mock<IDiscountRepository> discountProcessor;

        [TestInitialize]
        public void Initialise()
        {
            discountProcessor = new Mock<IDiscountRepository>();

            discountProcessor.Setup(x => x.GetDiscounts()).Returns(GetDiscounts());

            shoppingBasket = new ShoppingBasket(discountProcessor.Object);
        }

        [TestMethod]
        public void WhenNoBooks_ThenTotalZero()
        {
            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void WhenOneOfFirstBook_ThenTotalPriceIsOfFirstBook()
        {
            AddBooks(1);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(8, total);
        }

        [TestMethod]
        public void WhenOneOfSecondBook_ThenTotalPriceIsOfSecondBook()
        {
            AddBooks(2);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(8, total);
        }

        [TestMethod]
        public void WhenOneOfThirdBook_ThenTotalPriceIsOfThirdBook()
        {
            AddBooks(3);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(8, total);
        }

        [TestMethod]
        public void WhenOneOfFourthBook_ThenTotalPriceIsOfFourthBook()
        {
            AddBooks(4);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(8, total);
        }

        [TestMethod]
        public void WhenOneOfFifthBook_ThenTotalPriceIsOfFifthBook()
        {
            AddBooks(5);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(8, total);
        }

        [TestMethod]
        public void WhenTwoOfFirstBook_ThenTotalPriceIsOfFirstBookMultipledTwice()
        {
            AddBooks(1, 1);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(16, total);
        }

        [TestMethod]
        public void WhenTwoDifferentBooks_ThenFivePercentDiscountApplied()
        {
            AddBooks(1, 2);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(15.2m, total);
        }

        [TestMethod]
        public void WhenThreeDifferentBooks_ThenTenPercentDiscountApplied()
        {
            AddBooks(1, 2, 3);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(21.6m, total);
        }

        [TestMethod]
        public void WhenThreeDifferentBooks_ThenTwentyPercentDiscountApplied()
        {
            AddBooks(1, 2, 3, 4);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(25.6m, total);
        }

        [TestMethod]
        public void WhenFiveDifferentBooks_ThenTwentyFivePercentDiscountApplied()
        {
            AddBooks(1, 2, 3, 4, 5);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(30, total);
        }

        [TestMethod]
        public void WhenThreeBooksWithTwoDifferent_ThenFivePercentDiscountAppliedOnTwoOfThem()
        {
            AddBooks(1, 2, 1);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(23.2m, total);
        }

        [TestMethod]
        public void WhenManyCopiesOfDifferentBooks_ThenMultipleDiscountsApplied()
        {
            AddBooks(1, 1, 2, 2, 3, 3, 4, 5);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(51.6m, total);
        }

        [TestMethod]
        public void WhenManyCopiesOfDifferentBooksInAnyOrder_ThenMultipleDiscountsAppliedCorrectly()
        {
            AddBooks(5, 2, 1, 3, 2, 4, 1, 3);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(51.6m, total);
        }

        [TestMethod]
        public void WhenMultipleIterationsOfDiscount_ThenSameDiscountAppliedMultipleTimes()
        {
            AddBooks(1, 2, 3, 4, 5, 5, 4, 3, 2, 1, 6);

            var total = shoppingBasket.GetTotal();

            Assert.AreEqual(68, total);
        }

        private void AddBooks(params int[] bookNumbers)
        {
            foreach (var bookNumber in bookNumbers)
            {
                shoppingBasket.AddBook(new Book { ISBN = bookNumber.ToString(), Price = 8 });
            }
        }

        private static DiscountDictionary GetDiscounts()
        {
            var discounts = new DiscountDictionary();
            
            discounts.Add(0.05M, 2);
            discounts.Add(0.1M, 3);
            discounts.Add(0.2M, 4);
            discounts.Add(0.25M, 5);

            return discounts;
        }
    }
}