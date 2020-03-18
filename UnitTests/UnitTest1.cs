using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.HtmlHelpers;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPaginate()
        {
            Mock<IClothesRepository> mock = new Mock<IClothesRepository>();

            mock.Setup(item => item.Clothes).Returns(new List<Wear>
            {
                new Wear { Article = 1, Name = "first t-short" },
                new Wear { Article = 2, Name = "second t-short" },
                new Wear { Article = 3, Name = "third t-short" },
                new Wear { Article = 4, Name = "fourth t-short" }
            });

            ClothesController controller = new ClothesController(mock.Object)
            {
                _pageSize = 2
            };

            IEnumerable<Wear> result = (IEnumerable<Wear>)controller.List(2).Model;

            List<Wear> clothes = result.ToList();

            Assert.IsTrue(clothes.Count == 2);
            Assert.AreEqual(clothes[0].Name, "third t-short");
            Assert.AreEqual(clothes[1].Name, "fourth t-short");
        }

        [TestMethod]
        public void TestGenerPageLinks()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 4,
                ItemsOnPage = 2
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>",
                result.ToString());

            //Сбой Assert.AreEqual. 
        //Ожидается: <<a class="btn btn-default" href="Page1">1</a><a class="btn btn-default btn-primary selected" href="Page2">2</a>>. 
        //Фактически: <<a class="btn btn-default btn-primary selected" href="Page1">1</a><a class="btn btn-default" href="Page2">2</a>>. 
        }
    }
}