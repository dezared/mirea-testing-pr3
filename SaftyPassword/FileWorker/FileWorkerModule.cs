using SaftyPassword.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaftyPassword.FileWorker
{
    public class FileWorkerModule
    {
        public static string folderPath = @"c:\sp\";
        public static string filePath = @"C:\sp\contents.hash";

        public FileWorkerModule()
        {
            FileInfo fileInf = new FileInfo(filePath);
            if (!fileInf.Exists)
            {
                Console.WriteLine("SP | На компьютере не найден первичный файл хранения, создаём его...");
                Thread.Sleep(1200);
                Directory.CreateDirectory(folderPath);
                var fs = File.Create(filePath);
                fs.Dispose();
                Console.WriteLine("SP | Файл хранения создан успешно.");
            }
        }

        /// <summary>
        /// Записываем новые данные
        /// </summary>
        public bool WriteData(string password, string data, string inditificator)
        {
            var cryptoModule = new CryptographyModule();
            // хэшируем наш индитификатор
            var hashedIndificator = cryptoModule.Encrypt(inditificator, true, CryptographyModule.indificatorKey);

            var isInFile = GetDataInFile(inditificator);

            if(isInFile != null)
            {
                return false;
            }

            // хэшируем данные паролём
            var hashedText = cryptoModule.Encrypt(data, true, password);

            File.AppendAllText(filePath, $"\n# {hashedIndificator}|{hashedText}");

            return true;
        }

        public string? ReadData(string password, string inditificator)
        {
            var cryptoModule = new CryptographyModule();
            // Получаем данные
            var data = GetDataInFile(inditificator);

            if(data != null)
            {
                if (data.data != null)
                {
                    var encryptMessage = cryptoModule.Decrypt(data.data, true, password);
                    return encryptMessage;
                }
            }

            return null;
        }

        public int GetCountLines()
        {
            var filestrings = readAllLines(filePath);
            return filestrings.Count() - 1;
        }

        /// <summary>
        /// Получения данных в ввиде { inditificator, data, strNum }
        /// </summary>
        private DataInfo? GetDataInFile(string inditificator)
        {
            var cryptoModule = new CryptographyModule();
            // хэшируем наш индитификатор
            var hashedIndificator = cryptoModule.Encrypt(inditificator, true, CryptographyModule.indificatorKey);
            var allLines = readAllLines(filePath);
            var counter = 0;

            foreach(var str in allLines)
            {
                if(str.StartsWith($"# {hashedIndificator}"))
                {
                    var model = new DataInfo()
                    {
                        data = str.Split('|')[1],
                        inditificator = inditificator,
                        hashedInditificator = hashedIndificator,
                        strNum = counter
                    };

                    return model;
                }

                counter++;
            }

            return null;
        }

        private List<string> readAllLines(String i_FileNameAndPath)
        {
            List<string> o_Lines = new List<string>();

            using (FileStream fileStream = File.Open(i_FileNameAndPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    while (streamReader.Peek() > -1)
                    {
                        o_Lines.Add(streamReader.ReadLine());
                    }
                }
            }

            return o_Lines;
        }
    }
}
