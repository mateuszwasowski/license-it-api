using System;
using System.Collections.Generic;
using System.Linq;
using licensemanager.Classes;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models;
using licensemanager.Models.AppModel;
using licensemanager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace licensemanager.Controllers
{
    [Produces("application/json")]
    public class LicenseController : Controller
    {
        // GET: api/License/Get
        [HttpGet]
        [Route("api/License/Get")]
        public ResponseModel<IEnumerable<LicenseModel>> Get()
        {
            var resp = new ResponseModel<IEnumerable<LicenseModel>>();

            try
            {
                ILicenseRepository appRepo = new LicenseRepository(new DataBaseContext());
                var appList = appRepo.GetLicenseModel();

                if (appList.Any())
                {
                    resp.Data = appList;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Description = "Not found";
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

        // GET: api/License/Get
        [HttpGet]
        [Route("api/License/GetByApplication/{id}")]
        public ResponseModel<IEnumerable<LicenseModel>> GetByApplication(int id)
        {
            var resp = new ResponseModel<IEnumerable<LicenseModel>>();

            try
            {
                ILicenseRepository appRepo = new LicenseRepository(new DataBaseContext());
                var app = appRepo.GetLicensesModelByApplication(id);

                if (app != null)
                {
                    resp.Data = app;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Description = "Not found";
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

        // GET: api/License/Get
        [HttpGet]
        [Route("api/License/Get/{id}")]
        public ResponseModel<LicenseModel> GetByIdn(int id)
        {
            var resp = new ResponseModel<LicenseModel>();

            try
            {
                ILicenseRepository appRepo = new LicenseRepository(new DataBaseContext());
                var app = appRepo.GetById(id);

                if (app != null)
                {
                    resp.Data = app;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                    throw new Exception("Not found");
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = null;
            }

            return resp;
        }

        // GET: api/License/GetNewKey
        [HttpGet]
        [Route("api/License/GetNewKey")]
        public ResponseModel<string> GetNewKey()
        {
            var resp = new ResponseModel<string>();

            try
            {
                var licNumber = LicenseClass.GetNewLicenseString();
                if (!string.IsNullOrEmpty(licNumber))
                {
                    resp.Status = 200;
                    resp.Description = "OK";
                    resp.Data = licNumber;
                }
                else {
                    throw new Exception("Number can't be generate");
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
        
        // POST: api/License/Add
        [HttpPost]
        [Route("api/License/Add")]
        public ResponseModel<string> Post([FromBody] LicenseModel dataToAdd)
        {
            var resp = new ResponseModel<string>();

            try
            {
                if (dataToAdd is null)
                {
                    resp.Data = string.Empty;
                    resp.Status = 500;
                    resp.Description = "Data is null";
                    return resp;
                }

                var validate = LicenseClass.ValidateLicenseAdd(dataToAdd);
                if (validate != null)
                {
                    resp.Data = string.Empty;
                    resp.Status = 500;
                    resp.Description = validate.Message;
                    return resp;
                }

                var licNumber = LicenseClass.GetNewLicenseString();
                if (string.IsNullOrEmpty(licNumber))
                {
                    throw new Exception("Number can't be generate");
                }

                var model = new Licenses
                {
                    Creation = DateTime.Now,
                    IdClients = dataToAdd.IdClient,
                    IsActive = dataToAdd.IsActive,
                    AssignedVersion = dataToAdd.AssignedVersion,
                    Expiration = dataToAdd.Expiration,
                    IdApplication = dataToAdd.IdApplication,
                    IdentityNumber = dataToAdd.IdentityNumber,
                    Inclusion = dataToAdd.Inclusion,
                    IsActivated = dataToAdd.IsActivated,
                    Number = licNumber,
                    Permissions = PermissionClass.ConvertPermission(dataToAdd.PermissionsModel)
                };

                ILicenseRepository appRepo = new LicenseRepository(new DataBaseContext());

                if (appRepo.Insert(model))
                {
                    if (model.Permissions != null)
                    {
                        if (!LicenseClass.InsertPermissions(model))
                            throw new Exception("Error in insert permissions");
                    }
                    resp.Data = licNumber;
                    resp.Status = 200;
                    resp.Description = "OK";
                    return resp;
                }
                throw new Exception("Unkown insert error");
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = string.Empty;
                return resp;
            }
        }

        // PUT: api/License/Edit
        [HttpPut]
        [Route("api/License/Edit")]
        public ResponseModel<bool> Put([FromBody]LicenseModel licenseModel)
        {
            var resp = new ResponseModel<bool>();

            try
            {
                if (licenseModel == null)
                    throw new Exception("Data is null");

                var id = licenseModel.Id;

                if (id <= 0)
                    throw new Exception("id_license <= 0");

                ILicenseRepository repo = new LicenseRepository(new DataBaseContext());
                resp.Data =  repo.EditLicense(id, licenseModel);

                resp.Status = 200;
                resp.Description = "OK";
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = false;
            }

            return resp;
        }

    }
}
