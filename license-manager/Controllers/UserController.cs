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
    public class UserController : Controller
    {
        // GET: api/User
        [HttpGet]
        [Route("api/User")]
        public IEnumerable<UserModel> Get()
        {
            IUserRepository userRepo = new UserRepository(new DataBaseContext());
            return userRepo.Get().Where(x => !x.IsDelete).Select(x => new UserModel
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
        [Route("api/User/{id}")]
        public ResponseModel<UserModel> Get(int id)
        {
            var response = new ResponseModel<UserModel>();

            try
            {
                IUserRepository userRepo = new UserRepository(new DataBaseContext());

                var user = userRepo.GetById(id);

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

        // POST api/User/Add
        [HttpPost]
        [AllowAnonymous]
        [Route("api/User/Add")]
        public ResponseModel<UserModel> Post([FromBody] UserModel value)
        {
            var response = new ResponseModel<UserModel>();
            try
            {
                IUserRepository userRepo = new UserRepository(new DataBaseContext());

                if (string.IsNullOrEmpty(value?.Email))
                {
                    response.Status = 200;
                    response.Description = "Wrong data...";
                    return response;
                }

                if (userRepo.ExistUserByEmail(value.Email))
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

                userRepo.Insert(user);

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

                IUserRepository userRepo = new UserRepository(new DataBaseContext());

                var user = userRepo.GetById(userModel.Id);

                user.IsDelete = true;


                if (userRepo.Update(user))
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

                IUserRepository userRepo = new UserRepository(new DataBaseContext());

                var user = userRepo.GetById(userModel.Id);

                if (!string.IsNullOrEmpty(userModel.Password))
                    user.Password = CryptoClass.CreateHash(userModel.Password);

                user.IsDelete = userModel.IsDelete;
                user.IsActive = userModel.IsActive;
                user.Email = userModel.Email;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;

                if (userRepo.Update(user))
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