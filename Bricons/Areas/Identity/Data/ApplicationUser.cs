﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bricons.Models;
using Microsoft.AspNetCore.Identity;

namespace Bricons.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}

