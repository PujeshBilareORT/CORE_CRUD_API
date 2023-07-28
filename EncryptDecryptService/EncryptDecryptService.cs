using CORE_CRUD_API.Model;
using System.Security.Cryptography;
using System.Text;

namespace CORE_CRUD_API.EncryptDecryptService
{
    public class EncryptDecryptService
    {
        MyUserMasterModel MUMM = new MyUserMasterModel();
        string MyKey;
        TripleDESCryptoServiceProvider cryptDES3 = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider cryptMD5Hash = new MD5CryptoServiceProvider();
        public EncryptDecryptService()
        {
            MyKey = GenerateRandomKey();
        }
        public string GenerateRandomKey()
        {
            Random r = new Random();
            string AllCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 12; i++)
            {
                int randomIndex = r.Next(AllCharacters.Length);
                stringBuilder.Append(AllCharacters[randomIndex]);
            }
            return stringBuilder.ToString();
        }

        public string Encrypt(MyUserMasterModel um)
        {
            cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(um.MyKey));
            cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = cryptDES3.CreateEncryptor();
            ASCIIEncoding MyASCIIEncoding = new ASCIIEncoding();
            byte[] buff = ASCIIEncoding.ASCII.GetBytes(um.Password);
            return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
        }

        public string EncryptKey(MyUserMasterModel um)
        {

            cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes("245_hASC3sxk@!#$"));
            cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = cryptDES3.CreateEncryptor();
            ASCIIEncoding MyASCIIEncoding = new ASCIIEncoding();
            byte[] buff = ASCIIEncoding.ASCII.GetBytes(um.MyKey);
            return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
        }

        public string DecryptKey(string myString)
        {
            cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes("245_hASC3sxk@!#$"));
            cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = cryptDES3.CreateDecryptor();
            byte[] buff = Convert.FromBase64String(myString);
            return ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
        }

        public string GeneratePassword()
        {
            const string digits = "0123456789";
            const string special = "!@#$&*.?";
            var random = new Random();
            // Select three digits
            var digitChars = new string(
                Enumerable.Repeat(digits, 3).SelectMany(x => x).ToArray()
                    .OrderBy(x => random.Next())
                    .Take(3)
                    .ToArray());
            // Select two special characters
            var specialChars = new string(
                Enumerable.Repeat(special, 2).SelectMany(x => x).ToArray()
                    .OrderBy(x => random.Next())
                    .Take(2)
                    .ToArray());
            // Select remaining characters from digits and special characters
            var remainingChars = new string(
                Enumerable.Repeat(digits + special, 5 - 3 - 2).SelectMany(x => x).ToArray()
                    .OrderBy(x => random.Next())
                    .Take(5 - 3 - 2)
                    .ToArray());
            // Combine the characters and shuffle them
            var passwordChars = digitChars + specialChars + remainingChars;
            var shuffledChars = new string(passwordChars.OrderBy(x => random.Next()).ToArray());
            return shuffledChars;
        }
    }
}
