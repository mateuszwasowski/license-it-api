using System;
using System.Collections.Generic;
using System.Linq;
using licensemanager.Models;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;
using licensemanager.Repositories;
using licensemanager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace licensemanager.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class ClientsController : Controller
    {
        // GET: api/Clients/Get
        [HttpGet]
        [Route("api/Clients/Get")]
        public ResponseModel<IEnumerable<ClientsModel>> Get()
        {
            var resp = new ResponseModel<IEnumerable<ClientsModel>>();

            try
            {
                IClientsRepository appRepo = new ClientsRepository(new DataBaseContext());
                var appList = appRepo.GetClientsModel();

                if (appList != null && appList.Any())
                {
                    resp.Data = appList;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Description = "No clients";
                    resp.Data = null;
                }
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = null;
            }

            return resp;
        }

        // GET: api/Clients/GetByGroup/{idGroup}
        [HttpGet]
        [Route("api/Clients/GetByGroup/{idGroup}")]
        public ResponseModel<IEnumerable<ClientsModel>> Get(int idGroup)
        {
            var resp = new ResponseModel<IEnumerable<ClientsModel>>();

            try
            {
                IClientsRepository appRepo = new ClientsRepository(new DataBaseContext());
                var appList = appRepo.GetClientsModel(idGroup);

                if (appList != null && appList.Any())
                {
                    resp.Data = appList;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Description = "No clients";
                    resp.Data = null;
                }
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = null;
            }

            return resp;
        }

        // GET: api/Clients/GetById/5
        [HttpGet]
        [Route("api/Clients/GetById/{id}")]
        public ResponseModel<ClientsModel> GetById(int id)
        {
            var resp = new ResponseModel<ClientsModel>();

            try
            {
                IClientsRepository appRepo = new ClientsRepository(new DataBaseContext());
                var app = appRepo.GetClientsModelById(id);

                if (app != null)
                {
                    resp.Data = app;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Data = null;
                    resp.Description = "Application not found";
                }
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = null;
            }

            return resp;

        }

        // POST: api/Clients/Add
        [HttpPost]
        [Route("api/Clients/Add")]
        public ResponseModel<int> Post([FromBody]ClientsModel dataToAdd)
        {
            var resp = new ResponseModel<int>();

            try
            {
                if (dataToAdd == null)
                {
                    throw new Exception("Data is null");
                }

                var model = new Clients()
                {
                    Name = dataToAdd.Name,
                    IsActive = dataToAdd.IsActive,
                    Creation = DateTime.Now,
                    Updated = DateTime.Now,
                    IdGroup = dataToAdd.IdGroup
                };

                IClientsRepository appRepo = new ClientsRepository(new DataBaseContext());
                
                if (appRepo.ExistByName(model.IdGroup, model.Name))
                    throw new Exception("Client already exist");

                if (appRepo.Insert(model))
                {
                    resp.Data = model.Id;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    throw new Exception("Not inserted");
                }
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = 0;
            }

            return resp;
        }

        // PUT: api/Clients/Edit
        [HttpPut]
        [Route("api/Clients/Edit")]
        public ResponseModel<bool> Put([FromBody]ClientsModel applicationData)
        {
            var resp = new ResponseModel<bool>();

            try
            {
                resp.Status = 200;
                resp.Data = true;
                resp.Description = "The function is not finished - always true :)";
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = true;
            }

            return resp;
        }

        // DELETE: api/Clients/Delete
        [HttpDelete]
        [Route("api/Clients/Delete")]
        public ResponseModel<bool> Delete([FromBody]int id)
        {
            var resp = new ResponseModel<bool>();

            try
            {
                if (id == 0)
                    throw new Exception("id not found");

                resp.Status = 200;
                resp.Data = true;
                resp.Description = "The function is not finished - always true :)";
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = true;
            }

            return resp;
        }
    }
}
