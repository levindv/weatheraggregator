using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace WA.Grabber.PhantomJS
{
    public static class IWebElementExtensions
    {
        public static IWebElement GetInnerElement(this IWebElement node, string tagName, string attrName, string attrValue)
        {
            return node?.FindElements(By.TagName(tagName)).FirstOrDefault(tag => tag.GetAttribute(attrName) == attrValue);
        }
        public static IWebElement GetInnerElement(this IWebDriver driver, string tagName, string attrName, string attrValue)
        {
            return driver?.FindElements(By.TagName(tagName)).FirstOrDefault(tag => tag.GetAttribute(attrName) == attrValue);
        }
        public static List<IWebElement> GetInnerElements(this IWebElement node, string tagName, string attrName, string attrValue)
        {
            return node?.FindElements(By.TagName(tagName)).Where(tag => tag.GetAttribute(attrName) == attrValue).ToList();
        }
        public static string GetInnerElementAtributeValue(this IWebElement node, string tagName, string attrName)
        {
            return node?.FindElement(By.TagName(tagName)).GetAttribute(attrName);
        }
        public static string GetInnerElementText(this IWebElement node, string tagName, string attrName, string attrValue)
        {
            return node?.FindElements(By.TagName(tagName)).FirstOrDefault(tag => tag.GetAttribute(attrName) == attrValue)?.Text;
        }
        public static string GetInnerElementAtributeValue(this IWebDriver node, string tagName, string attrName)
        {
            return node?.FindElement(By.TagName(tagName)).GetAttribute(attrName);
        }
        public static IWebElement ByInd(this List<IWebElement> list, int index)
        {
            return list?.Count > index ? list[index] : null;
        }
    }
}