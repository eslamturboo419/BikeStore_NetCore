using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.VM
{
    public class RegisterVM
    {


        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
       

    }
}
