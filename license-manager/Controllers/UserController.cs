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
    public class UserController : Controller
    {

        public IUserRepository UserRepo {get;set;} = new UserRepository(new DataBaseContext());

        // GET: api/User/Get
        [HttpGet]
        [Route("api/User/Get")]
        public IEnumerable<UserModel> Get()
        {
            return UserRepo.Get().Where(x => !x.IsDelete).Select(x => new UserModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Password = null,
                Email = x.Email,
                IsActive = x.IsActive,
                IsDelete = x.IsDelete
            });
        }

        // GET api/User/5
        [HttpGet]
        [Route("api/User/GetById/{id}")]
        public ResponseModel<UserModel> Get(int id)
        {
            var response = new ResponseModel<UserModel>();

            try
            {
                var user = UserRepo.GetById(id);

                if (user == null || user.IsDelete)
                {
                    response.Status = 200;
                    response.Description = "User not exist or is deleted";
                    return response;
                }

                response.Status = 200;
                response.Data = new UserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = null,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    IsDelete = user.IsDelete
                };
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Description = $"Critical error: {ex.Message}";
                response.Data = null;
            }

            return response;
        }

         // GET: api/User/GetUsersByIdGroup/5
        [HttpGet]
        [Route("api/User/GetUsersByIdGroup/{id}")]
        public ResponseModel<IEnumerable<UserModel>> GetUserListByIdGroup(int id)
        {
            var resp = new ResponseModel<IEnumerable<UserModel>>();

            try
            {
                var app = UserRepo.GetUsersByIdGroup(id);

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

        // POST api/User/Add
        [HttpPost]
        [AllowAnonymous]
        [Route("api/User/Add")]
        public ResponseModel<UserModel> Post([FromBody] UserModel value)
        {
            var response = new ResponseModel<UserModel>();
            try
            {
                if (value == null || string.IsNullOrEmpty(value?.Email))
                {
                    response.Status = 200;
                    response.Description = "Wrong data...";
                    return response;
                }

                if (UserRepo.ExistUserByEmail(value.Email))
                {
                    response.Status = 200;
                    response.Description = "This user already exist";
                    return response;
                }

                var pass = value.Password;
                value.Password = CryptoClass.CreateHash(pass);

                var user = new User
                {
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    Password = value.Password,
                    Email = value.Email,
                    IsActive = value.IsActive
                };

                UserRepo.Insert(user);

                value.Id = user.Id;
                value.Password = null;

                response.Status = 200;
                response.Description = "OK";
                response.Data = value;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Description = $"Critical error: {ex.Message}";
                response.Data = null;
            }
            return response;
        }

        // PUT api/User/Delete
        [HttpPut]
        [Route("api/User/Delete")]
        public ResponseModel<bool> DeleteUser([FromBody] UserModel userModel)
        {
            var response = new ResponseModel<bool>();

            try
            {
                if (userModel == null)
                {
                    response.Status = 200;
                    response.Description = "Wrong data...";
                    response.Data = false;
                    return response;
                }

                var user = UserRepo.GetById(userModel.Id);

                user.IsDelete = true;


                if (UserRepo.Update(user))
                {
                    response.Status = 200;
                    response.Description = "OK";
                    response.Data = true;
                    return response;
                }

                response.Status = 500;
                response.Description = "Error";
                response.Data = false;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Description = $"Critical error: {ex.Message}";
                response.Data = false;
                return response;
            }
        }

        // PUT api/User/Edit
        [HttpPut]
        [Route("api/User/Edit")]
        public ResponseModel<bool> EditUser([FromBody] UserModel userModel)
        {
            var response = new ResponseModel<bool>();
            try
            {
                if (userModel == null)
                {
                    response.Status = 200;
                    response.Description = "Wrong data...";
                    response.Data = false;
                    return response;
                }
                var user = UserRepo.GetById(userModel?.Id ?? 0);

                if (user == null)
                    throw new Exception("User not found");

                if(!user.Password.Equals(CryptoClass.CreateHash(userModel.OldPassword)))
                    throw new Exception("User's password is incorrect");

                if (!string.IsNullOrEmpty(userModel.Password))
                    user.Password = CryptoClass.CreateHash(userModel.Password);

                user.IsDelete = userModel.IsDelete;
                
                user.IsActive = userModel.IsActive;

                if(userModel.Email !=null)
                    user.Email = userModel.Email;

                if(userModel.FirstName !=null)
                    user.FirstName = userModel.FirstName;

                if(userModel.LastName !=null)
                    user.LastName = userModel.LastName;

                if (UserRepo.Update(user))
                {
                    response.Status = 200;
                    response.Description = "OK";
                    response.Data = true;
                    return response;
                }
                response.Status = 500;
                response.Description = "Error";
                response.Data = false;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Description = $"Critical error: {ex.Message}";
                response.Data = false;
                return response;
            }
        }
    }
}