using System.Collections.Generic;
using IVH7_Cinema.Domain.Entities;
using System;
using System.Net.Mail;
using System.Web;

namespace IVH7_Cinema.Domain.Abstract
{
    public interface IMailer
    {
        void SendMessage(Subscriber subscriber, HttpPostedFileBase file);

        void SendShortMessage(String Mailer, Int64 OrderID);
        //void SendShortMessage(string email, Int64 OrderID);

        void sentOrderPDF(String Email, Int64 OrderID, String PDFPad);
    }
}
