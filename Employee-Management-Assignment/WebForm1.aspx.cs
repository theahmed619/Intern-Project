using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Employee_Management_Assignment
{
    public partial class WebForm1 : System.Web.UI.Page
    {
         string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"SELECT e.Emp_Id, e.Emp_Name, e.Emp_Age, e.Emp_Salary, d.Dpt_Name 
                                 FROM Employee e 
                                 LEFT JOIN Department d ON e.Emp_Id = d.Emp_Id 
                                 ORDER BY e.Emp_Id DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        // Insert into Employee table
                        string empQuery = @"INSERT INTO Employee (Emp_Name, Emp_Age, Emp_Salary) 
                                            VALUES (@Name, @Age, @Salary); 
                                            SELECT SCOPE_IDENTITY();";

                        SqlCommand cmdEmp = new SqlCommand(empQuery, con, trans);
                        cmdEmp.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        cmdEmp.Parameters.AddWithValue("@Age", Convert.ToInt32(txtAge.Text.Trim()));
                        cmdEmp.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSalary.Text.Trim()));

                        int empId = Convert.ToInt32(cmdEmp.ExecuteScalar());

                        // Insert into Department table with foreign key Emp_Id
                        string deptQuery = "INSERT INTO Department (Emp_Id, Dpt_Name) VALUES (@EmpId, @DeptName)";
                        SqlCommand cmdDept = new SqlCommand(deptQuery, con, trans);
                        cmdDept.Parameters.AddWithValue("@EmpId", empId);
                        cmdDept.Parameters.AddWithValue("@DeptName", txtDept.Text.Trim());
                        cmdDept.ExecuteNonQuery();

                        trans.Commit();
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Record added successfully!";
                        ClearFields();
                        BindGridView();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Error: " + ex.Message;
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter Employee ID to update!";
                return;
            }

            int empId = Convert.ToInt32(txtId.Text.Trim());

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        string empQuery = "UPDATE Employee SET Emp_Name = @Name, Emp_Age = @Age, Emp_Salary = @Salary WHERE Emp_Id = @EmpId";
                        SqlCommand cmdEmp = new SqlCommand(empQuery, con, trans);
                        cmdEmp.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        cmdEmp.Parameters.AddWithValue("@Age", Convert.ToInt32(txtAge.Text.Trim()));
                        cmdEmp.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSalary.Text.Trim()));
                        cmdEmp.Parameters.AddWithValue("@EmpId", empId);
                        cmdEmp.ExecuteNonQuery();

                        string deptQuery = "UPDATE Department SET Dpt_Name = @DeptName WHERE Emp_Id = @EmpId";
                        SqlCommand cmdDept = new SqlCommand(deptQuery, con, trans);
                        cmdDept.Parameters.AddWithValue("@DeptName", txtDept.Text.Trim());
                        cmdDept.Parameters.AddWithValue("@EmpId", empId);
                        cmdDept.ExecuteNonQuery();

                        trans.Commit();
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Record updated successfully!";
                        ClearFields();
                        BindGridView();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Error: " + ex.Message;
                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter Employee ID to delete!";
                return;
            }

            int empId = Convert.ToInt32(txtId.Text.Trim());

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Employee WHERE Emp_Id = @EmpId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Record deleted successfully!";
                        ClearFields();
                        BindGridView();
                    }
                    else
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Record not found!";
                    }
                }
            }
        }

        private void ClearFields()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtDept.Text = string.Empty;
        }
    }
}