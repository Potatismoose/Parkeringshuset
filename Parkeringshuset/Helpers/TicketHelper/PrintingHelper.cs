using Parkeringshuset.Models;
using Spire.Pdf;
using System;
using System.Drawing.Printing;

namespace Parkeringshuset.Helpers.TicketHelper
{
    internal class PrintingHelper
    {
        /// <summary>
        /// Adapter method (calls other methods).
        /// Creates a html boilerplate code. Then injects the ticket information
        /// to that boilerplate code.
        /// Then converts that html boilerplate code into pdf format before sending it to
        /// the method PrintPdfParkingTicket to print the ticket.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns>true if the ticket is created and printed.</returns>
        public static bool PhysicalTicketCreationAndPrintout(PTicket ticket)
        {
            if (HtmlCreator.CreateHtmlBoilerPlateCode()
                && HtmlCreator.InsertTicketInformationInHtmlFile(ticket)
                && PdfCreator.CreatePdfFromHtmlFile()
                && PrintPdfParkingTicket())
            {
                return true;
            }

            return false;
        }

        private static bool PrintPdfParkingTicket()
        {
            try
            {
                Console.WriteLine("Printing PDF file");
                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromFile("parkingTicket.pdf");
                //Set the printer
                pdf.PrintSettings.PrinterName = GetDefaultPrinter();
                //Print the first page
                pdf.PrintSettings.SelectPageRange(1, 1);
                pdf.Print();
                return true;
            }
            catch
            {
                return false;
            }
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