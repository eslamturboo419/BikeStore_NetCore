using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.VM
{
    public class LoginVM
    {

        [Display(Name ="User Name")]
        public string UserName  { get; set; }

        [DataType(DataType.Password)]

        public string Password { get; set; }
        public bool RememberMe { get; set; }


    }
}
