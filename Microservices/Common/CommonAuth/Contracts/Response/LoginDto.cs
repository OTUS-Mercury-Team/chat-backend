using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonAuth.Contracts.Response;

public class LoginDto
{
    public string UserName { get; set; }

    public string AccessToken { get; set; }

    public eLoginResult LoginResult { get; set; }
}

public enum eLoginResult
{
    Success,
    BadPassword,
    BadLogin,
    ServerError

}