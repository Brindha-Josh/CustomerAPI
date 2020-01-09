using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CustomerMgmt.Controllers
{
    public class DbCountry
    {
      SqlConnection con = new SqlConnection("Data Source=mysqlserver14.database.windows.net;Initial Catalog=PersonDB;User ID=azureuser;Password=vinayagar0113*;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //Get country List
        public DataSet GetCountry()
        {
            SqlCommand com = new SqlCommand("Sp_Country", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet GetCity(string CountryName)
        {
            SqlCommand com = new SqlCommand("Sp_City", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CountryName", CountryName);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}