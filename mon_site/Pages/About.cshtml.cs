using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mon_site.Pages
{
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost()
        {

        }
        public int Experience()
        {
            int debut = 2018;
            return DateTime.Now.Year - debut;
        }
    }
}


/*
      T3K5NIP3R
 */