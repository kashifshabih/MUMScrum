using System;
using edu.mum.mumscrum.Models;

namespace edu.mum.mumscrum.DAL
{
    public class UnitOfWork : IDisposable
    {
        private MUMScrumContext context = new MUMScrumContext();
        private GenericRepository<ProductBacklog> productBacklogRepository;
        private GenericRepository<UserStory> userStoryRepository;

        public GenericRepository<ProductBacklog> ProductBacklogRepository
        {
            get
            {

                if (this.productBacklogRepository == null)
                {
                    this.productBacklogRepository = new GenericRepository<ProductBacklog>(context);
                }
                return productBacklogRepository;
            }
        }

        public GenericRepository<UserStory> UserStoryRepository
        {
            get
            {

                if (this.userStoryRepository == null)
                {
                    this.userStoryRepository = new GenericRepository<UserStory>(context);
                }
                return userStoryRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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