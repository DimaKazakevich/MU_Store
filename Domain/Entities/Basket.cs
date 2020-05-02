using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Basket
    {
        private List<BasketLine> lineCollection = new List<BasketLine>();

        public void AddItem(Wear wear, int quantity, string size)
        {
            BasketLine line = lineCollection
                .Where(item => item.Wear.Article == wear.Article)
                .FirstOrDefault();

            if (line == null )
            {
                lineCollection.Add(new BasketLine
                {
                    Wear = wear,
                    Quantity = quantity,
                    Size = size
                });
            }
            else
            {
                if (lineCollection.Where(x=>x.Wear.Article == wear.Article).Select(x=>x.Size).Contains(size))
                {
                    line.Quantity += quantity;
                }
                else
                {
                    lineCollection.Add(new BasketLine
                    {
                        Wear = wear,
                        Quantity = quantity,
                        Size = size
                    });
                }
            }
        }

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

        public void RemoveLine(Wear wear, string size)
        {
            lineCollection.RemoveAll(line => line.Wear.Article == wear.Article && line.Size == size);
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
