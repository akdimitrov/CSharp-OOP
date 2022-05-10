namespace Gym.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Gym.Models.Equipment.Contracts;
    using Gym.Repositories.Contracts;

    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly List<IEquipment> models;

        public EquipmentRepository()
        {
            models = new List<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models => models.AsReadOnly();

        public void Add(IEquipment model)
        {
            models.Add(model);
        }

        public bool Remove(IEquipment model)
        {
            return models.Remove(model);
        }

        public IEquipment FindByType(string type)
        {
            return models.FirstOrDefault(x => x.GetType().Name == type);
        }
    }
}
