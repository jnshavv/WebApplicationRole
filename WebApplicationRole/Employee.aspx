<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="WebApplicationRole.Employee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            padding: 20px;
            position: relative; /* Make the body position relative for the logout link positioning */
        }

        .form-container {
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            max-width: 500px;
            margin-bottom: 20px;
        }

        .form-container label {
            font-weight: bold;
            margin-bottom: 5px;
            display: block;
        }

        .form-container input, .form-container select {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .employee-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .employee-table th, .employee-table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        .employee-table th {
            background-color: #007bff;
            color: white;
        }

        .employee-table tr:hover {
            background-color: #f1f1f1;
        }

        .btn {
            padding: 10px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
        }

        .btn-danger {
            background-color: #dc3545;
            color: white;
        }

        .btn-secondary {
            background-color: #6c757d;
            color: white;
        }

        .logout-link {
            background-color: #dc3545;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            text-decoration: none;
            position: absolute;
            top: 20px;
            right: 20px;
        }

        .logout-link:hover {
            background-color: #c82333;
        }
    </style>
</head>
<body>
    <a href="#" class="logout-link" onclick="logout()">Logout</a>

    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Employee Management</h2>

            <label for="txtName">Name:</label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

            <label for="txtDepartment">Department:</label>
            <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>

            <label for="txtPosition">Position:</label>
            <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>

            <label for="txtSalary">Salary:</label>
            <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox>

            <asp:Button ID="btnAdd" runat="server" Text="Add Employee" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back to Admin Dashboard" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>

        <h3>Employee List</h3>
        <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" CssClass="employee-table"
            OnRowEditing="gvEmployees_RowEditing"
            OnRowUpdating="gvEmployees_RowUpdating"
            OnRowCancelingEdit="gvEmployees_RowCancelingEdit"
            OnRowDeleting="gvEmployees_RowDeleting"
            DataKeyNames="EmployeeID">

            <Columns>
                <asp:BoundField DataField="EmployeeID" HeaderText="ID" ReadOnly="True" />
                <asp:TemplateField HeaderText="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDepartment" runat="server" Text='<%# Bind("Department") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("Department") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Position">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPosition" runat="server" Text='<%# Bind("Position") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPosition" runat="server" Text='<%# Bind("Position") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salary">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSalary" runat="server" Text='<%# Bind("Salary") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSalary" runat="server" Text='<%# Bind("Salary") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </form>

    <script type="text/javascript">
        function logout() {
            if (confirm('Are you sure you want to logout?')) {
                window.location.href = 'Logout.aspx'; // Replace with the actual path to your logout page
            }
        }
    </script>
</body>
</html>
