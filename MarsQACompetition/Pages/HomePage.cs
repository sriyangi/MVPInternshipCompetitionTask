using MarsQACompetition.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQACompetition.Pages
{
    public class HomePage : Driver
    {
        public static void GoToProfilePage()
        {
            driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[1]/div/a[2]")).Click();
        }
    }
}
