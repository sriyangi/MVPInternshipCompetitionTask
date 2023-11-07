using MarsQACompetition.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MarsQACompetition.Pages
{
    public class CertificationPage : Driver
    {
        private IWebElement certificateTab;
        private IWebElement certificateTable;
        private IWebElement addNewButton;
        private IWebElement certificateTextbox;
        private IWebElement certificateFromTextbox;
        private IWebElement certificateYearDropdownSelect;
        private IWebElement certificateYearDropdown;
        private IWebElement certificateAddButton;
        private IWebElement certificateUpdateButton;
        private IWebElement certificateCancelButton;
        private IWebElement messageWindow;
        private IWebElement closeMessageIcon;

        #region Render Elements
        public void renderAddElements()
        {
            certificateTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Certificate or Award']"));
            certificateFromTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Certified From (e.g. Adobe)']"));
            certificateYearDropdownSelect = driver.FindElement(By.XPath("//select"));
            certificateAddButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Add']"));
            certificateCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));
        }

        public void renderEditlements()
        {
            certificateTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Certificate or Award']"));
            certificateFromTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Certified From (e.g. Adobe)']"));
            certificateYearDropdownSelect = driver.FindElement(By.XPath("//select"));
            certificateUpdateButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Update']"));
            certificateCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));
        }
        #endregion

        #region Page Data Manipulation

        //Clear State
        public void DeleteAllRecords()
        {
            IWebElement record = returnRecord();

            while (record != null)
            {
                record.Click();
                try
                {
                    Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
                    messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
                    closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));
                    closeMessageIcon.Click();
                    Thread.Sleep(1000);
                }
                catch
                {

                }
                record = returnRecord();
            }
        }

        //Create Certification Record
        public void CreateRecord(string certificationName, string certificationBody, string graduationYear)
        {
            string graduationYearlXpath = "//option[contains(text(),'" + graduationYear + "')]";

            //If Cancel button is visible, first click it
            if (IsCancelCertificationBtnVisible()) certificateCancelButton.Click();

            addNewButton.Click();
            renderAddElements();

            certificateTextbox.SendKeys(certificationName);
            certificateFromTextbox.SendKeys(certificationBody);
            certificateYearDropdownSelect.Click();
            certificateYearDropdown = driver.FindElement(By.XPath(graduationYearlXpath));
            certificateYearDropdown.Click();
            certificateAddButton.Click();
        }

        //Edit Certification Record
        public void EditRecord(string certificationName, string certificationBody, string graduationYear, string oldCertificationName, string oldCertificationBody, string oldGraduationYear)
        {
            string graduationYearlXpath = "//option[contains(text(),'" + graduationYear + "')]";

            //If Cancel button is visible, first click it
            if (IsCancelCertificationBtnVisible()) certificateCancelButton.Click();

            GetEditDeletButtonElement(true, oldCertificationName, oldCertificationBody, oldGraduationYear).Click();
            renderEditlements();

            certificateTextbox.Clear();
            certificateTextbox.SendKeys(certificationName);
            certificateFromTextbox.Clear();
            certificateFromTextbox.SendKeys(certificationBody);
            certificateYearDropdownSelect.Click();
            certificateYearDropdown = driver.FindElement(By.XPath(graduationYearlXpath));
            certificateYearDropdown.Click();
            certificateUpdateButton.Click();
        }

        //Delete Certificate Record
        public void DeletRecord(string certificationName, string certificationBody, string graduationYear)
        {
            //If Cancel button is visible, first click it
            if (IsCancelCertificationBtnVisible()) certificateCancelButton.Click();

            GetEditDeletButtonElement(false, certificationName, certificationBody, graduationYear).Click();
        }

        #endregion

        #region Supporting Methods

        //Click Skills Tab
        public void GoToCertificationTab()
        {
            certificateTab = driver.FindElement(By.XPath("//a[contains(text(),'Certifications')]"));
            certificateTab.Click();
            certificateTable = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table"));
            addNewButton = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/thead/tr/th[4]/div"));
        }

        //Common method to check cancel button is visible
        public bool IsCancelCertificationBtnVisible()
        {
            try
            {
                certificateCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool CheckforExistingRecord(string certificate, string certificateFrom, string graduateYear)
        {
            IList<IWebElement> tbodyCollection = certificateTable.FindElements(By.TagName("tbody"));
            IList<IWebElement> trCollection;
            IList<IWebElement> tdCollection;

            //loop every row in the table and init the columns to list
            foreach (IWebElement tbodyElement in tbodyCollection)
            {
                trCollection = tbodyElement.FindElements(By.TagName("tr"));

                foreach (IWebElement trElement in trCollection)
                {
                    tdCollection = trElement.FindElements(By.TagName("td"));
                    if (tdCollection[0].Text == certificate && tdCollection[1].Text == certificateFrom && tdCollection[2].Text == graduateYear)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Common method to return Edit or Delete button depending on the parameter
        public IWebElement GetEditDeletButtonElement(bool isEdit, string certificate, string certificateFrom, string graduateYear)
        {
            IList<IWebElement> tbodyCollection = certificateTable.FindElements(By.TagName("tbody"));
            IList<IWebElement> trCollection;
            IList<IWebElement> tdCollection;
            IList<IWebElement> spanCollection;

            //loop every row in the table and init the columns to list
            foreach (IWebElement tbodyElement in tbodyCollection)
            {
                trCollection = tbodyElement.FindElements(By.TagName("tr"));

                foreach (IWebElement trElement in trCollection)
                {
                    tdCollection = trElement.FindElements(By.TagName("td"));
                    if (tdCollection[0].Text == certificate && tdCollection[1].Text == certificateFrom && tdCollection[2].Text == graduateYear)
                    {
                        spanCollection = tdCollection[3].FindElements(By.TagName("span"));
                        if (isEdit) return spanCollection[0];
                        else return spanCollection[1];
                    }
                }
            }
            return null;
        }

        //Get record count in the grid
        public int GetRecordCount()
        {
            IList<IWebElement> tbodyCollection = certificateTable.FindElements(By.TagName("tbody"));
            return tbodyCollection.Count;
        }

        public IWebElement returnRecord()
        {
            IList<IWebElement> tbodyCollection = certificateTable.FindElements(By.TagName("tbody"));
            IList<IWebElement> trCollection;
            IList<IWebElement> tdCollection;
            IList<IWebElement> spanCollection;

            try
            {
                //loop every row in the table and init the columns to list
                foreach (IWebElement tbodyElement in tbodyCollection)
                {
                    trCollection = tbodyElement.FindElements(By.TagName("tr"));

                    foreach (IWebElement trElement in trCollection)
                    {
                        tdCollection = trElement.FindElements(By.TagName("td"));
                        spanCollection = tdCollection[3].FindElements(By.TagName("span"));
                        return spanCollection[1];
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        #endregion

        #region Assertions

        public void AssertInsertedRecord(string certificationName)
        {
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelCertificationBtnVisible())
            {
                certificateCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo(certificationName + " has been added to your certification"), "succes message is not correct for add certificate");
        }

        public void AssertDuplicatedRecord(string certificationName)
        {
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelCertificationBtnVisible())
            {
                certificateCancelButton.Click();
            }

            Assert.That(message == "This information is already exist." || message == "Duplicated data", "succes message is not correct for duplicated certificate");
        }

        public void AssertUpdatedRecord(string certificationName)
        {
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelCertificationBtnVisible())
            {
                certificateCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo(certificationName + " has been updated to your certification"), "succes message is not correct for update certificate");
        }

        public void AssertIncompleteRecord()
        {
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));

            string message = messageWindow.Text;

            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelCertificationBtnVisible())
            {
                certificateCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo("Please enter Certification Name, Certification From and Certification Year"), "succes message is not correct for certificate record with partial data");
        }

        public void AssertDeletedRecord(string certificationName)
        {
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelCertificationBtnVisible())
            {
                certificateCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo(certificationName + " has been deleted from your certification"), "succes message is not correct for delete certificate");
        }

        public void AssertEmptyCertifications()
        {
            Assert.That(0, Is.EqualTo(GetRecordCount()), "succes message is not correct for delete all certificates");
        }

        #endregion

    }
}
