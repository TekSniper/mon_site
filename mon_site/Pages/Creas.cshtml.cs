using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace mon_site.Pages
{
    public class CreasModel : PageModel
    {
        public Dictionary<int, string> listTypeCrea;
        public List<TypeCrea> typeCreas = new List<TypeCrea>();
        public List<MesCreas> listCreas = new List<MesCreas>();
        public string ErMessage { get; set; }
        public string SuMessage { get; set; }
        public void OnGet()
        {
            if (Request.Query["Id"] == "all")
            {
                try
                {
                    using (var cn = new SqlConnection(new Db_Connection().GetConnectionString()))
                    {
                        cn.Open();
                        var cm = new SqlCommand("select * from TS_Crea", cn);
                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            MesCreas mesCreas = new MesCreas();
                            mesCreas.ID = reader.GetInt32(0);
                            mesCreas.Type = reader.GetInt32(1);
                            mesCreas.Designation = reader.GetString(2);
                            mesCreas.Titre = reader.GetString(3);
                            mesCreas.DateCreation = (DateTime)reader[4];
                            mesCreas.Url_byte = (byte[])reader[7];
                            listCreas.Add(mesCreas);
                        }
                    }
                }
                catch(Exception ex)
                {
                    ErMessage = ex.Message;
                    return;
                }                
            }
            if (Request.Query["Id"] != "all")
            {
                try
                {
                    using (var cn = new SqlConnection(new Db_Connection().GetConnectionString()))
                    {
                        cn.Open();
                        var cm = new SqlCommand("select * from TS_Crea where id_type=@type", cn);
                        cm.Parameters.AddWithValue("@type", int.Parse(Request.Query["Id"]));
                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            MesCreas mesCreas = new MesCreas();
                            mesCreas.ID = reader.GetInt32(0);
                            mesCreas.Type = reader.GetInt32(1);
                            mesCreas.Designation = reader.GetString(2);
                            mesCreas.Titre = reader.GetString(3);
                            mesCreas.DateCreation = (DateTime)reader[4];
                            mesCreas.Url_byte = (byte[])reader[7];
                            listCreas.Add(mesCreas);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErMessage = ex.Message;
                    return;
                }
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
        public List<TypeCrea> GetTypeList()
        {
            using(var cn=new SqlConnection(new Db_Connection().GetConnectionString()))
            {
                cn.Open();
                var cm = new SqlCommand("select * form TS_TypeCrea", cn);
                var reader = cm.ExecuteReader();
                while (reader.Read())
                {
                    TypeCrea type = new TypeCrea();
                    type.ID = reader.GetInt32(0);
                    type.Designation = reader.GetString(1);
                    typeCreas.Add(type);
                }

                return typeCreas;
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

/*
      T3K5NIP3R
 */