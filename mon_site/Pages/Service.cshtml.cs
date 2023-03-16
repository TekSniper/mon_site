using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace mon_site.Pages
{
    public class ServicesModel : PageModel
    {
        private string connectionString { get; set; }
        private List<string> servicesParents;
        private Dictionary<int, string> services;
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
        private DataTable details { get; set; }
        List<Service_cl> listeServices = new List<Service_cl>();
        public ServicesModel() 
        {
            //this.connectionString = GetConfiguration().GetSection("ConnectionStrings").GetSection("TS_DbConnection").Value;
        }
        public void OnGet()
        {
            //int id = int.Parse(Request.Query["id"]);
            //new 
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
            details= new DataTable();
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
        private DataTable servicesTable { get;set; }
        public int id { get; set; }
        public string designation { get; set; }
        public Service_cl() 
        {
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
                    services.Add((int)reader[0], reader["serv_designation"].ToString());

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
                    servicesParents.Add(reader[0].ToString());
                }

                return servicesParents;
            }
        }
        public DataTable GetDetails(int parent)
        {
            using(var cn=new SqlConnection(this.connectionString))
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
    }
}