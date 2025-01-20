using GreenZones.Models;

namespace GreenZones.DAL
{
    public interface IUnitofWork
    {
        public IRepository<User> UserRepository { get; }
        public IRepository<Session> SessionRepository { get; }
        public IRepository<ShotType> ShotTypeRepository { get; }
        public void Save();
        public void Dispose(bool disposing);
    }
}
