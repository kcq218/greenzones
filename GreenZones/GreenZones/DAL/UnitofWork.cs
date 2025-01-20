﻿using GreenZones.Models;

namespace GreenZones.DAL
{
    public class UnitofWork : IUnitofWork
    {
        private DbAll01ProdUswest001Context _context = new DbAll01ProdUswest001Context();

        public UnitofWork(DbAll01ProdUswest001Context context, bool disposed)
        {
            _context = context;
            this.disposed = disposed;
        }

        public IRepository<User> UserRepository => new Repository<User>(_context);

        public IRepository<Session> SessionRepository => new Repository<Session>(_context);

        public IRepository<ShotType> ShotTypeRepository => new Repository<ShotType>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}