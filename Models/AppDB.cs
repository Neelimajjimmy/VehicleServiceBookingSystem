using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace project3VehicleServiceBookingApp.Models
{
    public class AppDB
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-6BVU8J4G\SQLEXPRESS;database=VServiceBookingDb;Integrated security=true");

        public string addAdmin(AdminRegister ar)
        {
            try
            { string maxid = "";
                int regid = 1;
                SqlCommand cmd = new SqlCommand("sp_GetMaxRegId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();


                 maxid = cmd.ExecuteScalar().ToString();
                con.Close();

                if (maxid!="")
                {
                    int maxregid = Convert.ToInt32(maxid);
                    regid = maxregid + 1;
                }

                SqlCommand cmd1 = new SqlCommand("sp_adminReg", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id", regid);
                cmd1.Parameters.AddWithValue("@na", ar.name);
                cmd1.Parameters.AddWithValue("@em", ar.email);
                cmd1.Parameters.AddWithValue("@ph", ar.phone);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_InsertLogin", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@regid", regid);
                cmd2.Parameters.AddWithValue("@una", ar.username);
                cmd2.Parameters.AddWithValue("@pw", ar.password);
                cmd2.Parameters.AddWithValue("@logtype", "admin");
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();

                return "Admin registered!";
            }catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message;
            }
        }


        public string addCustomer(Customer cus)
        {
            try
            {
                string maxid = "";
                int regid = 1;
                SqlCommand cmd = new SqlCommand("sp_GetMaxRegId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                maxid = cmd.ExecuteScalar().ToString();
                con.Close();
                if (maxid != "")
                {
                    int maxregid = Convert.ToInt32(maxid);
                    regid = maxregid + 1;
                }

                SqlCommand cmd1 = new SqlCommand("sp_InsertCustomer", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id", regid);
                cmd1.Parameters.AddWithValue("@na", cus.name);
                cmd1.Parameters.AddWithValue("@em", cus.email);
                cmd1.Parameters.AddWithValue("@ph", cus.phone);
                cmd1.Parameters.AddWithValue("@addr", cus.addr);
                cmd1.Parameters.AddWithValue("@st", "active");
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_InsertLogin", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@regid", regid);
                cmd2.Parameters.AddWithValue("@una", cus.username);
                cmd2.Parameters.AddWithValue("@pw", cus.password);
                cmd2.Parameters.AddWithValue("@logtype", "user");
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                return "Customer registered!";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message;
            }
        }

        public string Login( Login user)
        {
            SqlCommand cmd = new SqlCommand("sp_Login", con);
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@una", user.username);
            cmd.Parameters.AddWithValue("@pw", user.password);
            con.Open();
            string c = cmd.ExecuteScalar().ToString();
            con.Close();

            return c;
            
        }

        public string getRegId(Login user)
        {
            string regid = "";
            SqlCommand cmd1 = new SqlCommand("sp_GetRegId", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@una", user.username);
            cmd1.Parameters.AddWithValue("@pw", user.password);
            con.Open();
             regid = cmd1.ExecuteScalar().ToString();

            con.Close();
            return regid;
        }
        public string getLogtype(Login user)
        {
            string logtype = "";
            SqlCommand cmd2 = new SqlCommand("sp_Getlogtype", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@una", user.username);
            cmd2.Parameters.AddWithValue("@pw", user.password);
            con.Open();
             logtype = cmd2.ExecuteScalar().ToString();

            con.Close();

            return logtype;
        }
        public string insertService( AddServiceTypeDto st) {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertServiceType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@na", st.name);
                cmd.Parameters.AddWithValue("@desc", st.description);
                cmd.Parameters.AddWithValue("@pr", st.price);
                cmd.Parameters.AddWithValue("@st", "active");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Service added ";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message;
            }
        }

        public List<ServiceType> getAllServices()
        {
            List<ServiceType> values = new List<ServiceType>();
            SqlCommand cmd = new SqlCommand("sp_GetAllServices", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                ServiceType ob = new ServiceType{
                    id = Convert.ToInt32(dr["typeid"]),
                    name = dr["name"].ToString(),
                    description = dr["description"].ToString(),
                    price = Convert.ToDecimal(dr["price"]),
                    status = dr["status"].ToString(),
                };
                values.Add(ob);
            }
            con.Close();
            return values;
        }
        public string editService(string id,EditServiceTypeDto ob)
        {

        }

        }
}

