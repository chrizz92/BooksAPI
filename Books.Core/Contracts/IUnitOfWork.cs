using Books.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        
 
        IPublisherRepository PublisherRepository { get; }
        IBookRepository BookRepository { get; }
        void Save();

        void DeleteDatabase();

        void MigrateDatabase();

        void FillDb();

     
    }
}
