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
            HtmlCreator.CheckIfOsIsLinuxOrOSX();
            string filePath = Path.GetDirectoryName(HtmlCreator.fullPath);
            Console.WriteLine("Creating ticket");
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            try
            {
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