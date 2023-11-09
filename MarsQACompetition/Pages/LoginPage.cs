using MarsQACompetition.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQACompetition.Pages
{
    public class LoginPage : Driver
    {
        private static IWebElement loginButton;
        private static IWebElement userNameTextbox;
        private static IWebElement passwordTextbox;

        public static void LoginSteps()
        {
            Driver.NavigateUrl();

            driver.FindElement(By.XPath("//a[contains(text(),'Sign In')]")).Click();
            renderPageElements();

            userNameTextbox.SendKeys(ConstantHelpers.UserName);
            passwordTextbox.SendKeys(ConstantHelpers.Password);
            loginButton.Click();
        }

        public static void renderPageElements()
        {
            loginButton = driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));
            userNameTextbox = driver.FindElement(By.XPath("(//INPUT[@type='text'])[2]"));
            passwordTextbox = driver.FindElement(By.XPath("//input[@type='password']"));
        }

    }
}
