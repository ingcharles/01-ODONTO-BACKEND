using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Models
{
    public class ValidateRefreshTokenResponse : BaseResponse
    {
        public Int64? UserId { get; set; }

    }
}
