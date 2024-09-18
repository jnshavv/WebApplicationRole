using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationRole
{
    public partial class Employee : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                BindEmployeeData();
            }
        }

        private void BindEmployeeData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT EmployeeID, Name, Department, Position, Salary FROM Employees";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvEmployees.DataSource = dt;
                gvEmployees.DataBind();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountantDashboard.aspx"); // Replace with the actual path to your admin dashboard page
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string department = txtDepartment.Text.Trim();
            string position = txtPosition.Text.Trim();
            decimal salary;

            if (!decimal.TryParse(txtSalary.Text.Trim(), out salary))
            {
                // Handle invalid salary input
                // lblError.Text = "Invalid salary value.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employees (Name, Department, Position, Salary, CreatedDate) VALUES (@Name, @Department, @Position, @Salary, GETDATE())";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Salary", salary);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            // Clear form fields
            txtName.Text = "";
            txtDepartment.Text = "";
            txtPosition.Text = "";
            txtSalary.Text = "";

            // Rebind the data
            BindEmployeeData();
        }

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            BindEmployeeData();
        }

        protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvEmployees.Rows[e.RowIndex];
            string name = ((TextBox)row.FindControl("txtName"))?.Text.Trim() ?? string.Empty;
            string department = ((TextBox)row.FindControl("txtDepartment"))?.Text.Trim() ?? string.Empty;
            string position = ((TextBox)row.FindControl("txtPosition"))?.Text.Trim() ?? string.Empty;
            decimal salary;

            if (!decimal.TryParse(((TextBox)row.FindControl("txtSalary"))?.Text.Trim(), out salary))
            {
                // Handle invalid salary input
                // lblError.Text = "Invalid salary value.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employees SET Name=@Name, Department=@Department, Position=@Position, Salary=@Salary WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Salary", salary);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            gvEmployees.EditIndex = -1;
            BindEmployeeData();
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            BindEmployeeData();
        }

        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);

                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Employees WHERE EmployeeID=@EmployeeID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                BindEmployeeData();
            }
            catch (Exception ex)
            {
                // Display or log error
                // lblError.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
