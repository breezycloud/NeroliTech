using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeroliTech.Shared
{
    public class ExporterDeclaration
    {
        public Guid Id => Guid.NewGuid();
        [Required]
        public string CciNo { get; set; }
        [Required]
        public string NxpFormNo { get; set; }
        [Required]
        public string NepcNo { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public double HsCode { get; set; }
        public string Origin { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Exporter { get; set; }
        [Required]
        public string ExporterAddress { get; set; }
        [Required]
        public string ExporterRcNo { get; set; }        
        [Required]
        public string ImporterName { get; set; }
        [Required]
        public string ImporterAddress { get; set; }
        [Required]
        public string ExporterBankName { get; set; }
        [Required]
        public string ExporterBankAddress { get; set; }
        [Required]
        public string ExporterBankReference { get; set; }
        [Required]
        public string ImporterBank { get; set; }
        [Required]
        public string ImporterBankAddress { get; set; }
        [Required]
        public string ImporterBankReference { get; set; }
        [Required]
        public string GoodToExport { get; set; }
        [Required]
        public string Units { get; set; }
        public double? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ExporterInvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BasisOfSale { get; set; }
        public string PaymentTerms { get; set; }
        public string Currency { get; set; }
        public decimal ExporterFobInvoiceValue { get; set; }
        public decimal FreightCharges { get; set; }
        public decimal InsuranceCharges { get; set; }
        public decimal TotalInvoiceValue { get; set; }
        public byte[] Signature { get; set; }

        public ICollection<ExporterShippingDetails> ShippingDetails { get; set; }
        public ICollection<PreshipmentInspectionFindings> InspectionFindings { get; set; }
    }
}
