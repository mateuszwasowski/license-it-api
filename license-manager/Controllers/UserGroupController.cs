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
    [Authorize]
    public class UserGroupController : Controller
    {
        public IUserGroupRepository AppRepo { get; set; } = new UserGroupRepository(new DataBaseContext());
 
        // GET: api/UserGroup/GetByIdUser/5
        [HttpGet]
        [Route("api/UserGroup/GetByIdUser/{id}")]
        public ResponseModel<IEnumerable<UserGroupModel>> GetById(int id)
        {
            var resp = new ResponseModel<IEnumerable<UserGroupModel>>();

            try
            {
                var app = AppRepo.GetUserGroupModelByIdUser(id).ToList();

                if (app != null)
                {
                    resp.Data = app;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 500;
                    resp.Data = null;
                    resp.Description = "Not found";
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

        // GET: api/UserGroup/GetByIdGroup/5
        [HttpGet]
        [Route("api/UserGroup/GetByIdGroup/{id}")]
        public ResponseModel<IEnumerable<UserGroupModel>> GetByIdGroup(int id)
        {
            var resp = new ResponseModel<IEnumerable<UserGroupModel>>();

            try
            {
                var app = AppRepo.GetUserGroupModelByIdGroup(id);

                if (app != null)
                {
                    resp.Data = app;
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 500;
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

        // POST: api/UserGroup/Add
        [HttpPost]
        [Route("api/UserGroup/Add")]
        public ResponseModel<int> Post([FromBody]UserGroupModel dataToAdd)
        {
            var resp = new ResponseModel<int>();

            try
            {
                if (dataToAdd == null)
                {
                    throw new Exception("Data is null");
                }

                var model = new UserGroup()
                {
                    IdUser = dataToAdd.IdUser,
                    IdGroup = dataToAdd.IdGroup
                };

                if (AppRepo.Exist(model.IdUser,model.IdGroup))
                    throw new Exception("User in group already exist");

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

        // DELETE: api/UserGroup/Delete/2/1
        [HttpDelete]
        [Route("api/UserGroup/Delete/{idUser}/{idGroup}")]
        public ResponseModel<bool> Delete(int idUser, int idGroup)
        {
            var resp = new ResponseModel<bool>();

            try
            {
                if (idUser == 0 || idGroup == 0)
                    throw new Exception("Id User or Id Group not found");

                var groupObj = AppRepo.GetByIdUserIdGroup(idUser,idGroup);

                if (groupObj != null)
                {
                    AppRepo.Delete(groupObj);

                    resp.Status = 200;
                    resp.Data = true;
                    resp.Description = "OK";
                }
                else
                    throw new Exception("Id not exist");

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