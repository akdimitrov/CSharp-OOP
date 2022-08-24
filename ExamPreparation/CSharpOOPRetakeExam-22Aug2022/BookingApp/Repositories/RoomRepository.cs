using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories.Contracts;

namespace BookingApp.Repositories
{
    public class RoomRepository : IRepository<IRoom>
    {
        private List<IRoom> rooms;

        public RoomRepository()
        {
            this.rooms = new List<IRoom>();
        }

        public void AddNew(IRoom room)
        {
            this.rooms.Add(room);
        }

        public IRoom Select(string roomTypeName)
        {
            return this.rooms.FirstOrDefault(r => r.GetType().Name == roomTypeName);
        }

        public IReadOnlyCollection<IRoom> All()
        {
            return this.rooms.AsReadOnly();
        }
    }
}
