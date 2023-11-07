using MarsQACompetition.Helpers;
using MarsQACompetition.Model;
using MarsQACompetition.Pages;
using MarsQACompetition.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using MarsQACompetition.Helpers;

namespace MarsQACompetition.Tests
{
    [TestFixture]
    public class CertificationTests : TestHelper
    {
        CertificationPage certificationPage;

        public CertificationTests()
        {
            certificationPage = new CertificationPage();
            TestHelper.InitExtentReports("CertificationTest");
        }
               
 
        [Test, Order(1), Description("This test logs into the system")]
        public void Login()
        {
            TestHelper.StartExtentTest("Login/Click Certification Tab Test: " + TestContext.CurrentContext.Test.Name);
            try
            {
                LoginPage.LoginSteps();
                certificationPage.GoToCertificationTab();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Login/ Certification Tab Click failed: " + ex.Message);
            }
        }

        [Test, Order(2)]
        public void deleteAllCertificateRecords()
        {
            TestHelper.StartExtentTest("Certification deletion for all records Test: " + TestContext.CurrentContext.Test.Name);
            try
            {
                certificationPage.GoToCertificationTab();
                certificationPage.DeleteAllRecords();
                Thread.Sleep(2000);
                certificationPage.AssertEmptyCertifications();
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Certification record deletion for all data failed: " + ex.Message);
            }
        }


        [Test, Order(3), TestCaseSource(typeof(JsonReader<CertificationModel>), nameof(JsonReader<CertificationModel>.TestCases), new object[] { "CertificcationCreation" })]
        public void createCertificationRecord(CertificationModel parameter)
        {
            StartExtentTest("Certification Creation Test: "+TestContext.CurrentContext.Test.Name+ "<br /> Parameters: " + parameter.certificationName +", " + parameter.certificationBody + ", " + parameter.graduationYear);
            try
            {
                certificationPage.CreateRecord(parameter.certificationName, parameter.certificationBody, parameter.graduationYear);
                certificationPage.AssertInsertedRecord(parameter.certificationName);
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Certification record creation failed: " + ex.Message);
            }           
        }

        [Test, Order(4), TestCaseSource(typeof(JsonReader<CertificationModel>), nameof(JsonReader<CertificationModel>.TestCases), new object[] { "CertificcationDuplicate" })]
        public void duplicateCertificationRecord(CertificationModel parameter)
        {
            //Create record
            StartExtentTest("Certification Duplication Test (1st Step-Create Record): " + TestContext.CurrentContext.Test.Name);
            try
            {
                certificationPage.CreateRecord(parameter.certificationName, parameter.certificationBody, parameter.graduationYear);
                certificationPage.AssertInsertedRecord(parameter.certificationName);
                CloseTest();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Certification record creation failed in duplication test case (1st Step-Create Record) : " + ex.Message);
            }

            //Dplicate the same record
            StartExtentTest("Certification Duplication Test (2nd Step-Duplicate Record): " + TestContext.CurrentContext.Test.Name);
            try
            {
                certificationPage.CreateRecord(parameter.certificationName, parameter.certificationBody, parameter.graduationYear);
                certificationPage.AssertDuplicatedRecord(parameter.certificationName);
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Certification record creation failed in duplication test case (2nd Step-Duplicate Record) : " + ex.Message);
            }
        }

        [Test, Order(5), TestCaseSource(typeof(JsonReader<CertificationEditModel>), nameof(JsonReader<CertificationEditModel>.TestCases), new object[] { "CertificcationEdit" })]
        public void editCertificationRecord(CertificationEditModel parameter)
        {
            StartExtentTest("Certification Edit Test: " + TestContext.CurrentContext.Test.Name);
            try
            {
                if (certificationPage.CheckforExistingRecord(parameter.oldCertificationName, parameter.oldCertificationBody, parameter.oldGraduationYear))
                {
                    certificationPage.EditRecord(parameter.certificationName, parameter.certificationBody, parameter.graduationYear, parameter.oldCertificationName, parameter.oldCertificationBody, parameter.oldGraduationYear);
                    certificationPage.AssertUpdatedRecord(parameter.certificationName);
                    Thread.Sleep(2000);
                }
                else
                {
                    Assert.Fail("Certifiacte Record: " + parameter.oldCertificationName + " do not exist.");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Certification record edit failed: " + ex.Message);
            }
        }

        [Test, Order(6), TestCaseSource(typeof(JsonReader<CertificationModel>), nameof(JsonReader<CertificationModel>.TestCases), new object[] { "CertificcationDelete" })]
        public void deleteCertificationRecord(CertificationModel parameter)
        {
            StartExtentTest("Certification Delete Test: " + TestContext.CurrentContext.Test.Name);
            try
            {
                if (certificationPage.CheckforExistingRecord(parameter.certificationName, parameter.certificationBody, parameter.graduationYear))
                {
                    certificationPage.DeletRecord (parameter.certificationName, parameter.certificationBody, parameter.graduationYear);
                    certificationPage.AssertDeletedRecord(parameter.certificationName);
                    Thread.Sleep(2000);
                }
                else
                {
                    Assert.Fail("Certifiacte Record: " + parameter.certificationName + " do not exist.");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Certification record delete failed: " + ex.Message);
            }
        }

        [Test, Order(7), TestCaseSource(typeof(JsonReader<CertificationModel>), nameof(JsonReader<CertificationModel>.TestCases), new object[] { "CertificcationIncompleteCreate" })]
        public void createCertificationRecordWithIncompleteData(CertificationModel parameter)
        {
            StartExtentTest("Certification Creation with Incomplete Data Test: " + TestContext.CurrentContext.Test.Name + "<br /> Parameters: " + parameter.certificationName + ", " + parameter.certificationBody + ", " + parameter.graduationYear);
            try
            {
                certificationPage.CreateRecord(parameter.certificationName, parameter.certificationBody, parameter.graduationYear);
                certificationPage.AssertIncompleteRecord();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Certification record creation with Incomplete Data failed: " + ex.Message);
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
