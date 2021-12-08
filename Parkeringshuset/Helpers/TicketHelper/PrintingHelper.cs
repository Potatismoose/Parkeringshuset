using Parkeringshuset.Models;
using Spire.Pdf;
using System;
using System.Drawing.Printing;

namespace Parkeringshuset.Helpers.TicketHelper
{
    public class PrintingHelper
    {
        /// <summary>
        /// Adapter method (calls other methods).
        /// Creates a html boilerplate code. Then injects the ticket information
        /// to that boilerplate code.
        /// Then converts that html boilerplate code into pdf format before sending it to
        /// the method PrintPdfParkingTicket to print the ticket.
        /// </summary>
        /// <param name="ticket">Takes a PTicket as in parameter.</param>
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

        /// <summary>
        /// Informs user that a ticket is beeing printed.
        /// Then prints the PDF document.
        /// </summary>
        /// <returns>true if printed false if not.</returns>
        private static bool PrintPdfParkingTicket()
        {
            try
            {
                Console.WriteLine("Printing ticket.");
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

        /// <summary>
        /// Gets the computers default printer.
        /// </summary>
        /// <returns>Default printer name or an empty string if none is found.</returns>
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