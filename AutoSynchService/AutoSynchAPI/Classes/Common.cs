using AutoSynchSqlServer.Models;
using Microsoft.Extensions.Caching.Memory;

namespace AutoSynchAPI.Classes
{
    internal class Common
    {
        public static OrgOrganization GetOrganization(int OrgId, int BranchId, OrgOrganization OrgInfo, OrgBranch branch)
        {
            // var OrgInfo = new OrgOrganization();


            //OrgInfo = (from x in dbContext.OrgOrganization where x.Id == OrgId select x).FirstOrDefault();

            if (OrgInfo.UseInfoForAllBranches == false)
            {
                //List<OrgBranch> Branches = new List<OrgBranch>();
                //OrgBranch branch = new OrgBranch();

                //branch = (from x in dbContext.OrgBranch where x.Id == BranchId select x).FirstOrDefault();

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
            return OrgInfo;
        }
        public static AccFiscalYear GetPurchaseFiscalYear(int OrgId, int BranchId, OrgOrganization OrgInfo, OrgBranch orgBranch, Entities dataContext)
        {
            int DefaltFyId = orgBranch.AccPurchaseFiscalYearId;
            var fiscalYear = new AccFiscalYear();
            fiscalYear = (from x in dataContext.AccFiscalYear
                          join b in dataContext.OrgBranch on x.BranchId equals b.Id
                          where (GetOrganization(OrgId, BranchId,OrgInfo, orgBranch).DmAccFyear == "Merge" ? (b.OrgId == OrgId && b.UseDataInMerging == "Yes") : x.BranchId == BranchId)
                          && (DefaltFyId > 0 ? x.Id == DefaltFyId : x.IsActive == true)
                          select x).FirstOrDefault();
            return fiscalYear;
        }

    }
}
