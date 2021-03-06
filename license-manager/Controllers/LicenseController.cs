using System;
using System.Collections.Generic;
using System.Linq;
using licensemanager.Classes;
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
    public class LicenseController : Controller
    {
        public ILicenseRepository AppRepo = new LicenseRepository(new DataBaseContext());
        public IPermissionsRepository PermissionsRepository = new PermissionsRepository(new DataBaseContext());

        // GET: api/License/Get
        [HttpGet]
        [Route("api/License/Get")]
        public ResponseModel<IEnumerable<LicenseModel>> Get()
        {
            var resp = new ResponseModel<IEnumerable<LicenseModel>>();

            try
            {
                var appList = AppRepo.GetLicenseModel();

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
                var app = AppRepo.GetLicensesModelByApplication(id);

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
        public ResponseModel<LicenseModel> GetById(int id)
        {
            var resp = new ResponseModel<LicenseModel>();

            try
            {
                var app = AppRepo.GetById(id);

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
                var licenseClass = new LicenseClass
                {
                    LicenseRepository = AppRepo,
                    PermissionsRepository = PermissionsRepository
                };

                var licNumber = licenseClass.GetNewLicenseString();
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
                var licenseClass = new LicenseClass
                {
                    LicenseRepository = AppRepo,
                    PermissionsRepository = PermissionsRepository
                };
                var validate = licenseClass.ValidateLicenseAdd(dataToAdd);
                if (validate != null)
                {
                    resp.Data = string.Empty;
                    resp.Status = 500;
                    resp.Description = validate.Message;
                    return resp;
                }

                var licNumber = licenseClass.GetNewLicenseString();
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
                
                if (AppRepo.Insert(model))
                {
                    if (model.Permissions != null)
                    {
                        if (!licenseClass.InsertPermissions(model))
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

        // POST: api/License/AddReturnId
        [HttpPost]
        [Route("api/License/AddReturnId")]
        public ResponseModel<int> PostReturnId([FromBody] LicenseModel dataToAdd)
        {
            var resp = new ResponseModel<int>();

            try
            {
                if (dataToAdd is null)
                {
                    resp.Data = 0;
                    resp.Status = 500;
                    resp.Description = "Data is null";
                    return resp;
                }

                var licenseClass = new LicenseClass
                {
                    LicenseRepository = AppRepo,
                    PermissionsRepository = PermissionsRepository
                };

                var validate = licenseClass.ValidateLicenseAdd(dataToAdd);
                if (validate != null)
                {
                    resp.Data = 0;
                    resp.Status = 500;
                    resp.Description = validate.Message;
                    return resp;
                }

                var licNumber = licenseClass.GetNewLicenseString();
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
                
                if (AppRepo.Insert(model))
                {
                    if (model.Permissions != null)
                    {
                        if (!licenseClass.InsertPermissions(model))
                            throw new Exception("Error in insert permissions");
                    }
                    resp.Data = model.Id;
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
                resp.Data = 0;
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
                    throw new Exception("Id license <= 0");
                
                resp.Data = AppRepo.EditLicense(id, licenseModel);

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
