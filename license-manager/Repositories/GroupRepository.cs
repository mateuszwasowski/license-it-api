using System.Collections.Generic;
using System.Linq;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly DataBaseContext _context;

        public GroupRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public bool ExistByName(string name)
        {
            return _context.Group.FirstOrDefault(x=>x.Name.Equals(name)) !=null;
        }

        public GroupModel GetGroupModelById(int id)
        {
            return _context.Group.Select(x=> new GroupModel{
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                IsDelete = x.IsDelete,
                Date = x.Date
            }).FirstOrDefault(x=>x.Id == id);
        }

        public IEnumerable<GroupModel> GetGroupsModel()
        {
            return _context.Group.Select(x=> new GroupModel{
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                IsDelete = x.IsDelete,
                Date = x.Date
            });
        }
    }
}
