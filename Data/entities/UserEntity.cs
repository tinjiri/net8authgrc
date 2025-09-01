using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace authapinet8.Data.entities;

public class UserEntity : IdentityUser
{
   // [PersonalData]
   // [Column(TypeName = "nvarchar(100)")]
   // public string FirstName { get; set; }
   // [PersonalData]
   // [Column(TypeName = "nvarchar(100)")]
   // public string MiddleName { get; set; }
   // [PersonalData]
   // [Column(TypeName = "nvarchar(100)")]
   // public string LastName { get; set; }
    
       [PersonalData]
  [Column(TypeName = "nvarchar(100)")]
    public string FullName { get; set; }
 }