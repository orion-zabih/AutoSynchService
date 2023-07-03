using System.Security.Cryptography;
using System.Text;

namespace AutoSynchPostVoucher.Classes
{
    public class Utility
    {
    }

    public class CryptoHelper
    {

        public static string Encrypt(string textToEncrypt)
        {

            return EncryptString(textToEncrypt);
        }

        private static string EncryptString(string textToEncrypt)
        {
            var tempKEY = "ANSADDFRGAHCJMKALSLS";
            using (Aes rijndaelCipher = Aes.Create())
            {
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;

                rijndaelCipher.KeySize = 0x80;
                rijndaelCipher.BlockSize = 0x80;
                //byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
                byte[] pwdBytes = Encoding.UTF8.GetBytes(tempKEY);
                byte[] keyBytes = new byte[0x10];
                int len = pwdBytes.Length;
                if (len > keyBytes.Length)
                {
                    len = keyBytes.Length;
                }
                Array.Copy(pwdBytes, keyBytes, len);
                rijndaelCipher.Key = keyBytes;
                rijndaelCipher.IV = keyBytes;
                ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
                return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
            }

        }

        public static string Decrypt(string textToDecrypt)
        {
            return DecryptString(textToDecrypt);
        }

        private static string DecryptString(string textToDecrypt)
        {
            try
            {
                var tempKEY = "ANSADDFRGAHCJMKALSLS";
                using (Aes rijndaelCipher = Aes.Create())
                {
                    rijndaelCipher.Mode = CipherMode.CBC;
                    rijndaelCipher.Padding = PaddingMode.PKCS7;

                    rijndaelCipher.KeySize = 0x80;
                    rijndaelCipher.BlockSize = 0x80;
                    byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
                    //byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
                    byte[] pwdBytes = Encoding.UTF8.GetBytes(tempKEY);
                    byte[] keyBytes = new byte[0x10];
                    int len = pwdBytes.Length;
                    if (len > keyBytes.Length)
                    {
                        len = keyBytes.Length;
                    }
                    Array.Copy(pwdBytes, keyBytes, len);
                    rijndaelCipher.Key = keyBytes;
                    rijndaelCipher.IV = keyBytes;
                    byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    return Encoding.UTF8.GetString(plainText);
                }
            }
            catch (Exception exp)
            {

                throw;
            }
        }



    }
}
