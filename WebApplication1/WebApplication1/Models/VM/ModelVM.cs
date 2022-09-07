using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ModelVM
    {

        public Model model  { get; set; } 
        public IEnumerable<Make>   makes   { get; set; }

        public IEnumerable<SelectListItem> CselectListItems(IEnumerable<Make> items)
        {
            List<SelectListItem> maklist = new List<SelectListItem>();

            SelectListItem sli = new SelectListItem
            {
                Text = "---  Select ---",
                Value = "0"
            };
            maklist.Add(sli);
            foreach (Make make  in items)
            {
                sli = new SelectListItem
                {
                    Text = make.Name,
                    Value = make.Id.ToString()
                };
                maklist.Add(sli);
            }
            return maklist;
           
        }



    }
}
