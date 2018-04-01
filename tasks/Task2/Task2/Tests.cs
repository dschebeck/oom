using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task2
{
    [TestFixture]
    public class PDF_Tests
    {
        [Test]
        public void PDF_Constructor()
        {
            var testData = new PDF("TestFile");

            string correctFilename = "TestFile";
            string correctExtension = "PDF";
            string correctPermission = "NOTSET";
            int correcFilesize = 0;

            Assert.IsTrue(testData.FileName == correctFilename);
            Assert.IsTrue(testData.FileExtension == correctExtension);
            Assert.IsTrue(testData.FilePermission == correctPermission);
            Assert.IsTrue(testData.FileSize == correcFilesize);
        }

        [Test]
        public void PDF_ChangeFileSize()
        {
            var testData = new PDF("TestFile");
            testData.FileSize = 500;
            int correctFilesize = 500;

            Assert.IsTrue(testData.FileSize == correctFilesize);
        }

        [Test]
        public void PDF_CatchNegativeFileSize()
        {
            var testData = new PDF("TestFile");

            Assert.Catch(() =>
            {
                var x = testData.FileSize = -500;
            });

        }

        [Test]
        public void PDF_AppendData()
        {
            var testData = new PDF("TestFile");
            testData.AppendDate(12);
            int correctFilesize = 12;

            Assert.IsTrue(testData.FileSize == correctFilesize);


        }

        [Test]
        public void PDF_ChangePermission()
        {
            var testData = new PDF("TestFile");
            testData.UpdateFilePermission("RWX------", "UltraSecretPassword");
            string correctPermission = "RWX------";

            Assert.IsTrue(testData.FilePermission == correctPermission);
        }

        [Test]
        public void PDF_ChangePermissionWithWrongPassword()
        {
            var testData = new PDF("TestFile");
            int ret = testData.UpdateFilePermission("RWX------", "UltraSecretWrongPassword");

            string correctPermission = "NOTSET";
            int correctRetVal = 1;

            Assert.IsTrue(testData.FilePermission == correctPermission);
            Assert.IsTrue(ret == correctRetVal);
        }
    }

    [TestFixture]
    public class AUDIO_Tests
    {
        [Test]
        public void AUDIO_Constructor()
        {
            var testData = new Audio("TestFile");

            string correctFilename = "TestFile";
            string correctExtension = "wav";
            string correctPermission = "RW-R--R--";
            int correcFilesize = 0;

            Assert.IsTrue(testData.FileName == correctFilename);
            Assert.IsTrue(testData.FileExtension == correctExtension);
            Assert.IsTrue(testData.FilePermission == correctPermission);
            Assert.IsTrue(testData.FileSize == correcFilesize);
        }

        [Test]
        public void AUDIO_AppendNegativeData()
        {
            var testData = new Audio("TestFile");
            var ret = testData.AppendDate(-5);

            int retVal = 0;
            Assert.IsTrue(ret == retVal);
        }
    }
}
