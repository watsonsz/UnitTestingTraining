using Moq;
using RPGCombat.Application.Classes;
using RPGCombat.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Tests.Mocks
{
    public class MockEntityManagementService
    {
        public static Mock<IEntityManagementService<Entity>> GetEntityService()
        {
            var Entities = new List<Entity>
            {
                new Character(),
                new Character(),
                new Character()
            };
            var mockService = new Mock<IEntityManagementService<Entity>>();
            mockService.Setup(r => r.GetAll()).Returns(Entities);
            mockService.Setup(r => r.AddEntity(It.IsAny<Entity>())).Returns((Entity entity) =>
            {
                Entities.Add(entity);
                return Task.CompletedTask;
            });
            mockService.Setup(r => r.GetEntity(It.IsAny<Guid>())).Returns((Entity entity) =>
            {
               var retrievedEntity = Entities.FirstOrDefault(q => q.Id == entity.Id);
                return retrievedEntity;
            });
            mockService.Setup(r=>r.DeleteEntity(It.IsAny<Guid>())).Returns((Entity entity) =>
            {
                var entityToDelete = Entities.FirstOrDefault(q => q.Id == entity.Id);
                Entities.Remove(entityToDelete);
                return Task.CompletedTask;
            });

            return mockService;
        }
    }
}
