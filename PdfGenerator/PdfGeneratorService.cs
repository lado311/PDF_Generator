using Microsoft.AspNetCore.Mvc.RazorPages;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using SixLabors.Fonts;
using System.Drawing;

namespace PdfGenerator
{
    public class PdfGeneratorService
    {
        public byte[] GeneratePdf(string title, decimal amount, bool includeVat,  string pdfDesctiption )
        {
            using (var document = new PdfDocument())
            {
                document.Info.Title = "Estimate";

                var Pdfpage = document.AddPage();
                var DisplayStyle = XGraphics.FromPdfPage(Pdfpage);
                var textFormatter = new XTextFormatter(DisplayStyle);
                DisplayStyle.DrawRectangle(XBrushes.Black, new XRect(0, 0, Pdfpage.Width, Pdfpage.Height));

                var titleFont = new XFont("Arial", 24, XFontStyle.Bold);
                DisplayStyle.DrawString("500", titleFont, XBrushes.White, new XRect(250, 20, Pdfpage.Width - 40, Pdfpage.Height - 40), XStringFormats.TopLeft);
                DisplayStyle.DrawString("ml", new XFont("Arial", 11, XFontStyle.Regular), XBrushes.OrangeRed, new XRect(290, 32, Pdfpage.Width - 40, Pdfpage.Height - 40), XStringFormats.TopLeft);
                DisplayStyle.DrawString("DIGITAL AGENCY", new XFont("Arial", 8.2, XFontStyle.Regular), XBrushes.White, new XRect(250, 42, Pdfpage.Width - 40, Pdfpage.Height - 40), XStringFormats.TopLeft);

                var offerFont = new XFont("Arial", 20, XFontStyle.Bold);
                DisplayStyle.DrawString("OFFER 01.", offerFont, XBrushes.LightGray, new XRect(20, 100, Pdfpage.Width - 40, Pdfpage.Height - 40), XStringFormats.TopLeft);

                var taskTitleFont = new XFont("Arial", 12, XFontStyle.Regular);
                DisplayStyle.DrawString(title, taskTitleFont, XBrushes.White, new XRect(250, 110, Pdfpage.Width - 40, Pdfpage.Height - 40), XStringFormats.TopLeft);

                DisplayStyle.DrawLine(new XPen(XColors.Gray, 1), 20, 130, Pdfpage.Width - 20, 130) ;

                var contentFont = new XFont("Arial", 12, XFontStyle.Regular); 

                float maxWidth = 300;
                var layout = new XRect(20, 170,maxWidth, Pdfpage.Height);
                DisplayStyle.DrawString(pdfDesctiption, taskTitleFont, XBrushes.White, layout, XStringFormats.TopLeft);

                int yPoint = 150;
   /*             foreach (var task in tasks)
                {
                    DisplayStyle.DrawString($"• {task}", contentFont, XBrushes.White, new XRect(20, yPoint, Pdfpage.Width - 40, Pdfpage.Height - 40), XStringFormats.TopLeft);
                    yPoint += 20;
                }*/


   

                string deadLineText = $"Time - 1 Day";
                string priceText =  $"Price - {amount:C} {(includeVat ? "(Without VAT)" : "")}";
                DisplayStyle.DrawString(deadLineText, contentFont, XBrushes.White, new XRect(20, yPoint + 50, Pdfpage.Width - 40, Pdfpage.Height - 40), XStringFormats.TopLeft);
                DisplayStyle.DrawString(priceText, contentFont, XBrushes.White, new XRect(20, yPoint + 50, Pdfpage.Width - 40, Pdfpage.Height - 40), XStringFormats.TopRight);


                var footerFont = new XFont("Arial", 10, XFontStyle.Italic);
                DisplayStyle.DrawString("2024 © 500ml All Rights Reserved", footerFont, XBrushes.Gray, new XRect(20, Pdfpage.Height - 40, Pdfpage.Width - 40, 20), XStringFormats.BottomLeft);

                using (var stream = new MemoryStream())
                {
                    document.Save(stream, false);
                    return stream.ToArray();
                }
            }
        }
    }
    
}
