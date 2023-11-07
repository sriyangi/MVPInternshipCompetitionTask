using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQACompetition.Helpers
{
    public class ConstantHelpers
    {
        //Base Url
        public static string Url = "http://localhost:5000";
        //User Name
        public static string UserName = "sriyangi@yahoo.com";
        //Password
        public static string Password = "sri123";
        //Test Report folder name
        public static string TestReportFolderName = "Reports";

        //json test data folder name
        public static string jsonTestDataFolderName = "TestLibrary";

        //json file names for certificcate test data
        public static string fileNameCertificationTestDataforCreation = "CertificationTestDataforCreation.json";
        public static string fileNameCertificationTestDataforDuplicate = "CertificationTestDataforDuplicate.json";
        public static string fileNameCertificationTestDataforEdit = "CertificationTestDataforEdit.json";
        public static string fileNameCertificationTestDataforDelete = "CertificationTestDataforDelete.json";
        public static string fileNameCertificationTestDataforIncompleteCreation = "CertificationTestDataforIncompleteCreation.json";

        //json file names for education test data
        public static string fileNameEducationTestDataforCreation = "EducationTestDataforCreation.json";
        public static string fileNameEducationTestDataforDuplicate = "EducationTestDataforDuplicate.json";
        public static string fileNameEducationTestDataforEdit = "EducationTestDataforEdit.json";
        public static string fileNameEducationTestDataforDelete = "EducationTestDataforDelete.json";
        public static string fileNameEducationTestDataforIncompleteCreation = "EducationTestDataforIncompleteCreation.json";
    }
}
