using System;
using System.IO;
using System.Reflection;

namespace Parkeringshuset.Helpers.TicketHelper
{
    internal static class PdfCreator
    {
        /// <summary>
        /// Uses the nugget SelectPdf to create a PDF from html file.
        /// </summary>
        /// <returns>True if succeded, false if failed.</returns>
        public static bool CreatePdfFromHtmlFile()
        {
            try
            {
                string filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\ticket.html";
                Console.WriteLine("Creating PDF file");
                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertUrl(filePath);
                doc.Save("parkingTicket.pdf");
                doc.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}