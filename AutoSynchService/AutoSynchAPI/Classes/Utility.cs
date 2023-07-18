using AutoSynchSqlServer.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace AutoSynchAPI.Classes
{
    public class Utility
    {
        public static AccFiscalYear GetCurrentFiscalYear(Entities dbContext, int OrgId, int BranchId)
        {

            List<AccFiscalYear> fiscalYears = new List<AccFiscalYear>();
            var data = new AccFiscalYear();

            data = (from x in dbContext.AccFiscalYear
                    join y in dbContext.OrgBranch on x.BranchId equals y.Id
                    where y.OrgId == OrgId && x.IsActive == true && x.IsDeleted == false
                    select x).FirstOrDefault();
            if (GetOrganization(dbContext, OrgId, BranchId).DmAccFyear == "Separate")
            {
                data = (from x in dbContext.AccFiscalYear
                        where x.BranchId == BranchId && x.IsActive == true && x.IsDeleted != true
                        select x).FirstOrDefault();
            }
            return data;
        }
        public static AccFiscalYear GetPurchaseFiscalYear(Entities dbContext, int OrgId, int BranchId)
        {
            int DefaltFyId = GetBranchDefaultSettings(dbContext,BranchId).AccPurchaseFiscalYearId;
            var fiscalYear = new AccFiscalYear();
            fiscalYear = (from x in dbContext.AccFiscalYear
                          join b in dbContext.OrgBranch on x.BranchId equals b.Id
                          where (GetOrganization(dbContext,OrgId,BranchId).DmAccFyear== "Merge" ? (b.OrgId == OrgId && b.UseDataInMerging == "Yes") : x.BranchId == BranchId)
                          && (DefaltFyId > 0 ? x.Id == DefaltFyId : x.IsActive == true)
                          select x).FirstOrDefault();
            return fiscalYear;
        }
        public static OrgOrganization GetOrganization(Entities dbContext, int OrgId, int BranchId)
        {
            var OrgInfo = new OrgOrganization();


            OrgInfo = (from x in dbContext.OrgOrganization where x.Id == OrgId select x).FirstOrDefault();

            if (OrgInfo != null && OrgInfo.UseInfoForAllBranches == false)
            {

                List<OrgBranch> Branches = new List<OrgBranch>();
                OrgBranch branch = (from x in dbContext.OrgBranch where x.Id == BranchId select x).FirstOrDefault();
                if (branch != null)
                {
                    OrgInfo.OrgName = branch.BranchName;
                    OrgInfo.OrgLogo = branch.BranchLogoName;
                    OrgInfo.LongAddress = branch.LongAddress;
                    OrgInfo.ShortAddress = branch.ShortAddress;
                    OrgInfo.MobileNumber = branch.MobileNumber;
                    OrgInfo.PhoneNumber = branch.PhoneNumber;
                    OrgInfo.Email = branch.Email;
                    OrgInfo.Website = branch.Website;
                    OrgInfo.KpraNo = OrgInfo.KpraNo;
                }
            }
            return OrgInfo;
        }
        public static OrgOrganization GetOrganizationById(Entities dbContext, int OrgId)
        {
            var OrgInfo = (from x in dbContext.OrgOrganization where x.Id == OrgId select x).FirstOrDefault();
            if (OrgInfo != null)
            {
                if (OrgInfo.BankLogo == null || OrgInfo.BankLogo == "")
                {
                    OrgInfo.BankLogo = "0da9ab8c-f0a7-476b-a005-2cb6109e1ece-AblLogo.jpg";
                }
            }
            return OrgInfo;
        }
        public static OrgBranch GetBranchDefaultSettings(Entities dbContext, int BranchId)
        {
            List<OrgBranch> Branches = new List<OrgBranch>();
            OrgBranch branch = new OrgBranch();

            branch = (from x in dbContext.OrgBranch where x.Id == BranchId select x).FirstOrDefault();

            return branch;
        }
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
