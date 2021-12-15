using GemBox.Document;
using Spire.Pdf;
using System;
using System.IO;
using System.Reflection;

namespace Parkeringshuset.Helpers.TicketHelper
{
    public static class PdfCreator
    {
        /// <summary>
        /// Uses the nugget SelectPdf to create a PDF from html file.
        /// </summary>
        /// <returns>True if succeded, false if failed.</returns>
        public static bool CreatePdfFromHtmlFile()
        {
            string fileName = "ticket.html";
            string fullPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory) + 
                @"/" + fileName;
            try
            {
                // If using Professional version, put your serial key below.
                ComponentInfo.SetLicense("FREE-LIMITED-KEY");

                // Load input HTML file.
                DocumentModel document = DocumentModel.Load(fullPath);

                // When reading any HTML content a single Section element is created.
                // We can use that Section element to specify various page options.
                Section section = document.Sections[0];
                PageSetup pageSetup = section.PageSetup;

                PageMargins pageMargins = pageSetup.PageMargins;
                pageMargins.Top = pageMargins.Bottom = pageMargins.Left = pageMargins.Right = 190;

                // Save output PDF file.
                document.Save("parkingTicket.pdf");

                //string filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
                //@"\ticket.html";
                //Console.WriteLine("Creating ticket");
                //SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                //SelectPdf.PdfDocument doc = converter.ConvertUrl(filePath);
                //doc.Save("parkingTicket.pdf");
                //doc.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}