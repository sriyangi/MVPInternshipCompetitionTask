using MarsQACompetition.Helpers;
using MarsQACompetition.Model;
using MarsQACompetition.Pages;
using MarsQACompetition.Utils;

namespace MarsQACompetition.Tests
{
    //[TestFixture]
    public class EducationTests: TestHelper
    {
        EducationPage educationPage;

        public EducationTests()
        {
            educationPage = new EducationPage();
            TestHelper.InitExtentReports("EducationTest");
        }

        [Test, Order(1), Description("This test logs into the system")]

        public void Login()
        {
            StartExtentTest("Login/Click Education Tab Test: " + TestContext.CurrentContext.Test.Name);
            try
            {
                LoginPage.LoginSteps();
                Thread.Sleep(2000);
                educationPage.GoToEducationTab();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Login/ Education Tab Click failed: " + ex.Message);
            }
        }

        [Test, Order(2)]
        public void deleteAllEducationRecords()
        {
            StartExtentTest("Education deletion for all records Test: " + TestContext.CurrentContext.Test.Name);
            try
            {
                educationPage.GoToEducationTab();
                educationPage.DeleteAllRecords();
                Thread.Sleep(2000);
                educationPage.AssertEmptyEducations();
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Education record deletion for all data failed: " + ex.Message);
            }
        }


        [Test, Order(3), TestCaseSource(typeof(JsonReader<EducationModel>), nameof(JsonReader<EducationModel>.TestCases), new object[] { "EducationCreation" })]
        public void createEducationRecord(EducationModel parameter)
        {
            StartExtentTest("Education Creation Test: " + TestContext.CurrentContext.Test.Name + "<br /> Parameters: " + parameter.universityName + ", " + parameter.degree + ", " + parameter.title + ", " + parameter.country + ", " + parameter.graduationYear);
            try
            {
                educationPage.CreateRecord(parameter.universityName, parameter.degree, parameter.title, parameter.country, parameter.graduationYear);
                educationPage.AssertInsertedRecord();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Education record creation failed: " + ex.Message);
            }
        }

        [Test, Order(4), TestCaseSource(typeof(JsonReader<EducationModel>), nameof(JsonReader<EducationModel>.TestCases), new object[] { "EducationDuplicate" })]
        public void duplicateEducationRecord(EducationModel parameter)
        {
            //Create record
            StartExtentTest("Educcation Duplication Test (1st Step-Create Record): " + TestContext.CurrentContext.Test.Name);
            try
            {
                educationPage.CreateRecord(parameter.universityName, parameter.degree, parameter.title, parameter.country, parameter.graduationYear);
                educationPage.AssertInsertedRecord();
                CloseTest();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Education record creation failed in duplication test case (1st Step-Create Record) : " + ex.Message);
            }

            //Dplicate the same record
            StartExtentTest("Education Duplication Test (2nd Step-Duplicate Record): " + TestContext.CurrentContext.Test.Name);
            try
            {
                educationPage.CreateRecord(parameter.universityName, parameter.degree, parameter.title, parameter.country, parameter.graduationYear);
                educationPage.AssertDuplicatedRecord();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Education record creation failed in duplication test case (2nd Step-Duplicate Record) : " + ex.Message);
            }
        }

        [Test, Order(5), TestCaseSource(typeof(JsonReader<EducationEditModel>), nameof(JsonReader<EducationEditModel>.TestCases), new object[] { "EducationEdit" })]
        public void editEducationRecord(EducationEditModel parameter)
        {
            StartExtentTest("Certification Edit Test: " + TestContext.CurrentContext.Test.Name);
            try
            {
                if (educationPage.CheckforExistingRecord(parameter.oldUniversityName, parameter.oldDegree, parameter.oldTitle, parameter.oldCountry, parameter.oldGraduationYear))
                {
                    educationPage.EditRecord(parameter.universityName, parameter.degree, parameter.title, parameter.country, parameter.graduationYear, parameter.oldUniversityName, parameter.oldDegree, parameter.oldTitle, parameter.oldCountry, parameter.oldGraduationYear);
                    educationPage.AssertUpdatedRecord();
                    Thread.Sleep(2000);
                }
                else
                {
                    Assert.Fail("Education Record: " + parameter.oldUniversityName + " do not exist.");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Education record edit failed: " + ex.Message);
            }
        }

        [Test, Order(6), TestCaseSource(typeof(JsonReader<EducationModel>), nameof(JsonReader<EducationModel>.TestCases), new object[] { "EducationDelete" })]
        public void deleteEducationRecord(EducationModel parameter)
        {
            StartExtentTest("Education Delete Test: " + TestContext.CurrentContext.Test.Name);
            try
            {
                if (educationPage.CheckforExistingRecord(parameter.universityName, parameter.degree, parameter.title, parameter.country, parameter.graduationYear))
                {
                    educationPage.DeletRecord(parameter.universityName, parameter.degree, parameter.title, parameter.country, parameter.graduationYear);
                    educationPage.AssertDeletedRecord();
                    Thread.Sleep(2000);
                }
                else
                {
                    Assert.Fail("Education Record: " + parameter.universityName + " do not exist.");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Education record delete failed: " + ex.Message);
            }
        }

        [Test, Order(7), TestCaseSource(typeof(JsonReader<EducationModel>), nameof(JsonReader<EducationModel>.TestCases), new object[] { "EducationIncompleteCreate" })]
        public void createEducationRecordWithIncompleteData(EducationModel parameter)
        {
            StartExtentTest("Education Creation with Incomplete Data Test: " + TestContext.CurrentContext.Test.Name + "<br /> Parameters: " + parameter.universityName + ", " + parameter.degree + ", " + parameter.title + ", " + parameter.country + ", " + parameter.graduationYear);
            try
            {
                educationPage.CreateRecord(parameter.universityName, parameter.degree, parameter.title, parameter.country, parameter.graduationYear);
                educationPage.AssertIncompleteRecord();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Education record creation with Incomplete Data failed: " + ex.Message);
            }

        }

        //Write Report
        [TearDown]
        public static void CloseTest()
        {
            TestHelper.LoggingTestStatusExtentReport();
        }
    }
}