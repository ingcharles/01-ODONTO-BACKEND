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
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUserMapper _mapper;
        public UserService(IUserRepository repository, IUserMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<IQueryable<UserViewModel>> getUserByCiPas(Task<UserByCiPasQuery> request)
        {
            var _request = _mapper.UserQueryToUserByCiPas(request);
            var _response = _repository.getUserByCiPas(_request.FirstOrDefault()!);
            var _result = _response.Result?.Count() > 0 ? _mapper.UserQueryFromUserByCiPas(Task.FromResult(_response.Result.FirstOrDefault()!)) : default!;
            return Task.FromResult(_result);
        }
       

    }
}
