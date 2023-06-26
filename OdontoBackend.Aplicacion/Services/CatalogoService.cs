using OdontoBackend.Aplicacion.Mappers.Contracts;
using OdontoBackend.Aplicacion.Services.Contracts;
using OdontoBackend.Aplicacion.ViewModels;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Domain.Contracts;
using OdontoBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly ICatalogoRepository _repository;
        private readonly ICatalogoMapper _mapper;
        public CatalogoService(ICatalogoRepository repository, ICatalogoMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<IQueryable<CatalogoAllViewModel>> GetCatalogoByCodUni(Task<CatalogoByCodUniQuery> request)
        {
            var _request = _mapper.CatalogoQueryToCatalogoCodUni(request);
            var _response = _repository.GetCatalogoByCodUni(_request.FirstOrDefault()!);
            var _result = _response.Result?.Count() > 0 ? _mapper.CatalogoToCatalogoVmByCodUni(Task.FromResult(_response.Result.FirstOrDefault()!)) : default!;
            return Task.FromResult(_result);
        }
       

    }
}
