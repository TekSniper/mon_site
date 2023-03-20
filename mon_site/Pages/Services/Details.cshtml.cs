using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Collections.Generic;

namespace mon_site.Pages.Services
{
    public class DetailsModel : PageModel
    {
        public List<Service_cl> service_list = new List<Service_cl>();
        public string designationParent { get; set; }
        public string ErrMessage { get; set; }
        public void OnGet()
        {
            try
            {
                if (Request.Query["id"] != "1" && Request.Query["id"] != "2")
                {
                    ErrMessage = "Aucun élément trouvé concernant ce détail";
                }
                else
                {
                    using (var cn = new SqlConnection(new Db_Connection().GetConnectionString()))
                    {
                        cn.Open();
                        var cm = new SqlCommand("select * from TS_Service where parent=@parent", cn);
                        cm.Parameters.AddWithValue("@parent", int.Parse(Request.Query["id"]));
                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            Service_cl service_Cl = new Service_cl();
                            service_Cl.id = reader.GetInt32(0);
                            service_Cl.designation = reader.GetString(1);

                            if (reader[3].ToString() == "") { }
                            else
                                service_Cl.prix = reader.GetDecimal(3);

                            if (reader[4].ToString() == "") { }
                            else
                                service_Cl.img = reader.GetString(4);
                            service_list.Add(service_Cl);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ErrMessage = ex.Message;
                return;
            }                        
        }
        public void OnPost() 
        { 
        }
        public string GetDesignationParent()
        {
            this.designationParent = Request.Query["designation"].ToString();

            return designationParent;
        }
        //public string ConvertByteToStringUrl(byte[] data)
        //{
        //    //using (MemoryStream ms = new MemoryStream(data))
        //    //{
        //    //    return Image.FromStream(ms);
        //    //}
        //    var imgBase64 = Convert.ToBase64String(data);
        //    var imgUrl = 
        //}

    }
}


/*
      T3K5NIP3R
 */