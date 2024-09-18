<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountantDashboard.aspx.cs" Inherits="WebApplicationRole.AccountantDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Accountant Dashboard</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            padding: 20px;
            margin: 0;
        }

        .dashboard-container {
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            max-width: 600px;
            margin: 0 auto;
            position: relative; /* Required for positioning the logout button */
        }

        .dashboard-container h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #333;
        }

        .btn {
            display: block;
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            text-align: center;
            text-decoration: none;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        /* Styling for the logout button */
        .logout-btn {
            position: absolute;
            top: 10px;
            right: 10px;
            padding: 10px 15px;
            background-color: #dc3545;
            color: white;
            border: none;
            border-radius: 5px;
            text-decoration: none;
            font-size: 14px;
            cursor: pointer;
        }

        .logout-btn:hover {
            background-color: #c82333;
        }
    </style>
</head>
<body>
    <div class="dashboard-container">
        <!-- Logout Button -->
        <a href="Logout.aspx" class="logout-btn">Logout</a>

        <h2>Accountant Dashboard</h2>
        
        <!-- Link to Employee Management Page -->
        <a href="Employee.aspx" class="btn">Manage Employees</a>
        
        <!-- Other dashboard options can go here -->
    </div>
</body>
</html>
