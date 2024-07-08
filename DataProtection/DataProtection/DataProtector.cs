using System.Security.Cryptography;
using System.Text;

namespace DataProtection
{
    public class DataProtector
    {
        /*
         * Bu uygulamada kullanacağınız verileri (bağlantı cümlesi gibi) kripto ederek saklamak istiyorsunuz.
         * E1. Veriyi şifrele (kripto)
         * E2. Kriptolanmış halini dosyada sakla.
         * 
         * D1 Dosyadan oku
         * D2 Ve şifreyi çöz
         */
        private readonly byte[] entropy;
        private readonly string path;
        public DataProtector(string path)
        {
            entropy = RandomNumberGenerator.GetBytes(16);
            this.path = path;
        }

        public int EncryptData(string value)
        {
            var encodedValue = Encoding.UTF8.GetBytes(value);
            var encryptedValue=  ProtectedData.Protect(encodedValue, entropy, DataProtectionScope.CurrentUser);
            using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
            if (fileStream.CanWrite && encryptedValue !=null)
            {
                fileStream.Write(encryptedValue, 0, encryptedValue.Length);
            }

            return encryptedValue.Length;

        }

        public string DecryptData(int encryptedLength) {
            var fileStream = new FileStream(path, FileMode.Open);

            var input = new byte[encryptedLength];
            /*
             * HMACSHA256 -> BCrypt (Kullanıcının parolasının şifrelenmesi)
             * AES (Metinsel şifreleme) 
             * Ryjndael Managed (Tranfer edilecek verinin şifrelenmesi)
             * 
             * Türkay
             * UVSLBZ
             */

            var output = new byte[encryptedLength];
            if (fileStream.CanRead)
            {
                fileStream.Read(input, 0, input.Length);
                output = ProtectedData.Unprotect(input, entropy, DataProtectionScope.CurrentUser);

            }

            return Encoding.UTF8.GetString(output);
        }

    }
}
