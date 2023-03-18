using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mon_site.Pages.Services;
using System.Data;
using System.Data.SqlClient;

namespace mon_site.Pages.Clients
{
    public class CommandeModel : PageModel
    {

        //public DataTable cmd_cart = new DataTable();
        Dictionary<int,string> subServices = new Dictionary<int, string>();
        public string ErrMessage { get; set; }
        public string SucMessage { get; set; }
        public int RowCount = 0;
        public DataTable CreateDataTable()
        {
            DataTable cmd_cart = new DataTable();
            cmd_cart.Columns.Add("ID_Service");
            cmd_cart.Columns.Add("Designation");
            cmd_cart.Columns.Add("Prix");

            return cmd_cart;
        }
        public void InsertIntoDataTable()
        {
            var tab = CreateDataTable();
        }
        public void OnGet()
        {
            try
            {

            }
            catch(Exception ex)
            {
                ErrMessage = ex.Message;
            }
        }
        public void OnPost() 
        {            
            try
            {
                var cart = CreateDataTable();
                cart.Rows.Add(Request.Form["service"],new Service_cl().GetDesignationService(int.Parse(Request.Form["service"])),new Service_cl().GetPriceService(int.Parse(Request.Form["service"])));
                RowCount = cart.Rows.Count;
            }
            catch(Exception ex)
            {
                ErrMessage = ex.Message;
                return;
            }
        }
        public Dictionary<int, string> GetSubServices()
        {
            try
            {
                subServices = new Services.Service_cl().GetSubServiceList();                
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
            }
            return subServices;
        }
    }

    public class Commande
    {
        public string NumCmd { get; set; }
        public int NumInd { get; set; }
        public DateTime Date { get; set; }
        public string HeureCmd { get; set; }
        public DateTime DateFin { get; set; }
        public int Deadline { get; set; }

        public bool UpdateNumCmd()
        {
            using(var cn=new SqlConnection(new Db_Connection().GetConnectionString()))
            {
                cn.Open();
                var cm = new SqlCommand("UpdateNumCmd", cn);
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                var i = cm.ExecuteNonQuery();

                if(i!=0) return true;
                else return false;
            }
        }

        public string GetNumCmd()
        {
            var isTrue = UpdateNumCmd();
            if (isTrue)
            {
                using(var cn=new SqlConnection(new Db_Connection().GetConnectionString()))
                {
                    cn.Open();
                    var cm = new SqlCommand("select * from souche_num_cmd", cn);
                    var reader = cm.ExecuteReader();
                    if (reader.Read())
                        this.NumCmd = reader.GetString(1) +"-"+ reader.GetInt32(2).ToString();
                }
                
            }
            else { }
            return this.NumCmd;
        }
    }
    public class DetailsCommande
    {
        public int Id { get; set; }
    }
}
