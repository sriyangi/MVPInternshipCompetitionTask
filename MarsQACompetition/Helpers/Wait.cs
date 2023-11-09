using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQACompetition.Helpers
{
    public class Wait
    {
        //Public variable to keep wait type when finding the IWebElement
        public enum WaitType
        {
            ToBeVisible,
            ToBeClickable
        }

        //Public variable to keep locator type when finding the IWebElement
        public enum LocatorType
        {
            XPath,
            Id
        }

        //Common method to wait till the Element is visible
        public static void WaitToBeVisible(LocatorType locatorType, string locatorValue, int seconds)
        {
            var wait = new WebDriverWait(Driver.driver, new TimeSpan(0, 0, seconds));

            if (locatorType == LocatorType.Id)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(locatorValue)));
            }
            else if (locatorType == LocatorType.XPath)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(locatorValue)));
            }
        }

        //Common method to wait till the Element is clickable
        public static void WaitToBeClickable(LocatorType locatorType, string locatorValue, int seconds)
        {
            var wait = new WebDriverWait(Driver.driver, new TimeSpan(0, 0, seconds));

            if (locatorType == LocatorType.Id)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(locatorValue)));
            }
            else if (locatorType == LocatorType.XPath)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(locatorValue)));
            }
        }

        //Common method to wait till the Element exists
        public static void WaitToBeExists(LocatorType locatorType, string locatorValue, int seconds)
        {
            var wait = new WebDriverWait(Driver.driver, new TimeSpan(0, 0, seconds));

            if (locatorType == LocatorType.Id)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id(locatorValue)));
            }
            else if (locatorType == LocatorType.XPath)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(locatorValue)));
            }
        }
    }
}
