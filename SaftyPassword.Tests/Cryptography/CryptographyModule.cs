using SaftyPassword.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaftyPassword.Tests.Cryptography
{
    public class CryptographyModuleTest
    {
        [Test]
        public void CryptographyModule_Encrypt_ShouldReturnValid()
        {
            var module = new CryptographyModule();

            var data = module.Encrypt("mydata", true, "pass");

            Assert.IsTrue(data == "a/EOT0QV8Gk=");
        }

        [Test]
        public void CryptographyModule_Decrypt_ShouldReturnValid()
        {
            var module = new CryptographyModule();

            var data = module.Decrypt("a/EOT0QV8Gk=", true, "pass");

            Assert.IsTrue(data == "mydata");
        }
    }
}
