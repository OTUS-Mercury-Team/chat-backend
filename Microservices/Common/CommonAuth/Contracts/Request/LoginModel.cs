﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACommonAuth.Contracts.Request;

public record LoginModel
{
    [Required]
    public string UserName { get; init; }

    [Required]
    public string Password { get; init; }
}
