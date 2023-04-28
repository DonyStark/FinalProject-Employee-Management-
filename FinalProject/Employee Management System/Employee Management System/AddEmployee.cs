using System;
using System.Drawing;
using System.Windows.Forms;
using EmployeeManagementSystem.Data;
namespace EmployeeManagementSystem
{
    public partial class AddEmployee : Form
    {
        private bool _dragging;
        private Point _startPoint = new(0, 0);
        public delegate void IdentityHandler(object sender, IdentityEventArgs e);
        public event IdentityHandler IdentityUpdated;
        public AddEmployee()
        {
            InitializeComponent();
            comboBoxDepartment.Items.Add("Administrative");
            comboBoxDepartment.Items.Add("Finance");
            comboBoxDepartment.Items.Add("Customer service");
            comboBoxDepartment.Items.Add("Marketing");
            comboBoxDepartment.Items.Add("IT");
            comboBoxDepartment.SelectedIndex = 0;
        }
        public void LoadData(string id, string name, string address, string contact, string email, string desigination, string department, string dateOfJoin, string wageRate, string workedHour)
        {
            txtIdNo.Text = id;
            txtFullName.Text = name;
            txtAddress.Text = address;
            txtContact.Text = contact;
            txtEmail.Text = email;
            txtDesignation.Text = desigination;
            comboBoxDepartment.Text = department;
            dateTimePicker.Text = dateOfJoin;
            txtWage.Text = wageRate;
            txtWorkedHour.Text = workedHour;
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            var p = PointToScreen(e.Location);
            Location = new Point(p.X - this._startPoint.X, p.Y - this._startPoint.Y);
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            var id = txtIdNo.Text;
            var name = txtFullName.Text;
            var address = txtAddress.Text;
            var contactNo = txtContact.Text;
            var email = txtEmail.Text;
            var desigination = txtDesignation.Text;
            var department = comboBoxDepartment.Text;
            var dateOfJoin = dateTimePicker.Text;
            var wageRate = txtWage.Text;
            var hourWorked = txtWorkedHour.Text;
            using (var context = new EmployeeManagementContext())
            {
                var emp = new Employee(id, name, address, contactNo, email, desigination, department, dateOfJoin, wageRate, hourWorked);
                context.Employees.Add(emp);
                await context.SaveChangesAsync();
            }
            var args = new IdentityEventArgs(id, name, address, contactNo, email, desigination, department, dateOfJoin, wageRate, hourWorked);
            IdentityUpdated?.Invoke(this, args);
            this.Hide();
        }
        private bool Validation(TextBox t, string name)
        {
            bool error = false;
            if (int.TryParse(t.Text, out int n))
            {
                error = true;
                MessageBox.Show("Invalid character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return error;
        }
        private void TxtFullName_TextChanged(object sender, EventArgs e)
        {
            Validation(txtFullName, "Full name");
        }
    }
}