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
            return _context.UserGroup.FirstOrDefault(x=>x.IdGroup == idGroup && x.IdUser == idUser)!=null;
        }

        public UserGroup GetByIdUserIdGroup(int idUser, int idGroup)
        {
            return _context.UserGroup.FirstOrDefault(x=>x.IdGroup == idGroup && x.IdUser == idUser);
        }

        public IEnumerable<UserGroupModel> GetUserGroupModelByIdGroup(int id)
        {
            return _context.UserGroup.Where(x=>x.IdGroup == id).Select(x=> new UserGroupModel{
                Id = x.Id,
                IdUser = x.IdUser,
                IdGroup = x.IdGroup
            });
        }

        public IEnumerable<UserGroupModel> GetUserGroupModelByIdUser(int id)
        {
            return _context.UserGroup.Where(x=>x.IdUser == id).Select(x=> new UserGroupModel{
                Id = x.Id,
                IdUser = x.IdUser,
                IdGroup = x.IdGroup
            });
        }
    }
}
