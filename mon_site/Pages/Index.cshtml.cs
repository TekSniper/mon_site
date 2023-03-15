using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace mon_site.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string buttonColor { get; set; }
        public List<MesCreas> listCreas = new List<MesCreas>();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public string GetButtonColor()
        {
            this.buttonColor = "btn-info";
            return this.buttonColor;
        }
        //public List<MesCreas> GetCreas()
        //{
        //    using (var cn = new SqlConnection(new Db_Connection().GetConnectionString()))
        //    {
        //        cn.Open();
        //        var cm = new SqlCommand("select top(5) * from TS_Crea order by id_crea desc", cn);
        //        var reader = cm.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            MesCreas mesCreas = new MesCreas();
        //            mesCreas.ID = reader.GetInt32(0);
        //            mesCreas.Type = reader.GetInt32(1);
        //            mesCreas.Designation = reader.GetString(2);
        //            mesCreas.Titre = reader.GetString(3);
        //            mesCreas.DateCreation = reader.GetDateTime(4);
        //            mesCreas.Url = reader.GetString(6);
        //            mesCreas.Url_byte = (byte[])reader[7];

        //            listCreas.Add(mesCreas);
        //        }

        //        return listCreas;
        //    }
        //}
    }

    public class Db_Connection
    {
        IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        string connectionString { get; set; }
        public Db_Connection()
        {
            this.connectionString = GetConfiguration().GetSection("ConnectionStrings").GetSection("TS_DbConnection").Value;
        }
        public string GetConnectionString()
        {
            return this.connectionString;
        }
    }
    public class MainClass
    {
        string jour = "1";
        int mois = DateTime.Now.Month;
        int annee = DateTime.Now.Year;
        List<string> listImg;
        public byte[] data { get; set; }
        public MainClass()
        {

        }
        //public List<string> GetCrea()
        //{
        //    using (var cn = new SqlConnection(new Db_Connection().GetConnectionString()))
        //    {
        //        int daysInMonth = DateTime.DaysInMonth(annee, mois);
        //        cn.Open();
        //        var cm = new SqlCommand("select top(5) * from TS_Crea order by id_crea desc", cn);
        //        var reader = cm.ExecuteReader();
        //        listImg = new List<string>();
        //        while (reader.Read())
        //        {
        //            listImg.Add(reader.GetString(6));
        //        }
        //        return listImg;
        //    }
        //}
    }
}