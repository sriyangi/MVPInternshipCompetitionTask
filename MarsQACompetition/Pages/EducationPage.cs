using MarsQACompetition.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MarsQACompetition.Pages
{
    public class EducationPage:Driver
    {
        private IWebElement educationTab;
        private IWebElement educationTable;
        private IWebElement addNewButton;
        private IWebElement universityNameTextbox;
        private IWebElement degreeTextbox;
        private IWebElement graduationYearDropdownSelect;
        private IWebElement graduationYearDropdown;
        private IWebElement countryDropdownSelect;
        private IWebElement countryDropdown;
        private IWebElement titleDropdownSelect;
        private IWebElement titleDropdown;
        private IWebElement educationAddButton;
        private IWebElement educationUpdateButton;
        private IWebElement educationCancelButton;
        private IWebElement messageWindow;
        private IWebElement closeMessageIcon;

        #region Render Elements
        public void renderAddElements()
        {
            universityNameTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='College/University Name']"));
            degreeTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Degree']"));
            graduationYearDropdownSelect = driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));
            countryDropdownSelect = driver.FindElement(By.XPath("//select[@name='country']"));
            titleDropdownSelect = driver.FindElement(By.XPath("//select[@name='title']"));
            educationAddButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Add']"));
            educationCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));
        }

        public void renderEditlements()
        {
            universityNameTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='College/University Name']"));
            degreeTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Degree']"));
            graduationYearDropdownSelect = driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));
            countryDropdownSelect = driver.FindElement(By.XPath("//select[@name='country']"));
            titleDropdownSelect = driver.FindElement(By.XPath("//select[@name='title']"));
            educationUpdateButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Update']"));
            educationCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));
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

        public IWebElement returnRecord()
        {
            IList<IWebElement> tbodyCollection = educationTable.FindElements(By.TagName("tbody"));
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
                        spanCollection = tdCollection[5].FindElements(By.TagName("span"));
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

        //Create Certification Record
        public void CreateRecord(string university, string degree, string title, string country, string graduationYear)
        {
            string titleXpath = "//option[contains(text(),'" + title + "')]";
            string countryXpath = "//option[contains(text(),'" + country + "')]";
            string graduationYearlXpath = "//option[contains(text(),'" + graduationYear + "')]";

            //If Cancel button is visible, first click it
            if (IsCancelEducationBtnVisible()) educationCancelButton.Click();

            addNewButton.Click();
            renderAddElements();

            universityNameTextbox.SendKeys(university);
            degreeTextbox.SendKeys(degree);
            titleDropdownSelect.Click();
            titleDropdown = driver.FindElement(By.XPath(titleXpath));
            titleDropdown.Click();
            countryDropdownSelect.Click();
            countryDropdown = driver.FindElement(By.XPath(countryXpath));
            countryDropdown.Click();
            graduationYearDropdownSelect.Click();
            graduationYearDropdown = driver.FindElement(By.XPath(graduationYearlXpath));
            graduationYearDropdown.Click();
            educationAddButton.Click();
        }

        //Edit Certification Record
        public void EditRecord(string university, string degree, string title, string country, string graduationYear, string oldUniversity, string oldDegree, string oldTitle, string oldCountry, string oldGraduationYear)
        {
            string titleXpath = "//option[contains(text(),'" + title + "')]";
            string countryXpath = "//option[contains(text(),'" + country + "')]";
            string graduationYearlXpath = "//option[contains(text(),'" + graduationYear + "')]";

            //If Cancel button is visible, first click it
            if (IsCancelEducationBtnVisible()) educationCancelButton.Click();

            GetEditDeletButtonElement(true, oldUniversity,oldDegree,oldTitle,oldCountry,oldGraduationYear ).Click();
            renderEditlements();

            universityNameTextbox.Clear();
            universityNameTextbox.SendKeys(university);
            degreeTextbox.Clear();
            degreeTextbox.SendKeys(degree);
            titleDropdownSelect.Click();
            titleDropdown = driver.FindElement(By.XPath(titleXpath));
            titleDropdown.Click();
            countryDropdownSelect.Click();
            countryDropdown = driver.FindElement(By.XPath(countryXpath));
            countryDropdown.Click();
            graduationYearDropdownSelect.Click();
            graduationYearDropdown = driver.FindElement(By.XPath(graduationYearlXpath));
            graduationYearDropdown.Click();
            educationUpdateButton.Click();
        }

        //Delete Education Record
        public void DeletRecord(string university, string degree, string title, string country, string graduationYear)
        {
            //If Cancel button is visible, first click it
            if (IsCancelEducationBtnVisible()) educationCancelButton.Click();

            GetEditDeletButtonElement(false, university, degree, title, country,graduationYear).Click();
        }

        #endregion 

        #region Supporting Methods

        //Click Skills Tab
        public void GoToEducationTab()
        {
            educationTab = driver.FindElement(By.XPath("//a[contains(text(),'Education')]"));
            educationTab.Click();
            educationTable = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table"));
            addNewButton = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div"));
        }

        //Common method to check cancel button is visible
        public bool IsCancelEducationBtnVisible()
        {
            try
            {
                educationCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool CheckforExistingRecord(string university, string degree, string title, string country, string graduationYear)
        {
            IList<IWebElement> tbodyCollection = educationTable.FindElements(By.TagName("tbody"));
            IList<IWebElement> trCollection;
            IList<IWebElement> tdCollection;

            //loop every row in the table and init the columns to list
            foreach (IWebElement tbodyElement in tbodyCollection)
            {
                trCollection = tbodyElement.FindElements(By.TagName("tr"));

                foreach (IWebElement trElement in trCollection)
                {
                    tdCollection = trElement.FindElements(By.TagName("td"));
                    if (tdCollection[0].Text == country && tdCollection[1].Text == university && tdCollection[2].Text == title && tdCollection[3].Text == degree && tdCollection[4].Text == graduationYear)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Common method to return Edit or Delete button depending on the parameter
        public IWebElement GetEditDeletButtonElement(bool isEdit, string university, string degree, string title, string country, string graduationYear)
        {
            IList<IWebElement> tbodyCollection = educationTable.FindElements(By.TagName("tbody"));
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
                    if (tdCollection[0].Text == country && tdCollection[1].Text == university && tdCollection[2].Text == title && tdCollection[3].Text == degree && tdCollection[4].Text == graduationYear)
                    {
                        spanCollection = tdCollection[5].FindElements(By.TagName("span"));
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
            IList<IWebElement> tbodyCollection = educationTable.FindElements(By.TagName("tbody"));
            return tbodyCollection.Count;
        }

        #endregion

        #region Assertions

        public void AssertEmptyEducations()
        {
            Assert.That(0, Is.EqualTo(GetRecordCount()), "succes message is not correct for delete all educations");
        }

        public void AssertInsertedRecord()
        {
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelEducationBtnVisible())
            {
                educationCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo("Education has been added"), "succes message is not correct for add educcation");
        }

        public void AssertDuplicatedRecord()
        {
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelEducationBtnVisible())
            {
                educationCancelButton.Click();
            }

            Assert.That(message == "This information is already exist.", "succes message is not correct for duplicated education");
        }

        public void AssertUpdatedRecord()
        {
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelEducationBtnVisible())
            {
                educationCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo("Education as been updated"), "succes message is not correct for update educcation");
        }

        public void AssertDeletedRecord()
        {
            Wait.WaitToBeExists(Wait.LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelEducationBtnVisible())
            {
                educationCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo("Education entry successfully removed"), "succes message is not correct for delete education");
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

            if (IsCancelEducationBtnVisible())
            {
                educationCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo("Please enter all the fields"), "succes message is not correct for education record with partial data");
        }

        #endregion
    }
}
