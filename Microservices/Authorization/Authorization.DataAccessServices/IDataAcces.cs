using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.DataAccessServices
{
    public interface IDataAcces
    {
        Task<uint?> ValidateToken(string token);
    }
}
