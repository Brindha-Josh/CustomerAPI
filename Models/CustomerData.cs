using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMgmt.Models
{
    public class CustomerData
    {
        private readonly string connectionString = "Data Source=mysqlserver14.database.windows.net;Initial Catalog = PersonDB; User ID = azureuser; Password=vinayagar0113*;Connect Timeout = 60; Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
    //Get All Customer
        public IEnumerable<Customer>GetAllCustomer()
        {
            List<Customer> CustList = new List<Customer>();
            using (SqlConnection con=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllCustomer", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    Customer cust = new Customer();
                   cust.Id = Convert.ToInt32(dr["Id"].ToString());
                    cust.Name = dr["Name"].ToString();
                   // cust.Age = dr["Age"].ToString();
                    cust.Age = Convert.ToInt32(dr["Age"].ToString());
                    cust.Address = dr["Address"].ToString();
                   
                    CustList.Add(cust);
                }
                con.Close();
            }
            return CustList;
                }
    //To Insert Customer
    public void InsertCustomer(Customer cust)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_InsertCustomer", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Name", cust.Name);
            cmd.Parameters.AddWithValue("@Age", cust.Age);
            cmd.Parameters.AddWithValue("@Address", cust.Address);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //To Update Customer
        public void UpdateCustomer(Customer cust)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_UpdateCustomer", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Id", cust.Id);
            cmd.Parameters.AddWithValue("@Name", cust.Name);
            cmd.Parameters.AddWithValue("@Age", cust.Age);
            cmd.Parameters.AddWithValue("@Address", cust.Address);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //To Delete Customer
        public void DeleteCustomer(int? Id)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_DeleteCustomer", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //Get Customer By ID
        public Customer GetCustomerByID(int? Id)
        {
            Customer cust = new Customer();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetCustByID", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                                   cust.Id = Convert.ToInt32(dr["Id"].ToString());
                    cust.Name = dr["Name"].ToString();
                    cust.Age = Convert.ToInt32(dr["Age"].ToString());
                    cust.Address = dr["Address"].ToString();
                   
                }
                con.Close();
            }

           return cust;
        }

    }


}
