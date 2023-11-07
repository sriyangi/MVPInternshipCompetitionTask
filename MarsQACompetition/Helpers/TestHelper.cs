using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter.Config;
using AventStack.ExtentReports.Reporter;

namespace MarsQACompetition.Helpers
{
    public  class TestHelper:Driver
    {
        public static ExtentReports extentReports;
        public static ExtentTest testlog;

        public static void InitExtentReports(string fileName)
        {
            string projectPath = TestHelper.GetProjectPath();

            string reportPath = projectPath + ConstantHelpers.TestReportFolderName + "\\";

            if (!(Directory.Exists(reportPath))) Directory.CreateDirectory(reportPath);

            string reportPathwithFileName = Path.Combine(reportPath, fileName + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".html");
            ExtentSparkReporter htmlReporter = new ExtentSparkReporter(reportPathwithFileName);
            extentReports = new ExtentReports();
            htmlReporter.Config.Theme = Theme.Standard;
            extentReports.AttachReporter(htmlReporter);
        }

        public static void FlushExtentReport()
        {
            extentReports.Flush();
        }

        public static void StartExtentTest(string testsToStart)
        {
            testlog = extentReports.CreateTest(testsToStart);
        }

        public static Media CaptureScreenShot(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot iTakesScreenshot = (ITakesScreenshot)driver;
            var screenshot = iTakesScreenshot.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

        public static void LoggingTestStatusExtentReport()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = string.Empty + TestContext.CurrentContext.Result.StackTrace + string.Empty;
                var errorMessage = TestContext.CurrentContext.Result.Message;
                Status logstatus;
                string fileName = "Screenshot_" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".png";

                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        testlog.Log(Status.Fail, "Test steps NOT Completed for Test case " + TestContext.CurrentContext.Test.Name + " ");
                        testlog.Log(Status.Fail, "Test ended with " + Status.Fail + " – " + errorMessage);
                        testlog.Log(logstatus, "Test ended with" + logstatus + stacktrace);

                        //Capture Screen Shot
                        var mediaEntity = CaptureScreenShot(driver, fileName);
                        testlog.Fail("Screen Shot: ", mediaEntity);

                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        testlog.Log(Status.Skip, "Test ended with " + Status.Skip);
                        break;
                    default:
                        logstatus = Status.Pass;
                        testlog.Log(Status.Pass, "Test steps finished for test case " + TestContext.CurrentContext.Test.Name);
                        testlog.Log(Status.Pass, "Test ended with " + Status.Pass);
                        break;
                }
            }
            catch (WebDriverException ex)
            {
                throw ex;
            }
        }

        public static string GetProjectPath()
        {
            string path = Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            return projectPath;
        }
    }
}
