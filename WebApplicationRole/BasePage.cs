using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Diagnostics;
using System.Web;

namespace WebApplicationRole
{
    public class BasePage : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Debug.WriteLine("BasePage OnLoad called.");

            if (Session["Username"] == null)
            {
                Debug.WriteLine("User not logged in. Redirecting to Login.aspx.");
                HandleRedirection("~/Login.aspx");
                return;
            }

            string role = Session["Role"] as string;
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            Debug.WriteLine($"Page Name: {pageName}");
            Debug.WriteLine($"User Role: {role}");

            if (!HasAccess(role, pageName))
            {
                Debug.WriteLine("Access denied. Redirecting to AccessDenied.aspx.");
                HandleRedirection("~/AccessDenied.aspx");
                return;
            }
        }

        private bool HasAccess(string role, string pageName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT COUNT(*)
                    FROM PageRoles pr
                    INNER JOIN Pages p ON pr.PageID = p.PageID
                    INNER JOIN Roles r ON pr.RoleID = r.RoleID
                    WHERE p.PageName = @PageName AND r.RoleName = @RoleName";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PageName", pageName);
                    cmd.Parameters.AddWithValue("@RoleName", role);

                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    con.Close();

                    Debug.WriteLine($"Access Count: {count}");

                    return count > 0;
                }
            }
        }

        private void HandleRedirection(string url)
        {
            // This method avoids the ThreadAbortException by not calling Response.Redirect directly
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write($"<script>window.location='{ResolveUrl(url)}';</script>");
            HttpContext.Current.Response.End();
        }
    }
}
