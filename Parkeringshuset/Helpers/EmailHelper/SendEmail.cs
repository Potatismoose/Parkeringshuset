namespace Parkeringshuset.Helpers.Email
{
    using FluentEmail.Core;
    using FluentEmail.Razor;
    using FluentEmail.Smtp;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public static class SendEmail
    {
        public static void SendWithBlazor(int totalIncome, int nrOfUnPaidBills, string subject, 
            DateTime from, DateTime to)
        {
            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential()
                {
                    UserName = "parkinggarage2021@gmail.com",
                    Password = "Vinter2021"
                }
            });

            StringBuilder template = new();
            template.AppendLine("<h3>Dear Admin</h3>");
            template.AppendLine("<p>During the period @Model.From - @Model.To the garage have " +
                "earned @Model.TotalIncome SEK.</p>");
            if (nrOfUnPaidBills > 1)
            {
                template.AppendLine("<p> However, there is still @Model.NrOfUnPaidBills " +
                    "unpaid bills.</p>");
            }
            template.AppendLine("- Garage System");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = Email
                .From("parkinggarage2021@gmail.com", "Parking Business")
                .To("parking.garage.boss@gmail.com", "Admin")
                .Subject(subject)
                .UsingTemplate(template.ToString(), new { TotalIncome = totalIncome, 
                    NrOfUnPaidBills = nrOfUnPaidBills, From = from.ToString("yyyy,mm,dd"), 
                    To = to.ToString("yyyy,mm,dd") })
                .Send();
        }
    }
}