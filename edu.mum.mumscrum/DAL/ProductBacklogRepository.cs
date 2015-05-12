using edu.mum.mumscrum.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace edu.mum.mumscrum.DAL
{
    public class ProductBacklogRepository : IProductBacklogRepository, IDisposable
    {
        private MUMScrumContext context;

        public ProductBacklogRepository(MUMScrumContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProductBacklog> GetProductBacklogs()
        {
            return context.ProductBacklogs.ToList();
        }

        public ProductBacklog GetProductBacklogByID(int? id)
        {
            return context.ProductBacklogs.Find(id);
        }

        public void InsertProductBacklog(ProductBacklog productBacklog)
        {
            context.ProductBacklogs.Add(productBacklog);
        }

        public void DeleteProductBacklog(int productBacklogID)
        {
            ProductBacklog productBacklog = context.ProductBacklogs.Find(productBacklogID);
            context.ProductBacklogs.Remove(productBacklog);
        }

        public void UpdateProductBacklog(ProductBacklog productBacklog)
        {
            context.Entry(productBacklog).State = EntityState.Modified;
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