using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NeroliTech.Server.Models
{
    public partial class ExporterDeclaration
    {
        public ExporterDeclaration()
        {
            ExporterShippingDetails = new HashSet<ExporterShippingDetail>();
            PreshipmentInspectionFindings = new HashSet<PreshipmentInspectionFinding>();
        }

        public Guid Id { get; set; }
        public string CciNo { get; set; }
        public string NxpFormNo { get; set; }
        public string NepcNo { get; set; }
        public string Year { get; set; }
        public double HsCode { get; set; }
        public string Origin { get; set; }
        public DateTime? Date { get; set; }
        public string Exporter { get; set; }
        public string ExporterAddress { get; set; }
        public string ExporterRcNo { get; set; }
        public string ExporterBankReference { get; set; }
        public string ImporterName { get; set; }
        public string ImporterAddress { get; set; }
        public string ExporterBankName { get; set; }
        public string ExporterBankAddress { get; set; }
        public string ImporterBank { get; set; }
        public string ImporterBankAddress { get; set; }
        public string ImporterBankReference { get; set; }
        public string GoodToExport { get; set; }
        public string Units { get; set; }
        public double? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ExporterInvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string BasisOfSale { get; set; }
        public string PaymentTerms { get; set; }
        public string Currency { get; set; }
        public decimal? ExporterFobInvoiceValue { get; set; }
        public decimal? FreightCharges { get; set; }
        public decimal? InsuranceCharges { get; set; }
        public decimal? TotalInvoiceValue { get; set; }
        public byte[] Signature { get; set; }

        //[JsonIgnore]
        public virtual ICollection<ExporterShippingDetail> ExporterShippingDetails { get; set; }
        //[JsonIgnore]
        public virtual ICollection<PreshipmentInspectionFinding> PreshipmentInspectionFindings { get; set; }
    }
}
