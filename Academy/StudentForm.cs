using Academy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Academy
{
    public partial class StudentForm : HumanForm
    {
        public StudentForm()
        {
            InitializeComponent();
            DataTable groups = DataBase.Connector.Select("SELECT * FROM Groups");
            cbGroup.DataSource = groups;
            cbGroup.DisplayMember = "group_name";
            cbGroup.ValueMember = "group_id";
        }
        public StudentForm(int id)
        {
            DataTable data = DataBase.Connector.Select("*", "Students", $"stud_id={id}");
            //object[] arr = data.Rows[0].ItemArray;
            student = new Models.Student(data.Rows[0].ItemArray);
            human = student;
            Extract();
            cbGroup.SelectedValue = student.group;
            //pbPhoto.Image = DataBase.Connector.DownloadPhoto("Students", "photo", student.id);
        }
        protected override void buttonOK_Click(object sender, EventArgs e)
        {
            base.buttonOK_Click(sender, e);
            student = new Models.Student(human, Convert.ToInt32(cbGroup.SelectedValue));
            DataBase.Connector.Insert("Students", $"{student.GetNames()}", $"{student.GetValues()}");
            //DataBase.Connector.Insert
            //("Students","last_name,first_name,middle_name,birth_date,email,phone,[group]",
            //$"{tbLastName.Text},{tbFirstName.Text},{tbMiddleName.Text},{dtpBirthDate.Value.ToString("yyyy-MM-dd")},{tbEmail.Text},{tbPhone.Text},{cbGroup.SelectedValue}");
        }
    }
}
