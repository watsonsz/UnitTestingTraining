using RPGCombat.Application.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Contracts
{
    public interface IEntityManagementService<T> where T : Entity
    {
        public List<T> GetAll();
        public Task AddEntity(object entity);
        public Task DeleteEntity(Guid id);
        public Task<T> GetEntity(Guid id);
    }
}
