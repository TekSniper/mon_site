using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mon_site.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public int currency { get; set; }
        public void OnGet()
        {
        }
        public void OnPost() { }

        public int CurrencyRates()
        {
            this.currency = 2250;

            return this.currency;
        }
    }
}
