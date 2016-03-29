using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stock.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;
using Stock.App_Start;

namespace Stock.Services
{
    public class ShareMarketingService : IShareMarketingService
    {
        private ApplicationDbContext _applicationDbContext;

        public ShareMarketingService()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public async Task<Share> GetLatestShareInformationByCompanyCode(string companyCode)
        {
            return await _applicationDbContext.Shares.FirstOrDefaultAsync(x => x.CompanyCode == companyCode && x.PublicationDate == _applicationDbContext.Shares.Max(y => y.PublicationDate));
        }

        public async Task<BoughtShareViewModel> GetCurrentBoughtShareInformation(string companyCode)
        {
            Share _share = await _applicationDbContext.Shares.FirstOrDefaultAsync(x => x.CompanyCode == companyCode && x.PublicationDate == _applicationDbContext.Shares.Max(y => y.PublicationDate));
            return new BoughtShareViewModel(_share);
        }

        public async Task<SoldShareViewModel> GetCurrentSoldShareInformation(string accountName, string companyCode)
        {
            Share _share = await GetLatestShareInformationByCompanyCode(companyCode);
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_share == null || _account == null)
            {
                return null;
            }

            return new SoldShareViewModel(_share, _account.AccountOwnedShares.FirstOrDefault(x => x.OwnedShareCompanyCode == companyCode).NumberOfOwnedShares);

        }

        public async Task<bool> BuyShare(string accountName, string shareCompanyCode, int numberOfShares)
        {
            if (!ApplicationGlobals.IsRemoteServerAvalaible)
            {
                return false;
            }

            Share _share = await _applicationDbContext.Shares.FirstOrDefaultAsync(x => x.CompanyCode == shareCompanyCode && x.PublicationDate == _applicationDbContext.Shares.Max(y => y.PublicationDate));

            if (_share == null)
            {
                return false;
            }

            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_account == null || _share.UnitNumber < numberOfShares || _share.UnitPrice * numberOfShares > _account.AccountWallet)
            {
                return false;
            }

            OwnedShare _ownedShareViewModel = _account.AccountOwnedShares.FirstOrDefault(x => x.OwnedShareCompanyCode == shareCompanyCode);

            if (_ownedShareViewModel == null)
            {
                _ownedShareViewModel = new OwnedShare();
                _account.AccountOwnedShares.Add(_ownedShareViewModel);
            }

            _ownedShareViewModel.NumberOfOwnedShares += numberOfShares;
            _ownedShareViewModel.OwnedShareCompanyCode = shareCompanyCode;
            _share.UnitNumber -= numberOfShares;

            _account.AccountWallet -= _share.UnitPrice * numberOfShares;
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SellShare(string accountName, string shareCompanyCode, int numberOfShares)
        {
            if (!ApplicationGlobals.IsRemoteServerAvalaible)
            {
                return false;
            }

            Share _share = await GetLatestShareInformationByCompanyCode(shareCompanyCode);
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_share == null)
            {
                return false;
            }

            if (_account.AccountOwnedShares.FirstOrDefault(x=>x.OwnedShareCompanyCode == shareCompanyCode && x.NumberOfOwnedShares >= numberOfShares) == null)
            {
                return false;
            }

            _account.AccountOwnedShares.FirstOrDefault(x => x.OwnedShareCompanyCode == shareCompanyCode).NumberOfOwnedShares -= numberOfShares;
            _account.AccountWallet += _share.UnitPrice * numberOfShares;
            _share.UnitNumber += numberOfShares;

            if (_account.AccountOwnedShares.FirstOrDefault(x=>x.OwnedShareCompanyCode == shareCompanyCode).NumberOfOwnedShares == 0)
            {
                _account.AccountOwnedShares.Remove(_account.AccountOwnedShares.FirstOrDefault(x => x.OwnedShareCompanyCode == shareCompanyCode));
            }

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}