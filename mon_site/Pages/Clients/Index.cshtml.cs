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

    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public char Sexe { get; set; }
    }
}
