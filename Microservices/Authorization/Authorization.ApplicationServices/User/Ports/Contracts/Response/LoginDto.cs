using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.ApplicationServices.User.Ports.Contracts.Response;

public class LoginDto
{
    public string UserName { get; set; }

    public string AccessToken { get; set; }
}
