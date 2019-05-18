using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class Form1 : Form
    {
        Employee empModel = new Employee();
       
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            empModel.FirstName = txtFirstName.Text;
            empModel.LastName = txtLastname.Text;
            if (rbtMale.Checked)
            {
                empModel.Sex = rbtMale.Text;
            }
            else if (rbtMale.Checked)
            {
                empModel.Sex = rbtFemale.Text;
            }
            empModel.Age = int.Parse(cmbAge.Text);
            empModel.Salary = decimal.Parse( txtSalary.Text);
            empModel.Address = txtAddress.Text;
            try
            {
                if (btnsave.Text == "Save" )
                {
                    using (EmployeeManagementSystemEntities1 db = new EmployeeManagementSystemEntities1())
                    {
                        db.Employees.Add(empModel);
                        db.SaveChanges();
                        LoadData();
                        MessageBox.Show("Data Saved Successfully");
                      
                        Clear();

                    }
                   
                }
                else if (btnsave.Text == "Update")
                {
                    using (EmployeeManagementSystemEntities1 db = new EmployeeManagementSystemEntities1())
                    {
                        db.Entry(empModel).State = EntityState.Modified;
                        db.SaveChanges();
                        LoadData();
                        MessageBox.Show("Data Updated Successfully");
                        Clear();
                    }
                    
                }

               
            }
            catch (Exception ex)
            {

                MessageBox.Show("Cannot to save info" + ex.Message);
            }

           
         
       
        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvemployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Clear();
            LoadCombobox();
            LoadData();
        }

        public void LoadData()
        {
            using (EmployeeManagementSystemEntities1 db = new EmployeeManagementSystemEntities1())
            {
                dgvemployees.DataSource = db.Employees.ToList();
            }
           
        }
        public void LoadCombobox()
        {
            for (int i = 20; i <= 55; i++)
            {
                cmbAge.Items.Add(i);
            }

        }
        public void Clear()
        {
            txtFirstName.Text = txtLastname.Text = txtSalary.Text = txtAddress.Text = cmbAge.Text = "";
            rbtMale.Checked = true;
            btnsave.Text = "Save";
            btndelete.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dgvemployees_DoubleClick(object sender, EventArgs e)
        {
            using (EmployeeManagementSystemEntities1 db = new EmployeeManagementSystemEntities1())
            {
                var employeeid = dgvemployees.CurrentRow.Cells["Id"].Value;
                var theRestOfTheEmployeeData = db.Employees.Find(employeeid);
                txtFirstName.Text = theRestOfTheEmployeeData.FirstName;
                txtLastname.Text = theRestOfTheEmployeeData.LastName;
                if (theRestOfTheEmployeeData.Sex == "Male")
                {
                    rbtMale.Checked = true;
                }
                else if (theRestOfTheEmployeeData.Sex == "Female")
                {
                    rbtFemale.Checked = true;
                }

                cmbAge.Text = theRestOfTheEmployeeData.Age.ToString();
                txtSalary.Text = theRestOfTheEmployeeData.Salary.ToString();
                txtAddress.Text = theRestOfTheEmployeeData.Address;

                //id to be used to know which record to update
                empModel.id = theRestOfTheEmployeeData.id;
            }
          
            
            btnsave.Text = "Update";
            btndelete.Enabled = true;

        }

        private void btndelete_Click(object sender, EventArgs e)
        { 
            try
            {
                if (MessageBox.Show("Are you sure you want to delete","Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)== DialogResult.Yes)
                {
                    //var employeeid = dgvemployees.CurrentRow.Cells["Id"].Value;
                    using (EmployeeManagementSystemEntities1 db = new EmployeeManagementSystemEntities1())
                    {
                        db.Entry(empModel).State = EntityState.Deleted;
                        db.SaveChanges();
                        LoadData();
                        MessageBox.Show("Data Deleted Successfully");

                        Clear();
                        //second way
                        //var employeetodelete = db.Employees.Find(employeeid);
                        //db.Employees.Remove(employeetodelete);
                        //db.SaveChanges();
                        //LoadData();
                        //MessageBox.Show("Data Deleted Successfully");
                    }

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Unable to delete data" + ex.Message);
            }

        }
    }
}
