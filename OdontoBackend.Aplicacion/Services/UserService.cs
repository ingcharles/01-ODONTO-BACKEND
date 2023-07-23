using OdontoBackend.Aplicacion.Mappers.Contracts;
using OdontoBackend.Aplicacion.Services.Contracts;
using OdontoBackend.Aplicacion.ViewModels;
using OdontoBackend.Aplication.Entities.Commands;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Domain.Contracts;
using OdontoBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            this._repository = repository;
            this._mapper = mapper;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        //Login User
        public Task<IQueryable<UserViewModel>> GetUserByCiPas(Task<UserByCiPasQuery> request)
        {
            var _request = _mapper.UserQueryToUserByCiPas(request);
            var _response = _repository.GetUserByCiPas(_request.FirstOrDefault()!);
            var _result = _response.Result?.Count() > 0 ? _mapper.UserQueryFromUserByCiPas(Task.FromResult(_response.Result.FirstOrDefault()!)) : default!;
            return Task.FromResult(_result);
        }
        public Task<IQueryable<UserViewModel>> SaveRegisterUser(Task<UserCommand> request)
        {
            var _request = _mapper.UserCommandToRegisterUser(request);
            var _response = _repository.SaveRegisterUser(_request.FirstOrDefault()!);
            var _result = _response.Result?.Count() > 0 ? _mapper.UserCommandFromRegisterUser(Task.FromResult(_response.Result.FirstOrDefault()!)) : default!;
            return Task.FromResult(_result);
        }

        public Task<IQueryable<User>> GetTokensCodUsuario(Task<UserByCodUsuarioQuery> request)
        {
            //var _request = _mapper.UserCommandToGetToken(request);
            //var _response = _repository.GetTokensCodUsuario(_request.FirstOrDefault()!);
            //var _result = _response.Result?.Count() > 0 ? _mapper.UserCommandFromGetToken(Task.FromResult(_response.Result.FirstOrDefault()!)) : default!;
            //return Task.FromResult(_result);
            var result = Enumerable.Empty<User>().AsQueryable();
            return Task.FromResult(result);
        }
        public Task<IQueryable<User>> UpdateTokensCodUsuario(Task<User> request)
        {
            Debug.WriteLine("request" + request);
            var _request = _mapper.UserCommandToUpdateTokens(request);
            var _response = _repository.UpdateTokensCodUsuario(_request.FirstOrDefault()!);
            var _result = _response.Result?.Count() > 0 ? _mapper.UserCommandFromUpdateTokens(Task.FromResult(_response.Result.FirstOrDefault()!)) : default!;
            return Task.FromResult(_result);
            return default;
        }

      
    }
}
