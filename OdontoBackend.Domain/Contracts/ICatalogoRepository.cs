using OdontoBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Contracts
{
    public interface ICatalogoRepository
    {
        Task<IQueryable<Catalogo>> GetCatalogoByCodUni(Catalogo request);
   
    }
}
