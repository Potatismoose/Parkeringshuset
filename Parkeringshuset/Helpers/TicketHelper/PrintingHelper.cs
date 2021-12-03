using Parkeringshuset.Models;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringshuset.Helpers.TicketHelper
{
    internal class PrintingHelper
    {
        public static bool TicketPrintout(PTicket ticket)
        {
            HtmlCreator.CreateHtmlBoilerPlateCode();
            HtmlCreator.InsertTicketInformationInHtmlFile(ticket);
            PdfCreator.CreatePdfFromHtmlFile();
            PrintPdfParkingTicket();
            return true;
        }

        private static void PrintPdfParkingTicket()
        {
            Console.WriteLine("Printing PDF file");
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile("parkingTicket.pdf");
            //Set the printer 
            pdf.PrintSettings.PrinterName = GetDefaultPrinter();
            //Print the first page
            pdf.PrintSettings.SelectPageRange(1, 1);
            pdf.Print();
        }

        private static string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return String.Empty;
        }
    }
}
