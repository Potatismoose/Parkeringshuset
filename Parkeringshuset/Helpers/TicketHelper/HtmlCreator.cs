using Parkeringshuset.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Parkeringshuset.Helpers.TicketHelper
{
    public class HtmlCreator
    {
        private string fileName;
        private string fullPath;
        
        public HtmlCreator()
        {
            fileName = "ticket.html";
            fullPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory) + @"/" + fileName;
        }

        /// <summary>
        /// The boilerplate html code that creates the html document..
        /// </summary>
        /// <returns>True if created. False if it fails.</returns>
        public bool CreateHtmlBoilerPlateCode()
        {
            var boilerplateCode =
$@"<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Document</title>
</head>
<body>

    <div
        class='ticket'
        style='
            width:300px;
            text-align:center;'>
            

        <div
            style='
                display:flex;
                justify-content:space-between;
                font-weight: bold;
                background:#add8e6;
                padding-bottom: 0.2em;'>

            <div
                style='
                    display: flex;
                    justify-content: center;
                    align-content: center;
                    flex-direction: column;'>

                <h1
                    style='padding-left: 10px;
                    margin:0px;'>Parking Ticket</h1>
            </div>
            <div>
                <img
                    src='https://potatismoose.com/qr.png'
                    style='
                        height:70px;
                        width:70px;
                        padding: 10px 10px 0 0'>
                </img>
            </div>
        </div>

        <h3 style='margin:0px;
          font-weight: bold;
          background:#FFFFE0;
          padding-top: 0.8em;'
        >Check in time</h3>

        <p style='margin:0px;
          font-weight: bold;
          background:#FFFFE0;
          font-size: 1.5rem;'
          id='date'></p>

        <p style='margin:0px;
          font-weight: bold;
          background:#FFFFE0;
          font-size: 1.5rem;'
          id='timeOfParking'></p>

        <h3 style='margin:0px;
            font-weight: bold;
            background:#FFFFE0;
        '>Parking zone</h3>

        <p style='margin:0px;
          font-weight: bold;
          background:#FFFFE0;
          font-size: 1.5rem;
          padding-bottom: 0.8em;'
          id='type'></p>

        <h3 style='margin:0px;
          font-weight: bold;
          background:#add8e6;'
          >Reg number</h3>

        <h1 style='margin:0px;
          font-weight: bold;
          background:#add8e6;'
          id='regNr'></h1>
    </div>

</body>
</html>";
            try
            {
                var fs = File.Create(fullPath);
                fs.Close();
                TextWriter tw = new StreamWriter(fileName);
                tw.WriteLine(boilerplateCode);
                tw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Insert parking ticket info into the html file.
        /// </summary>
        /// <param name="ticket">Takes a PTicket as in parameter.</param>
        /// <returns>True if successful insert. False it faild.</returns>
        public bool InsertTicketInformationInHtmlFile(PTicket ticket)
        {
            List<string> listOfTicketItems = new() { "date", "timeOfParking", "type", "regNr" };
            var fileRows = File.ReadAllLines(fullPath);
            List<string> newHtmlFile = new();
            if (ticket is not null)
            {
                foreach (var line in fileRows)
                {
                    bool hasFoundLine = false;
                    for (int i = 0; i < listOfTicketItems.Count(); i++)
                    {
                        if (line.Contains(listOfTicketItems[i]))
                        {
                            var indexOfChar = line.IndexOf('>');

                            var newLine = "";
                            switch (i)
                            {
                                case 0:
                                    if (ticket.CheckedInTime != DateTime.MinValue)
                                    {
                                        newLine = line.Insert(indexOfChar + 1, 
                                            ticket?.CheckedInTime.ToShortDateString());
                                    }
                                    else
                                    {
                                        newLine = line;
                                    }
                                    break;

                                case 1:
                                    if (ticket.CheckedInTime != DateTime.MinValue)
                                    {
                                        newLine = line.Insert(indexOfChar + 1, 
                                            ticket?.CheckedInTime.ToShortTimeString());
                                    }
                                    else
                                    {
                                        newLine = line;
                                    }
                                    break;

                                case 2:
                                    if (ticket.Type is not null)
                                    {
                                        newLine = line.Insert(indexOfChar + 1, ticket?.Type?.Name);
                                    }
                                    else
                                    {
                                        newLine = line;
                                    }
                                    break;

                                case 3:
                                    if (ticket.Vehicle is not null)
                                    {
                                        newLine = line.Insert(indexOfChar + 1, 
                                            ticket?.Vehicle?.RegistrationNumber);
                                    }
                                    else
                                    {
                                        newLine = line;
                                    }
                                    break;

                                default:
                                    break;
                            }
                            newHtmlFile.Add(newLine);
                            hasFoundLine = true;
                        }
                    }
                    if (hasFoundLine)
                    {
                        continue;
                    }
                    newHtmlFile.Add(line);
                }
            }
            File.WriteAllLines(fullPath, newHtmlFile);
            if (!fileRows.SequenceEqual(newHtmlFile))
            {
                return true;
            }
            return false;
        }
    }
}