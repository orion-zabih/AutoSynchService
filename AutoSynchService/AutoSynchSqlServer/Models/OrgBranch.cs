﻿using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class OrgBranch
    {
        public int Id { get; set; }
        public string? BranchName { get; set; }
        public string? ShortAddress { get; set; }
        public string? LongAddress { get; set; }
        public string? BranchLogoName { get; set; }
        public string? Website { get; set; }
        public string? ReportsTitle { get; set; }
        public int OrgId { get; set; }
        public string? MobileNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BranchLogo { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDeleted { get; set; }
        public bool? ResIsTableSelection { get; set; }
        public bool? ResIsPrintKotPlaceOrder { get; set; }
        public decimal ResTaxInPercent { get; set; }
        public decimal ResSchargesInPercent { get; set; }
        public bool ResIsShowDetailsOfPackage { get; set; }
        public decimal ResBillAmountLimitForScharges { get; set; }
        public int ResTellLimit { get; set; }
        public bool ResIsAllCategoryOption { get; set; }
        public string? ResDefaultBillTypeInPayment { get; set; }
        public string? ResPrintOptionFromPos { get; set; }
        public decimal InvTaxInPercent { get; set; }
        public decimal InvSchargesInPercent { get; set; }
        public decimal InvBillAmountLimitForScharges { get; set; }
        public int InvTellLimit { get; set; }
        public bool InvIsAllCategoryOption { get; set; }
        public bool InvIsPrintKotPlaceOrder { get; set; }
        public bool? ResIsShowIconicSearch { get; set; }
        public bool ResIsDirectAddtoCartItem { get; set; }
        public bool ResIsActiveKipraService { get; set; }
        public bool? ResIsItemAddtoSeparateLine { get; set; }
        public bool ResIsOpenPaymentScreen { get; set; }
        public bool InvIsActiveFbrService { get; set; }
        public int InvFbrPosId { get; set; }
        public bool InvIsShowIconicSearch { get; set; }
        public bool InvIsDirectAddtoCartItem { get; set; }
        public bool InvIsActiveKipraService { get; set; }
        public bool InvIsItemAddtoSeparateLine { get; set; }
        public bool InvIsOpenPaymentScreen { get; set; }
        public string? NtnNo { get; set; }
        public string? TaxFormation { get; set; }
        public string? Strn { get; set; }
        public bool InvIsAllowSaleOnZeroStock { get; set; }
        public string? InvUseFbrApi { get; set; }
        public bool ResIsDisplayCodeOnReceipts { get; set; }
        public bool ResIsTaxInclusive { get; set; }
        public int AccCashAccountId { get; set; }
        public int AccBankGroupId { get; set; }
        public int AccFrieghtAccountId { get; set; }
        public int AccIncomeTaxAccountId { get; set; }
        public int AccSaleTaxAccountId { get; set; }
        public int AccDiscountAccountId { get; set; }
        public string? AccAccountMethod { get; set; }
        public string? AccPostingMethod { get; set; }
        public string? AccAccountPeriod { get; set; }
        public bool InvIsDiscountFlatPos { get; set; }
        public bool InvIsSchargesFlatPos { get; set; }
        public bool ResIsDiscountFlatPos { get; set; }
        public bool InvIsShowCounterSaleCashPos { get; set; }
        public bool? InvIsItemComboLoadOnStart { get; set; }
        public bool InvShowOnlyInvoices { get; set; }
        public bool InvIsEnableSmsOnSale { get; set; }
        public string? SmsApiKey { get; set; }
        public int InvMixSaleQtyCount { get; set; }
        public int AccDiscountOutAccountId { get; set; }
        public int AccFrieghtOutAccountId { get; set; }
        public int AccCustomerAccountId { get; set; }
        public int AccSupplierAccountId { get; set; }
        public int AccInventoryAccountId { get; set; }
        public string? InventoryType { get; set; }
        public bool InvIsEnableSmsOnPurchase { get; set; }
        public string? AccBussinessType { get; set; }
        public string? InvRowAddingStyle { get; set; }
        public string? InvDefaultPaymentType { get; set; }
        public string? InvSaleAccInteg { get; set; }
        public string? InvPurchaseAccInteg { get; set; }
        public string? InsFeeAccInteg { get; set; }
        public string? InvProductLedgerTransactionFromJv { get; set; }
        public string? AccVoucherAutoApproved { get; set; }
        public string? InvCreateJvInCaseOfQtsale { get; set; }
        public string? InvSaleInvoiceDate { get; set; }
        public string? InvUpdateLastPrices { get; set; }
        public int ResDefaultOrderTypeId { get; set; }
        public int InvDefaultOrderTypeId { get; set; }
        public string? UseDataInMerging { get; set; }
        public int AccInvSaleFiscalYearId { get; set; }
        public int AccPurchaseFiscalYearId { get; set; }
        public int AccVoucherFiscalYearId { get; set; }
        public string? InvItemCodeReadOnly { get; set; }
        public string? InvSkipSchargesIfBillIsQt { get; set; }
        public string? InvPurCheckRetailWcost { get; set; }
        public string? InvSaleQtyPriceSeq { get; set; }
        public string? IsShowSohinOpening { get; set; }
        public string? PrivateKey { get; set; }
        public bool IsAffiliated { get; set; }
        public string? ShortName { get; set; }
        public bool EnablePrivateKey { get; set; }
        public bool ResIsShowCounterSaleCashPos { get; set; }
        public string? InvDefaultOrderStatus { get; set; }
        public string? InvShowOrderStatus { get; set; }
        public string? InvSaveOrderZeroItems { get; set; }
        public string? InvCreateJvInCaseOfKotsale { get; set; }
        public string? InsExamUnPaidIsAppear { get; set; }
        public string? MobileAreaCode { get; set; }
        public int CurrentPayChartYear { get; set; }
    }
}
