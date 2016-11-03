﻿using System.Collections.Generic;
using IVH7_Cinema.Domain.Entities;
using System;


namespace IVH7_Cinema.Domain.Abstract
{
    public interface IPrinter
    {
        void PrintA4Ticket(List<Ticket> T, string username, Int64 ContainsPopcorn, Int64 Contains3D);

        void PrintBiosTicket(List<Ticket> T, string username, int id);

        void Print3DTicket(int Amount, int id, DateTime today, string username);

        void PrintPopcornTicket(int Amount, int id, DateTime today, string username);
    }
}
