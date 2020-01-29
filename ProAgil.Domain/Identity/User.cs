
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ProAgil.Domain.Identity;

public class User : IdentityUser<int>
    {   
        [Column (TypeName ="nvarchar(150)")]
        public string NomeCompleto  { get; set;}
        public List<UserRole> UserRoles { get; set; }


    }