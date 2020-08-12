using iTextSharp.text;
using iTextSharp.text.pdf;
using NeroliTech.Shared;
using Skclusive.Core.Component;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QRCoder;

namespace NeroliTech.Client.Report
{
    public class ReportCCI
    {
        #region Declaration
        int maxColumn = 3;
        Document document;
        PdfPTable pdfTable = new PdfPTable(3);
        PdfPCell pdfPCell;
        Font fontStyle;
        MemoryStream memoryStream = new MemoryStream();
        IEnumerable<ExporterDeclaration> exporterDeclaration = new List<ExporterDeclaration>();
        ExporterShippingDetails shippingDetails = new ExporterShippingDetails();
        
        #endregion

        public byte[] Report(IEnumerable<ExporterDeclaration> declaration)
        {
            exporterDeclaration = declaration;
            document = new Document(PageSize.A4, 10f, 10f, 20f, 30f);
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            fontStyle = FontFactory.GetFont("Arial", 8f, 1);
            PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            float[] sizes = new float[maxColumn];
            for (int i = 0; i < maxColumn; i++)
            {
                if (i == 0) sizes[i] = 100;
                else sizes[i] = 100;
            }

            pdfTable.SetWidths(sizes);

            this.ReportHeader();
            this.ExporterData();
            this.ShippingData();
            this.InspectionData();

            pdfTable.HeaderRows = 3;
            document.Add(pdfTable);
            document.Close();

            return memoryStream.ToArray();

        }

        private void ReportHeader()
        {
            //pdfPCell = new PdfPCell(this.AddLogo());
            //pdfPCell.Colspan = 1;
            //pdfPCell.Border = 0;
            //pdfTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(this.SetPageTitle());
            pdfPCell.Colspan = 3;
            pdfPCell.Border = 0;
            pdfTable.AddCell(pdfPCell);


            pdfPCell = new PdfPCell(this.Barcode());
            pdfPCell.Colspan = 1;
            pdfPCell.Border = 0;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();
        }

        private PdfPTable AddLogo()
        {
            PdfPTable pdfPTable = new PdfPTable(1);

            //string img = $"{Directory.GetCurrentDirectory()}{@"\wwwwroot\logo.png"}";
            //Image image = Image.GetInstance(img);

            pdfPCell = new PdfPCell();
            pdfPCell.Colspan = 1;
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.Border = 0;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfTable.AddCell(pdfPCell);

            pdfPTable.CompleteRow();

            return pdfPTable;
        }

        private PdfPTable SetPageTitle()
        {
            PdfPTable pdfPTable = new PdfPTable(2);

            fontStyle = FontFactory.GetFont("Arial", 12, 1);
            pdfPCell = new PdfPCell(new Phrase("NEROLI TECHNOLOGIES LIMITED:RC 998339", fontStyle));
            pdfPCell.Colspan = 3;
            pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPCell.Border = 0;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfTable.AddCell(pdfPCell);

            pdfPTable.CompleteRow();

            fontStyle = FontFactory.GetFont("Arial", 10, 1);
            pdfPCell = new PdfPCell(new Phrase("CLEAN CERTIFICATE OF INSEPCTION", fontStyle));
            pdfPCell.Colspan = 3;
            pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPCell.Border = 0;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfTable.AddCell(pdfPCell);

            pdfPTable.CompleteRow();



            return pdfPTable;
        }

        private PdfPTable Barcode()
        {
            PdfPTable pdf = new PdfPTable(3);

            var content = exporterDeclaration.Select(i => i.CciNo).FirstOrDefault();
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData codeData = qr.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);            
            BitmapByteQRCode qrCode = new BitmapByteQRCode(codeData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(3);

            Image image = Image.GetInstance(qrCodeAsBitmapByteArr);

            pdfPCell = new PdfPCell(image);
            pdfPCell.Colspan = 3;
            pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfPCell.Border = 0;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfTable.AddCell(pdfPCell);

            pdf.CompleteRow();

            return pdf;

        }

