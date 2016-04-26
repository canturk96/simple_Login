using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LoginSystem.Encryption
{
    class AEScrypt
    {
        //256-bit key
        public static string key = "1vsVCExkC2XoasBhjq8f1S5aDdxCfaic";

        //Initialization Vector must be 128-bit 
        public static string IV = "9kaxlohmrkafaoej";

        public static string Encrypt(string decrypted)
        {

            byte[] textbytes = Encoding.ASCII.GetBytes(decrypted);
            AesCryptoServiceProvider service = new AesCryptoServiceProvider();

            service.BlockSize = 128;
            service.KeySize = 256;
            service.Key = Encoding.ASCII.GetBytes(key);
            service.IV = Encoding.ASCII.GetBytes(IV);

            service.Padding = PaddingMode.PKCS7;
            service.Mode = CipherMode.CBC;

            ICryptoTransform Icryptor = service.CreateEncryptor(service.Key, service.IV);

            byte[] enc = Icryptor.TransformFinalBlock(textbytes, 0, textbytes.Length);
            Icryptor.Dispose();

            return Convert.ToBase64String(enc);
        }

        public static string Decrypt(string encrypted)
        {

            byte[] encbytes = Convert.FromBase64String(encrypted);
            AesCryptoServiceProvider service = new AesCryptoServiceProvider();

            service.BlockSize = 128;
            service.KeySize = 256;
            service.Key = Encoding.ASCII.GetBytes(key);
            service.IV = Encoding.ASCII.GetBytes(IV);

            service.Padding = PaddingMode.PKCS7;
            service.Mode = CipherMode.CBC;

            ICryptoTransform Icryptor = service.CreateDecryptor(service.Key, service.IV);

            byte[] dec = Icryptor.TransformFinalBlock(encbytes, 0, encbytes.Length);
            Icryptor.Dispose();

            return Encoding.ASCII.GetString(dec);
        }
    }
}
