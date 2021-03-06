﻿using System;
using System.Linq;
using System.Text;
using licensemanager.Models.AppModel;
using licensemanager.Repositories;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Classes
{
    public static class ClientClass
    {
        public static ClientsModel GetClient(int id)
        {
            IClientsRepository repo = new ClientsRepository(new DataBaseContext());

            return repo.GetClientsModelById(id);
        }
        
        public static string GetClientName(int id)
        {
            IClientsRepository repo = new ClientsRepository(new DataBaseContext());

            return repo.GetClientsModelById(id)?.Name ?? string.Empty;
        }
        
    }
}
