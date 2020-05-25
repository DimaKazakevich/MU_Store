using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Basket
    {
        private List<BasketLine> lineCollection = new List<BasketLine>();

        public void AddItem(Product wear, int quantity, string size)
        {
            BasketLine line = lineCollection
                .Where(item => item.Product.Article == wear.Article)
                .FirstOrDefault();

            if (line == null )
            {
                lineCollection.Add(new BasketLine
                {
                    Product = wear,
                    Quantity = quantity,
                    Size = size
                });
            }
            else
            {
                if (lineCollection.Where(x=>x.Product.Article == wear.Article).Select(x=>x.Size).Contains(size))
                {
                    line.Quantity += quantity;
                }
                else
                {
                    lineCollection.Add(new BasketLine
                    {
                        Product = wear,
                        Quantity = quantity,
                        Size = size
                    });
                }
            }
        }

        public void AddItem(Product wear, int quantity)
        {
            BasketLine line = lineCollection
                .Where(item => item.Product.Article == wear.Article)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new BasketLine
                {
                    Product = wear,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product wear)
        {
            lineCollection.RemoveAll(line => line.Product.Article == wear.Article);
        }

        public void RemoveLine(Product wear, string size)
        {
            lineCollection.RemoveAll(line => line.Product.Article == wear.Article && line.Size == size);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(item => item.Product.Price * item.Quantity);

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
