using SaftyPassword.FileWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaftyPassword.Tests.FileWorker
{
    public class FileWorkerTests
    {
        public FileWorkerTests()
        {
            FileWorkerModule.folderPath = @"c:\sp-test\";
            FileWorkerModule.filePath = FileWorkerModule.folderPath + "contents-test.hash";
        }

        [Test]
        public void FileWorkerModule_Ctr_ShouldCreateFile()
        {
            // Act
            var module = new FileWorkerModule();

            // Arrange

            FileInfo fileInf = new FileInfo(FileWorkerModule.filePath);

            // Assert

            Assert.IsTrue(fileInf.Exists);

            Directory.Delete(FileWorkerModule.folderPath, true);
        }

        [Test]
        public void FileWorkerModule_WriteData_ShouldWriteData()
        {
            // Act
            var module = new FileWorkerModule();

            // Arrange
            module.WriteData("mypassword", "mydata", "myindificator");

            // Assert
            FileInfo fileInf = new FileInfo(FileWorkerModule.filePath);
            Assert.IsTrue(fileInf.Length > 0);

            Directory.Delete(FileWorkerModule.folderPath, true);
        }

        [Test]
        public void FileWorkerModule_ReadData_ShouldReturnValidData()
        {
            // Act
            var module = new FileWorkerModule();
            var dataSave = "mydata";

            // Arrange
            module.WriteData("mypassword", dataSave, "myindificator");

            // Assert

            var data = module.ReadData("mypassword", "myindificator");

            Assert.IsTrue(data.Equals(dataSave));

            Directory.Delete(FileWorkerModule.folderPath, true);
        }

        [Test]
        public void FileWorkerModule_ReadData_ShouldReturnInvalidData()
        {
            // Act
            var module = new FileWorkerModule();
            var dataSave = "mydata";

            // Arrange
            module.WriteData("mypassword", dataSave, "myindificator");

            // Assert

            var data = module.ReadData("myanotherpassword", "myindificator");

            Assert.IsFalse(data == dataSave);

            Directory.Delete(FileWorkerModule.folderPath, true);
        }

        [Test]
        public void FileWorkerModule_ReadData_ShouldReturnNullData()
        {
            // Act
            var module = new FileWorkerModule();

            // Arrange
            module.WriteData("mypassword", "data", "myindificator");

            // Assert

            var data = module.ReadData("mypassword", "myanotherindificator");

            Assert.IsTrue(data == null);

            Directory.Delete(FileWorkerModule.folderPath, true);
        }

        [Test]
        public void FileWorkerModule_GetCount_ShouldReturnValidData()
        {
            // Act
            var module = new FileWorkerModule();

            // Arrange
            module.WriteData("mypassword", "data", "myindificator");
            module.WriteData("mypassword", "data", "myindificator1");
            module.WriteData("mypassword", "data", "myindificator2");
            module.WriteData("mypassword", "data", "myindificator3");
            module.WriteData("mypassword", "data", "myindificator4");

            // Assert

            var data = module.GetCountLines();

            Assert.IsTrue(data == 5);

            Directory.Delete(FileWorkerModule.folderPath, true);
        }
    }
}