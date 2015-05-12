using edu.mum.mumscrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace edu.mum.mumscrum.DAL
{
    public interface IProductBacklogRepository : IDisposable
    {
        IEnumerable<ProductBacklog> GetProductBacklogs();
        ProductBacklog GetProductBacklogByID(int? productBacklogId);
        void InsertProductBacklog(ProductBacklog productBacklog);
        void DeleteProductBacklog(int productBacklogID);
        void UpdateProductBacklog(ProductBacklog productBacklog);
        void Save();
    }
}