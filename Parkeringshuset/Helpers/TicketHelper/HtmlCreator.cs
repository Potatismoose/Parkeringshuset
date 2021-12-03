using Parkeringshuset.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringshuset.Helpers.TicketHelper
{
    internal static class HtmlCreator
    {
        static string fileName = "ticket.html";
        static string fullPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\" + fileName;
        public static bool CreateHtmlBoilerPlateCode() {
            
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

        public static void InsertTicketInformationInHtmlFile(PTicket ticket) 
        {
            List<string> listOfTicketItems = new() { "date", "timeOfParking", "type", "regNr" };
            var fileRows = File.ReadAllLines(fullPath);
            List<string> newHtmlFile = new();
            foreach (var line in fileRows)
            {
                bool hasFoundLine = false;
                if (ticket is not null)
                {                    
                    for (int i = 0; i < listOfTicketItems.Count(); i++)
                    {
                        if (line.Contains(listOfTicketItems[i]))
                        {
                            var indexOfChar = line.IndexOf('>');

                            var newLine = "";
                            switch (i)
                            {
                                case 0:
                                    newLine = line.Insert(indexOfChar + 1, ticket?.CheckedInTime.ToShortDateString());
                                    break;
                                case 1:
                                    newLine = line.Insert(indexOfChar + 1, ticket?.CheckedInTime.ToShortTimeString());
                                    break;
                                case 2:
                                    newLine = line.Insert(indexOfChar + 1, ticket?.Type.Name);
                                    break;
                                case 3:
                                    newLine = line.Insert(indexOfChar + 1, ticket?.Vehicle.RegistrationNumber);
                                    break;
                                default:
                                    break;
                            }
                            newHtmlFile.Add(newLine);
                            hasFoundLine = true;
                        }
                    } 
                }
                if (hasFoundLine) {
                    hasFoundLine = false;
                    continue;
                }
                newHtmlFile.Add(line);
            }

            File.WriteAllLines(fullPath, newHtmlFile);
            
            
                //loopa igenom listan med söktaggar och kolla om arrayens rad innehåller söktagg
        }
    }
}
