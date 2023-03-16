using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace mon_site.Pages.Clients
{
    public class CommandeModel : PageModel
    {

        //public DataTable cmd_cart = new DataTable();
        public DataTable CreateDataTable()
        {
            DataTable cmd_cart = new DataTable();
            cmd_cart.Columns.Add("ID_Service");
            cmd_cart.Columns.Add("Designation");

            return cmd_cart;
        }
        public void InsertIntoDataTable()
        {
            var tab = CreateDataTable();

        }
        public void OnGet()
        {

        }
        public void OnPost() 
        {
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
}
