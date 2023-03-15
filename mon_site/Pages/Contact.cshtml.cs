using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mon_site.Pages
{
    public class ContactModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost() 
        { 
        }

        public string GetBodyColor()
        {
            return "#013C4E";
        }
    }
}
