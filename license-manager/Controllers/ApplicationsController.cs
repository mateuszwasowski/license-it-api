using System;
using System.Collections.Generic;
using System.Linq;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models;
using licensemanager.Models.AppModel;
using licensemanager.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace licensemanager.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class ApplicationsController : Controller
    {
        // GET: api/Applications/Get
        [HttpGet]
        [Route("api/Applications/Get")]
        public ResponseModel<IEnumerable<ApplicationModel>> Get()
        {
            var resp = new ResponseModel<IEnumerable<ApplicationModel>>();

            try
            {
                IApplicationsRepository appRepo = new ApplicationsRepository(new DataBaseContext());
                var appList = appRepo.GetApplicationModel();

                if (appList != null && appList.Any())
                {
                    resp.Data = appList;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Description = "No applications";
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

         // GET: api/Applications/GetByGroup
        [HttpGet]
        [Route("api/Applications/GetByGroup/{idGroup}")]
        public ResponseModel<IEnumerable<ApplicationModel>> GetByGroup(int idGroup)
        {
            var resp = new ResponseModel<IEnumerable<ApplicationModel>>();

            try
            {
                IApplicationsRepository appRepo = new ApplicationsRepository(new DataBaseContext());
                var appList = appRepo.GetApplicationModel(idGroup);

                if (appList != null && appList.Any())
                {
                    resp.Data = appList;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Description = "No applications";
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

        // GET: api/Applications/GetById
        [HttpGet]
        [Route("api/Applications/GetById/{id}")]
        public ResponseModel<ApplicationModel> GetById(int id)
        {
            var resp = new ResponseModel<ApplicationModel>();

            try
            {
                IApplicationsRepository appRepo = new ApplicationsRepository(new DataBaseContext());
                var app = appRepo.GetApplicationModelById(id);

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

        // POST: api/Applications/Add
        [HttpPost]
        [Route("api/Applications/Add")]
        public ResponseModel<int> Post([FromBody]ApplicationModel dataToAdd)
        {
            var resp = new ResponseModel<int>();

            try
            {
                if (dataToAdd == null)
                {
                    resp.Data = 0;
                    resp.Status = 500;
                    resp.Description = "Data is null";
                }

                var model = new Application()
                {
                    Description = dataToAdd.Description,
                    Name = dataToAdd.Name,
                    IsActive = dataToAdd.IsActive,
                    Version = dataToAdd.Version,
                    Creation = DateTime.Now,
                    IdGroup = dataToAdd.IdGroup
                };

                IApplicationsRepository appRepo = new ApplicationsRepository(new DataBaseContext());

                if(appRepo.Insert(model))
                {
                    resp.Data = model.Id;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Data = 0;
                    resp.Description = "Error";
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

        // PUT: api/Applications/Edit
        [HttpPut]
        [Route("api/Applications/Edit")]
        public ResponseModel<bool> Put([FromBody]ApplicationModel applicationData)
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

        // DELETE: api/Applications/Delete
        [HttpDelete]
        [Route("api/Applications/Delete")]
        public ResponseModel<bool> Delete([FromBody]int id)
        {
            var resp = new ResponseModel<bool>();

            try
            {
                if(id==0)
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
