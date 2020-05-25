using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private IEnumerable<Product> _products;
        public EmailOrderProcessor(IProductUnitOfWork products)
        {
            _products = products.Products.GetAll().ToList();
        }

        public void ProcessOrder(string email, IEnumerable<OrderDetails> orderDetails)
        {
            string details = string.Empty;

            foreach (var item in orderDetails)
            {
                details += "<p><br> Product: " + _products.Where(x => x.Article == item.ProductId).Select(p => p.Name).First() + "</br>";
                details += "<br> Size: " + item.Size + "</br>";
                details += "<br> Quantity: " + item.Quantity + "</br></p>";
            }

            var from = "uniteddirectonlinestore@gmail.com";
            var pass = "UDS2077563";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;

            var mail = new MailMessage(from, email);
            mail.Subject = "Delivery";
            string htmlString = $@"<html>
                      <body>
                      <p>Hi, {email}</p>
                      <p>Your order has just been sent and will be with you in 6-7 business days.</p>
                      {details}
                      <p>With best regards, United Direct!</p>
                      </body>
                      </html>";
            mail.IsBodyHtml = true;
            mail.Body = htmlString;
            client.SendMailAsync(mail);
        }
    }
}