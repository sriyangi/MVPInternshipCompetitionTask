using MarsQACompetition.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsQACompetition.Tests;
using System.Reflection;
using MarsQACompetition.Helpers;

namespace MarsQACompetition.Utils
{
    public  class JsonReader<T>
    {
        private static string GetFilePath(string fileName)
        {
            string projectPath = TestHelper.GetProjectPath();

            string reportPath = projectPath+ConstantHelpers.jsonTestDataFolderName + "\\" + $"{fileName}";

            return reportPath;
        }

        public static IEnumerable<T> TestCases(string testCaseType)
        {
            string fileName = "";

            if (testCaseType == "CertificcationCreation")
            {
                fileName = ConstantHelpers.fileNameCertificationTestDataforCreation;
            }
            else if (testCaseType == "CertificcationDuplicate")
            {
                fileName = ConstantHelpers.fileNameCertificationTestDataforDuplicate;
            }
            else if (testCaseType == "CertificcationEdit")
            {
                fileName = ConstantHelpers.fileNameCertificationTestDataforEdit;
            }
            else if (testCaseType == "CertificcationDelete")
            {
                fileName = ConstantHelpers.fileNameCertificationTestDataforDelete;
            }
            else if (testCaseType == "CertificcationIncompleteCreate")
            {
                fileName = ConstantHelpers.fileNameCertificationTestDataforIncompleteCreation;
            }
            else if (testCaseType == "CertificcationWithTooManyDataCreate")
            {
                fileName = ConstantHelpers.fileNameCertificationTestDataWithTooManyCharacters;
            }
            else if (testCaseType == "EducationCreation")
            {
                fileName = ConstantHelpers.fileNameEducationTestDataforCreation;
            }
            else if (testCaseType == "EducationDuplicate")
            {
                fileName = ConstantHelpers.fileNameEducationTestDataforDuplicate;
            }
            else if (testCaseType == "EducationEdit")
            {
                fileName = ConstantHelpers.fileNameEducationTestDataforEdit;
            }
            else if (testCaseType == "EducationDelete")
            {
                fileName = ConstantHelpers.fileNameEducationTestDataforDelete;
            }
            else if (testCaseType == "EducationIncompleteCreate")
            {
                fileName = ConstantHelpers.fileNameEducationTestDataforIncompleteCreation;
            }
            else if (testCaseType == "EducationWithTooManyDataCreate")
            {
                fileName = ConstantHelpers.fileNameEducationTestDataWithTooManyCharacters;
            }

            var testFixtureParams = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(GetFilePath(fileName)));
            var genericItems = testFixtureParams[$"{typeof(T).Name}"].ToObject<IEnumerable<T>>();

            foreach (var item in genericItems)
            {
                yield return (item);
            }
        }
    }
}
