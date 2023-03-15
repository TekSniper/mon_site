using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace mon_site.Pages
{
    public class CreasModel : PageModel
    {
        public Dictionary<int, string> listTypeCrea;
        public void OnGet()
        {
            if (Request.Query["Id"] == "")
            {

            }
            else
            {

            }
        }
        public Dictionary<int, string> GetlistType()
        {
            using(var cn=new SqlConnection(new Db_Connection().GetConnectionString()))
            {
                cn.Open();
                var cm = new SqlCommand("select * from TS_TypeCrea", cn);
                var reader = cm.ExecuteReader();
                listTypeCrea = new Dictionary<int, string>();
                while (reader.Read())
                    listTypeCrea.Add(reader.GetInt32(0),reader.GetString(1));

                return listTypeCrea;
            }
        }
    }

    public class MesCreas
    {
        public int ID { get; set; }
        public int Type { get; set; }
        public string Titre { get; set; }
        public string Designation { get; set; }
        public DateTime DateCreation { get; set; }
        public string Url { get; set; }
        public byte[] Url_byte { get; set; }
    }
    public class TypeCrea
    {
        public int ID { get; set; }
        public string Designation { get; set; }
    }
}
