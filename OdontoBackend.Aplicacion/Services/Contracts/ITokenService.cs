using OdontoBackend.Aplicacion.ViewModels.User;
using OdontoBackend.Aplication.Entities.Commands;
using OdontoBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Services.Contracts
{
    public interface ITokenService
    {

        Task<Tuple<string, string>> GenerateTokensAsync(UserViewModel user);
        Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task<bool> RemoveRefreshTokenAsync(UserViewModel user);
    }
}
