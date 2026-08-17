<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Employee_Management_Assignment.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Employee & Department Details</h2>

            <asp:Label ID="lblId" runat="server" Text="ID (For Update/Delete):"></asp:Label>&nbsp;
            <asp:TextBox ID="txtId" runat="server"></asp:TextBox>
            <br /><br />

            <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>&nbsp;
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Enter Your Name" ForeColor="Red" ValidationGroup="CRUD">
            </asp:RequiredFieldValidator>
            <br /><br />

            <asp:Label ID="lblAge" runat="server" Text="Age:"></asp:Label>&nbsp;
            <asp:TextBox ID="txtAge" runat="server" TextMode="Number"></asp:TextBox>&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtAge" Display="Dynamic" ErrorMessage="Enter Age" ForeColor="Red" ValidationGroup="CRUD">
            </asp:RequiredFieldValidator>&nbsp;
            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                ControlToValidate="txtAge" Display="Dynamic" ForeColor="Red" 
                MinimumValue="18" MaximumValue="60" Type="Integer" ErrorMessage="Age must be 18 to 60" ValidationGroup="CRUD">
            </asp:RangeValidator>
            <br /><br />

            <asp:Label ID="lblSalary" runat="server" Text="Salary:"></asp:Label>&nbsp;
            <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox>&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txtSalary" Display="Dynamic" ErrorMessage="Enter Salary" ForeColor="Red" ValidationGroup="CRUD">
            </asp:RequiredFieldValidator>
            <br /><br />

            <asp:Label ID="lblDeptName" runat="server" Text="Department Name:"></asp:Label>&nbsp;
            <asp:TextBox ID="txtDept" runat="server"></asp:TextBox>&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txtDept" Display="Dynamic" ErrorMessage="Enter Department" ForeColor="Red" ValidationGroup="CRUD">
            </asp:RequiredFieldValidator>
            <br /><br />

            <asp:Button ID="btnAdd" runat="server" Text="ADD" OnClick="btnAdd_Click" ValidationGroup="CRUD" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="CRUD" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="false" />
            
            <br /><br />
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True"></asp:Label>
            <br /><br />

            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="5">
                <Columns>
                    <asp:BoundField DataField="Emp_Id" HeaderText="Employee ID" />
                    <asp:BoundField DataField="Emp_Name" HeaderText="Name" />
                    <asp:BoundField DataField="Emp_Age" HeaderText="Age" />
                    <asp:BoundField DataField="Emp_Salary" HeaderText="Salary" />
                    <asp:BoundField DataField="Dpt_Name" HeaderText="Department" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>