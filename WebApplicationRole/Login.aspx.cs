using System;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplicationRole
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Any initialization code can go here
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Get the username and password from the input fields
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Get the connection string from the web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // SQL query to check if the username and password exist and to retrieve the role
                string query = "SELECT Roles.RoleName FROM Users INNER JOIN Roles ON Users.RoleID = Roles.RoleID WHERE Username=@Username AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                string role = (string)cmd.ExecuteScalar();
                con.Close();

                if (role != null)
                {
                    // Store the username and role in session variables
                    Session["Username"] = username;
                    Session["Role"] = role;

                    // Redirect the user based on their role
                    if (role == "Admin")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                    else if (role == "Accountant")
                    {
                        Response.Redirect("AccountantDashboard.aspx");
                    }
                    else if (role == "Security")
                    {
                        Response.Redirect("SecurityDashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("UserDashboard.aspx");
                    }
                }
                else
                {
                    // Show an alert if the username or password is incorrect
                    Response.Write("<script>alert('Invalid username or password');</script>");
                }
            }
        }
    }
}
