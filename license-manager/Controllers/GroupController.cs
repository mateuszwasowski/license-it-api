using System;
using System.Collections.Generic;
using System.Linq;
using licensemanager.Classes;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models;
using licensemanager.Models.AppModel;
using licensemanager.Repositories;
using licensemanager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace licensemanager.Controllers
{
    [Produces("application/json")]
    [AllowAnonymous]
    public class GroupController : Controller
    {
        public IGroupRepository AppRepo { get; set; } = new GroupRepository(new DataBaseContext());

        // GET: api/Groups/Get
        [HttpGet]
        [Route("api/Groups/Get")]
        public ResponseModel<IEnumerable<GroupModel>> Get()
        {
            var resp = new ResponseModel<IEnumerable<GroupModel>>();

            try
            {
                var appList = AppRepo.GetGroupsModel();

                if (appList != null && appList.Any())
                {
                    resp.Data = appList;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Description = "No groups";
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

        // GET: api/Groups/GetById/5
        [HttpGet]
        [Route("api/Groups/GetById/{id}")]
        public ResponseModel<GroupModel> GetById(int id)
        {
            var resp = new ResponseModel<GroupModel>();

            try
            {
                var app = AppRepo.GetGroupModelById(id);

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
                    resp.Description = "Group not found";
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

        // POST: api/Groups/Add
        [HttpPost]
        [Route("api/Groups/Add")]
        public ResponseModel<int> Post([FromBody]GroupModel dataToAdd)
        {
            var resp = new ResponseModel<int>();

            try
            {
                if (dataToAdd == null)
                {
                    throw new Exception("Data is null");
                }

                var model = new Group()
                {
                    Name = dataToAdd.Name,
                    Description = dataToAdd.Description,
                    IsActive = dataToAdd.IsActive,
                    IsDelete = dataToAdd.IsDelete,
                    Date = dataToAdd.Date
                };

                if (AppRepo.ExistByName(model.Name))
                    throw new Exception("Group already exist");

                if (AppRepo.Insert(model))
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

        // PUT: api/Groups/Edit
        [HttpPut]
        [Route("api/Groups/Edit")]
        public ResponseModel<bool> Put([FromBody]GroupModel applicationData)
        {
            var resp = new ResponseModel<bool>();

            try
            {
                if (applicationData == null)
                    throw new Exception("Data is null");

                var groupObj = AppRepo.GetById(applicationData.Id);

                if (groupObj != null)
                {
                    groupObj.Id = applicationData.Id;
                    
                    if(applicationData.Name !=null)
                        groupObj.Name = applicationData.Name;

                    if(applicationData.Description !=null)
                        groupObj.Description = applicationData.Description;

                    if(applicationData.IsActive !=null)
                        groupObj.IsActive = applicationData.IsActive;

                    if(applicationData.IsDelete !=null)
                        groupObj.IsDelete = applicationData.IsDelete;

                    if(applicationData.Date !=null)
                        groupObj.Date = applicationData.Date;

                    AppRepo.Update(groupObj);

                    resp.Status = 200;
                    resp.Data = true;
                    resp.Description = "OK";

                }
                else
                {
                    throw new Exception("Group not exist");
                }
            }
            catch (Exception ex)
            {
                resp.Status = 500;
                resp.Description = $"Error: {ex.Message}";
                resp.Data = true;
            }

            return resp;
        }

        // DELETE: api/Groups/Delete/2
        [HttpDelete]
        [Route("api/Groups/Delete/{id}")]
        public ResponseModel<bool> Delete(int id)
        {
            var resp = new ResponseModel<bool>();

            try
            {
                if (id == 0)
                    throw new Exception("Id not found");

                var groupObj = AppRepo.GetById(id);

                if (groupObj != null)
                {
                    AppRepo.Delete(groupObj);

                    resp.Status = 200;
                    resp.Data = true;
                    resp.Description = "OK";
                }
                else
                    throw new Exception("id not exist");

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