using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Basket
    {
        private List<BasketLine> lineCollection = new List<BasketLine>();

        public List<BasketLine> GetBasketLines { get { return lineCollection; } }

        public void AddItem(Wear wear, int quantity)
        {
            BasketLine line = lineCollection
                .Where(item => item.Wear.Article == wear.Article)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new BasketLine
                {
                    Wear = wear,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Wear wear)
        {
            lineCollection.RemoveAll(line => line.Wear.Article == wear.Article);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(item => item.Wear.Price * item.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<BasketLine> Lines
        {
            get { return lineCollection; }
        }
    }
}
