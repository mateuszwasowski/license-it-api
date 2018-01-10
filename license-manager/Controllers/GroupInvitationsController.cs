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
    public class GroupInvitationsController : Controller
    {
        private IGroupInvitationsRepository GroupInvitationsRepository { get; set; } = new GroupInvitationsRepository(new DataBaseContext());
        private IUserGroupRepository UserGroupRepository { get; set; } = new UserGroupRepository(new DataBaseContext());
        private IUserRepository UserRepository { get; set; } = new UserRepository(new DataBaseContext());

        // POST: api/GroupInvitations/InviteUser
        [HttpPost]
        [Authorize]
        [Route("api/GroupInvitations/InviteUser")]
        public ResponseModel<int> Post([FromBody]GroupInvitationsModel dataToAdd)
        {
            var resp = new ResponseModel<int>();

            try
            {
                if (dataToAdd == null)
                {
                    throw new Exception("Data is null");
                }
                if(GroupInvitationsRepository.Exist(dataToAdd)){
                    throw new Exception("User has already been invited or exists in a group");
                }

                var model = new GroupInvitations
                {
                    Name = dataToAdd.Name,
                    Date = DateTime.Now,
                    Email = dataToAdd.Email,
                    GroupId = dataToAdd.GroupId,
                    IdUserInviting = dataToAdd.IdUserInviting,
                };

                model.Token = GenerateToken(model).Replace("/","").Replace("\\","");

                if (GroupInvitationsRepository.Insert(model))
                {
                    var mail = new MailClass();
                    mail.SendMail(model.Token,model.Email);

                    resp.Status = 200;
                    resp.Description = $"OK";
                    resp.Data = model.Id;
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

         // GET: api/GroupInvitations/Invite/token
        [HttpGet]
        [AllowAnonymous]
        [Route("api/GroupInvitations/Invite/{token}")]
        public ResponseModel<string> GetById(string token)
        {
            var resp = new ResponseModel<string>();

            try
            {
                var groupInvite = GroupInvitationsRepository.GetByToken(token);

                if (groupInvite != null)
                {
                    var user = UserRepository.GetUserByEmail(groupInvite.Email);

                    if(user==null)
                        throw new Exception("User not exist");

                     var model = new UserGroup()
                    {
                        IdUser = user.Id,
                        IdGroup = groupInvite.GroupId
                    };

                    if (UserGroupRepository.Exist(model.IdUser,model.IdGroup))
                        throw new Exception("User already exist in the group");

                    if (UserGroupRepository.Insert(model))
                    {
                        resp.Data = model.Id.ToString();
                        resp.Status = 200;
                        resp.Description = "OK";
                    }
                    else
                    {
                        throw new Exception("Not inserted");
                    }
                    resp.Data = "OK";
                    resp.Status = 200;
                    resp.Description = "OK";
                }
                else
                {
                    resp.Status = 200;
                    resp.Data = null;
                    resp.Description = "GroupInvitations not found";
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

        private string GenerateToken(GroupInvitations hash)
        {
            return CryptoClass.CreateHash(GetTokenHash(hash));
        }

        private string GetTokenHash(GroupInvitations model){
            return $"{model.Name}/{model.Date}/{model.GroupId}/{model.Email}";
        }
    }
}