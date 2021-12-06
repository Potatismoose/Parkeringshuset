using Microsoft.Win32;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringshuset.Helpers.TicketHelper
{
    internal static class PdfCreator
    {
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
