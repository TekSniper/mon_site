using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace mon_site.Pages
{
    public class ContactModel : PageModel
    {
        public string ErrMessage { get; set; }
        public string SucMessage { get; set; }
        Contact contactInfo = new Contact();
        public void OnGet()
        {
        }
        public void OnPost() 
        { 
            contactInfo.AuthorFistName = Request.Form["prenom"];
            contactInfo.AuthorName = Request.Form["nom"];
            contactInfo.AuthorEmail = Request.Form["email"];
            contactInfo.ObjectMsg = Request.Form["objet"];
            contactInfo.MessageContent = Request.Form["message"];
            contactInfo.DateSend = DateTime.Now.Date;
            contactInfo.TimeSend = DateTime.Now.ToShortTimeString();
            if(contactInfo.ObjectMsg.Length == 0 || contactInfo.MessageContent.Length == 0 || contactInfo.AuthorFistName.Length == 0 || contactInfo.AuthorName.Length == 0
                || contactInfo.AuthorEmail.Length == 0)
            {
                ErrMessage = "Tous les champs sont obligatoires !";
                return;
            }
            else
            {
                try
                {
                    using (var cn = new SqlConnection(new Db_Connection().GetConnectionString()))
                    {
                        cn.Open();
                        var cm = new SqlCommand("insert into TS_Contact(prenom_auteur,nom_auteur,email,objet,txt_message,date_msg,heure_msg) " +
                            "values(@prenom,@nom,@email,@objet,@msg,@date,@heure)", cn);
                        cm.Parameters.AddWithValue("@prenom", contactInfo.AuthorFistName);
                        cm.Parameters.AddWithValue("@nom", contactInfo.AuthorName);
                        cm.Parameters.AddWithValue("@email", contactInfo.AuthorEmail);
                        cm.Parameters.AddWithValue("@objet", contactInfo.ObjectMsg);
                        cm.Parameters.AddWithValue("@msg", contactInfo.MessageContent);
                        cm.Parameters.AddWithValue("@date", contactInfo.DateSend);
                        cm.Parameters.AddWithValue("@heure", Convert.ToDateTime(contactInfo.TimeSend));
                        cm.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    ErrMessage = ex.Message;
                    return;
                }
                contactInfo.AuthorFistName = ""; contactInfo.AuthorName = ""; contactInfo.AuthorEmail = ""; contactInfo.ObjectMsg = ""; contactInfo.MessageContent = "";
                SucMessage = "Message envoyé avec succès";
                //Response.Redirect("/Contact");
            }
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
        public DateTime DateSend { get; set; }
        public string TimeSend { get; set; }
    }
}


/*
      T3K5NIP3R
 */