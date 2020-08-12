using System;
using System.Collections.Generic;

namespace NeroliTech.Server.Models
{
    public partial class PreshipmentInspectionFinding
    {
        public Guid Id { get; set; }
        public string CciNo { get; set; }
        public DateTime InspectionDate { get; set; }
        public string PackagingDetail { get; set; }
        public double HsCode { get; set; }
        public string BasisOfSale { get; set; }
        public string GoodToExport { get; set; }
        public string Units { get; set; }
        public double? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Quality { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public decimal TotalFobGoodsValue { get; set; }
        public string TotalFobValueWord { get; set; }
        public decimal TotalFreightCharges { get; set; }
        public decimal TotalInsuranceCharge { get; set; }
        public decimal ForexProceedDueTransaction { get; set; }
        public DateTime ExchangeDate { get; set; }
        public string Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        public string ReceiptNo { get; set; }
        public decimal NessChargePayable { get; set; }
        public decimal ActualNessCharges { get; set; }
        public string ReceiptNo2 { get; set; }
        public decimal? BalancePaid { get; set; }

        public virtual ExporterDeclaration CciNoNavigation { get; set; }
    }
}