        private void ExporterData()
        {
            fontStyle = FontFactory.GetFont("Arial", 10f, 1);
            var _fontStyle = FontFactory.GetFont("Arial", 9f, 0);

            #region Table Header
            pdfPCell = new PdfPCell(new Phrase("EXPORTER DECLARATION", fontStyle));
            pdfPCell.Colspan = 3;
            pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();
            #endregion

            #region Table Body
            foreach (var exporter in exporterDeclaration)
            {
                pdfPCell = new PdfPCell(new Phrase($"N.X.P FORM NO: {exporter.NxpFormNo}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"N.E.P.C NO: {exporter.NepcNo}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"YEAR: {exporter.Year}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"H.S CODE: {exporter.HsCode}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"ORIGIN: {exporter.Origin}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"DATE: {exporter.Date}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"EXPORTER'S NAME AND ADDRESS: {exporter.Exporter}, " +
                    $"{exporter.ExporterAddress} ", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"IMPORTER'S NAME AND ADDRESS: {exporter.ImporterName}, " +
                    $"{exporter.ImporterAddress}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"RC NO: {exporter.ExporterRcNo}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"EXPORTER'S BANK NAME AND ADDRESS: {exporter.ExporterBankName}, " +
                    $"{exporter.ExporterBankAddress} ", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"IMPORTER'S NAME AND ADDRESS: {exporter.ImporterName}, " +
                    $"{exporter.ImporterAddress}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"BANK REFERENCE: {exporter.ExporterBankReference}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"BANK REFERENCE: {exporter.ImporterBankReference}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"GOODS TO BE EXPORTED: {exporter.GoodToExport}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"BASIS OF SALE-FOB/CF/CIF: {exporter.BasisOfSale}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"UNITS: {exporter.Units}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"PAYMENT TERMS: {exporter.PaymentTerms}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"QUANITY: {exporter.Quantity}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"CURRENCY: {exporter.Currency}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"UNIT PRICE: {exporter.UnitPrice}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"EXPORTER'S FOB INVOICE VALUE REFERENCE: {exporter.ExporterFobInvoiceValue}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"EXPORTER INVOICE NO: {exporter.ExporterInvoiceNo}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"FREIGHT CHARGES: {exporter.FreightCharges}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"INVOICE DATE: {exporter.InvoiceDate}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase($"INSURANCE CHARGES: {exporter.InsuranceCharges}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

                pdfPCell = new PdfPCell(new Phrase($"TOTAL INVOICE VALUE: {exporter.TotalInvoiceValue}", _fontStyle));
                pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
                pdfPCell.BackgroundColor = BaseColor.White;
                pdfTable.AddCell(pdfPCell);

                pdfTable.CompleteRow();

            }
            #endregion

        }

        private void ShippingData()
        {
            fontStyle = FontFactory.GetFont("Arial", 10f, 1);
            var _fontStyle = FontFactory.GetFont("Arial", 9f, 0);

            #region Table Header
            pdfPCell = new PdfPCell(new Phrase("DECLARED SHIPPING DETAILS", fontStyle));
            pdfPCell.Colspan = 3;
            pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();
            #endregion

            #region Shipping Details Data
            pdfPCell = new PdfPCell(new Phrase($"SHIPPING DATE: ", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase($"SHIPPING AGENT: ", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();

            pdfPCell = new PdfPCell(new Phrase($"CARRIER/VESSEL: ", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase($"LOADING REF NUMBER: ", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();

            pdfPCell = new PdfPCell(new Phrase($"EXIT POINT:", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase($"DESTINATION:", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();

            pdfPCell = new PdfPCell(new Phrase($"CONTAINER NUMBER:", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();

            #endregion
        }

        private void InspectionData()
        {
            fontStyle = FontFactory.GetFont("Arial", 10f, 1);
            var _fontStyle = FontFactory.GetFont("Arial", 9f, 0);

            #region Table Header
            pdfPCell = new PdfPCell(new Phrase("PRESHIPMENT INSPECTION FINDINGS", fontStyle));
            pdfPCell.Colspan = 3;
            pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();
            #endregion

            #region Inspection Findings Data
            pdfPCell = new PdfPCell(new Phrase($"GOODS TO BE EXPORTED: ", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase($"BASIS OF SALE-FOB/CF/CIF: ", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();

            pdfPCell = new PdfPCell(new Phrase($"UNITS: ", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase($"PAYMENT TERMS: ", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            pdfTable.CompleteRow();

            pdfPCell = new PdfPCell(new Phrase($"QUANITY: ", _fontStyle));
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BackgroundColor = BaseColor.White;
            pdfTable.AddCell(pdfPCell);

            #endregion

        }
    }
}
