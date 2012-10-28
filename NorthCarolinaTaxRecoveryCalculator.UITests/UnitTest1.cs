using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using HtmlAgilityPack;
using NorthCarolinaTaxRecoveryCalculator;

namespace NorthCarolinaTaxRecoveryCalculator.UITests
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void TestFixtureSetUp()
        {
            
        }

        [TestMethod]
        public void Root_Url_Renders_Index_View()
        {
            var view = new Index();

            // Set up the data that needs to be accessed by the view
            view.ViewBag.Message = "Testing";

            // Render it in an HtmlAgilityPack HtmlDocument. Note that
            // you can pass a 'model' object here if your view needs one.
            // Generally, what you do here is similar to how a controller
            //action sets up data for its view.
            HtmlDocument doc = view.RenderAsHtml();

            // Use the HtmlAgilityPack object model to verify the view.
            // Here, we simply check that the first <h2> tag contains
            // what we put in view.ViewBag.Message
            HtmlNode node = doc.DocumentNode.Element("h2");
            Console.WriteLine(doc.ToString());
            Assert.AreEqual("Testing", node.InnerHtml.Trim());
        }
    }
}
