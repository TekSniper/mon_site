using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace mon_site.Pages.Services
{
    public class IndexModel : PageModel
    {
        private List<string> servicesParents;
        private Dictionary<int, string> services;
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
        private DataTable details { get; set; }
        List<Service_cl> listeServices = new List<Service_cl>();
        public void OnGet()
        {
        }
        public void OnPost() { }

        public List<string> GetParentServices()
        {
            servicesParents = new List<string>();
            servicesParents = new Service_cl().GetParentServices();
            return servicesParents;
        }
        public Dictionary<int, string> GetDictParentServices()
        {
            services = new Dictionary<int, string>();
            services = new Service_cl().GetDictParentServices();

            return services;
        }
        public List<Service_cl> GetDetails()
        {
            details = new DataTable();
            details = new Service_cl().GetDetails(int.Parse(Request.Query["id"]));

            foreach (DataRow row in details.Rows)
            {
                Service_cl service = new Service_cl();
                service.id = (int)row[0];
                service.designation = row[1].ToString();

                listeServices.Add(service);
            }

            return listeServices;
        }
    }
    public class Service_cl
    {
        private string connectionString { get; set; }
        private List<string> servicesParents;
        private Dictionary<int, string> services;
        private DataTable servicesTable { get; set; }
        public int id { get; set; }
        public string designation { get; set; }
        public decimal prix { get; set; }
        public string img { get; set; }
        private Dictionary<int, string> subSeriveList { get; set; }
        public Service_cl()
        {
            this.prix = 0.00M;
            this.connectionString = new Db_Connection().GetConnectionString();
        }
        public Dictionary<int, string> GetDictParentServices()
        {
            using (var cn = new SqlConnection(this.connectionString))
            {
                cn.Open();
                var cm = new SqlCommand("select * from TS_Service where parent is null", cn);
                var reader = cm.ExecuteReader();
                services = new Dictionary<int, string>();
                while (reader.Read())
                    services.Add((int)reader[0], reader.GetString("serv_designation"));

                return services;
            }
        }
        public List<string> GetParentServices()
        {
            using (var cn = new SqlConnection(this.connectionString))
            {
                cn.Open();
                var cm = new SqlCommand("select serv_designation from TS_Service where parent is null", cn);
                var reader = cm.ExecuteReader();
                servicesParents = new List<string>();
                while (reader.Read())
                {
                    servicesParents.Add(reader.GetString(0));
                }

                return servicesParents;
            }
        }
        public DataTable GetDetails(int parent)
        {
            using (var cn = new SqlConnection(this.connectionString))
            {
                cn.Open();
                var cm = new SqlCommand("select * from TS_Service where parent=@parent", cn);
                cm.Parameters.AddWithValue("@parent", parent);
                var adapter = new SqlDataAdapter(cm);
                servicesTable = new DataTable();
                adapter.Fill(servicesTable);

                return servicesTable;
            }
        }
        public Dictionary<int, string> GetSubServiceList()
        {
            using(var cn=new SqlConnection(this.connectionString))
            {
                cn.Open();
                var cm = new SqlCommand("select id_service,serv_designation,prix from TS_Service where parent is not null", cn);
                var reader = cm.ExecuteReader();
                subSeriveList = new Dictionary<int, string>();
                while(reader.Read())
                    subSeriveList.Add(reader.GetInt32(0), reader.GetString(1));
                

                return subSeriveList;
            }
        }
        public string GetDesignationService(int ID)
        {
            var read = "";
            using(var cn=new SqlConnection(this.connectionString))
            {
                cn.Open();
                var cm = new SqlCommand("select serv_designation from TS_Service where id_service=@id", cn);
                cm.Parameters.AddWithValue("@id", ID);
                var reader = cm.ExecuteReader();

                if(reader.Read())
                    read = reader.GetString(0);

                return read;
            }
        }
        public decimal GetPriceService(int ID)
        {
            var read = 0.00M;
            using (var cn = new SqlConnection(this.connectionString))
            {
                cn.Open();
                var cm = new SqlCommand("select prix from TS_Service where id_service=@id", cn);
                cm.Parameters.AddWithValue("@id", ID);
                var reader = cm.ExecuteReader();

                if (reader.Read())
                    read = reader.GetDecimal(0);

                return read;
            }
        }
    }
}
