using OdontoBackend.Aplicacion.ViewModels;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Mappers.Contracts
{
    public interface ICatalogoMapper
    {
        public IQueryable<Catalogo> CatalogoQueryToCatalogoCodUni(Task<CatalogoByCodUniQuery> source);
        public IQueryable<CatalogoAllViewModel> CatalogoToCatalogoVmByCodUni(Task<Catalogo> source);
    }
}
