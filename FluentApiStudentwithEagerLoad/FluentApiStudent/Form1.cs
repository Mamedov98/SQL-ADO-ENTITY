using FluentApiStudent.Model;
using Microsoft.Data.SqlClient;
using System.Linq; 
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace FluentApiStudent
{
    public partial class Form1 : Form
    {
        private StudentContext _context;

        public Form1()
        {
            InitializeComponent();
            _context = new StudentContext();
            LoadData();

        }

        private void LoadData()
        {
            _context.Students.Load();
            //  dataGridView1.DataSource = _context.Students.Local.ToBindingList();

           dataGridView1.DataSource = _context.Students.Include(x => x.Group  ).ToList(); // eager loading
           

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            var selectedStudent = dataGridView1.SelectedRows[0].DataBoundItem as Student;

            if (selectedStudent != null)
            {
                textBox1.Text = selectedStudent.Name;
                textBox2.Text = selectedStudent.Surname;
                textBox3.Text = selectedStudent.DateOfBirth;
                textBox4.Text = selectedStudent.Group;

                //textBoxName.Text = selectedStudent.Name;
                //textBoxSurname.Text = selectedStudent.Surname;
                //dateTimePickerDateOfBirth.Value = DateTime.Parse(selectedStudent.DateOfBirth);
                //textBoxGroup.Text = selectedStudent.Group;
            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var Student = new Student()
            {
                Name = textBox1.Text
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {


            var student = new Student
            {

                Name = textBox1.Text,
                Surname = textBox2.Text,
                DateOfBirth = textBox3.Text,
                Group = textBox4.Text

            };
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox4.Text))
            {

                _context.Students.Add(student);
                _context.SaveChanges();

                LoadData();
            }


            else
            {
                MessageBox.Show("Заполните данные полностью!");
                _context.SaveChanges();

                LoadData();
            }

        }


        private void button2_Click(object sender, EventArgs e) // УДАЛЕНИЕ
        {
            var selectedStudent = dataGridView1.SelectedRows[0].DataBoundItem as Student;
            if (selectedStudent != null)
            {
                _context.Students.Remove(selectedStudent);
                _context.SaveChanges();

                LoadData();
            }
        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int studentId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

                var student = _context.Students.Find(studentId);

                if (student == null)
                {
                    MessageBox.Show("Студент не найден!");
                    return;
                }




               // student.Id = textBox1.Text;
                student.Name = textBox1.Text;
                student.Surname= textBox2.Text;
                student.DateOfBirth = textBox3.Text;
                student.Group = textBox4.Text;

               // LoadData();
                _context.SaveChanges();

                _context = new StudentContext();
                LoadData();
                MessageBox.Show("Студент успешно отредактирован!");
                
            }
            else
            {
                MessageBox.Show("Выберите студента для редактирования!");
            }

           
           
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _context.Dispose();
        }

        //private void button4_Click(object sender, EventArgs e)  Для обновления
        //{
        //    _context = new StudentContext();
        //    LoadData();
        //}
    }
}