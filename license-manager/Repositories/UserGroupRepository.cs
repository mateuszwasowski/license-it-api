using System.Collections.Generic;
using System.Linq;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Repositories
{
    public class UserGroupRepository : Repository<UserGroup>, IUserGroupRepository
    {
        private readonly DataBaseContext _context;

        public UserGroupRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public bool Exist(int idUser, int idGroup)
        {
            return _context.UserGroup.FirstOrDefault(x => x.IdGroup == idGroup && x.IdUser == idUser) != null;
        }

        public UserGroup GetByIdUserIdGroup(int idUser, int idGroup)
        {
            return _context.UserGroup.FirstOrDefault(x => x.IdGroup == idGroup && x.IdUser == idUser);
        }

        public IEnumerable<UserGroupModel> GetUserGroupModelByIdGroup(int id)
        {
             return from userGroup in _context.UserGroup
                   join groupObj in _context.Group on userGroup.IdGroup equals groupObj.Id
                   where userGroup.IdGroup == id
                   select new UserGroupModel
                   {
                       Id = userGroup.Id,
                       IdUser = userGroup.IdUser,
                       IdGroup = userGroup.IdGroup,
                       GroupLogoUrl = groupObj!=null ? groupObj.LogoUrl : "",
                       GroupName = groupObj!=null ? groupObj.Name : ""
                   };
        }

        public IEnumerable<UserGroupModel> GetUserGroupModelByIdUser(int id)
        {
            return from userGroup in _context.UserGroup
                   join groupObj in _context.Group on userGroup.IdGroup equals groupObj.Id
                   where userGroup.IdUser == id
                   select new UserGroupModel
                   {
                       Id = userGroup.Id,
                       IdUser = userGroup.IdUser,
                       IdGroup = userGroup.IdGroup,
                       GroupLogoUrl = groupObj!=null ? groupObj.LogoUrl : "",
                       GroupName = groupObj!=null ? groupObj.Name : ""
                   };
        }
    }
}
