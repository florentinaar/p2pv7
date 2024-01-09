using Azure;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Reflection.Metadata;
using iTextSharp.text;
using iTextSharp.text.pdf;
using p2pv7.Services;
using p2pv7.Models;
using Microsoft.AspNetCore.Mvc;

namespace p2pv7.Services
{

    public class PrintService: IPrintService
    {
        public byte[] GenerateDoc(List<Order> orders)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                Paragraph p = new Paragraph("Test");
                p.Alignment = Element.ALIGN_CENTER;
                document.Add(p);

                PdfPTable table = new PdfPTable(1);
                PdfPCell cell1 = new PdfPCell(new Phrase("Date", new Font(Font.FontFamily.HELVETICA, 10)));

                foreach(var order in orders)
                {
                    PdfPCell celln = new PdfPCell(new Phrase("Date", new Font(Font.FontFamily.HELVETICA, 10)));
                    table.AddCell(celln);
                    PdfPCell cellnn = new PdfPCell(new Phrase(order.Email, new Font(Font.FontFamily.HELVETICA, 10)));
                    table.AddCell(cellnn);
                }

                table.AddCell(cell1);
                document.Add(table);
                document.Close();

                writer.Close();

                var constant = ms.ToArray();
                return constant;
            }
        }
    }



}
