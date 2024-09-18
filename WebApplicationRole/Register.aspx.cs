using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationRole
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateRoles();
            }
        }
        private void PopulateRoles()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT RoleID, RoleName FROM Roles";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlRole.DataSource = dt;
                ddlRole.DataBind();
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            int roleId = int.Parse(ddlRole.SelectedValue);

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, Password, RoleID) VALUES (@Username, @Password, @RoleID)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@RoleID", roleId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Redirect("Login.aspx");
        }
    }
}