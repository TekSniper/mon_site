using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mon_site.Pages
{
    public class ContactModel : PageModel
    {
        public string ErrMessage { get; set; }
        public string SucMessage { get; set; }
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
    public class Contact
    {
        public int ID { get; set; }
        public string AuthorFistName { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string ObjectMsg { get; set; }
        public string MessageContent { get; set; }
        public string DateSend { get; set; }
        public string TimeSend { get; set; }
    }
}
