using Microsoft.AspNetCore.Identity;

namespace Web.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
        }

        public ApplicationUser(string userName) : base(userName)
        {
        }

        //[PersonalData]
        //public string FullName { get; set; }
    }
}
