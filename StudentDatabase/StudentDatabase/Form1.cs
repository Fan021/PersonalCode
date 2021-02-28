using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database;

namespace StudentDatabase
{
    public partial class Form1 : Form
    {
        private StudentSheet _sql;
        private DataSet _ds;
        private StudentClass _stu = new StudentClass();
        public Form1()
        {
            InitializeComponent();
        }

        private void create_button_Click(object sender, EventArgs e)
        {
            try
            {
                _sql.CreateTable();
                create_button.BackColor = Color.YellowGreen;
            }
            catch (Exception ex)
            {
                create_button.BackColor = Color.Red;
                MessageBox.Show("Create table failed:" + ex.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _sql = new StudentSheet();
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = DateTime.Now;
                _stu.Name = Name_textBox.Text;
                _stu.Age = Convert.ToInt32(Age_textBox.Text);
                _stu.Subject = Subject_listBox.SelectedItem.ToString();
                _stu.Score = Convert.ToInt32(Score_textBox.Text);

                _sql.Add(_stu.Name, _stu.Age, _stu.Subject, _stu.Score, dt);
                Add_button.BackColor = Color.YellowGreen;
            }
            catch (Exception ex)
            {
                Add_button.BackColor = Color.Red;
                MessageBox.Show("Fail to add info:" + ex.Message);
            }
        }

        private void Update_button_Click(object sender, EventArgs e)
        {
            try
            {
                Update_button.BackColor = Color.Gainsboro;
                var dt = DateTime.Now;
                _stu.ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                _stu.Name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                _stu.Age = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                _stu.Subject = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                _stu.Score = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);

                _sql.Update(_stu.ID, _stu.Name, _stu.Age, _stu.Subject, _stu.Score, dt);

            }
            catch (Exception ex)
            {
                Update_button.BackColor = Color.Red;
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                //获得当前选取行的某一列的值
                var index = dataGridView1.CurrentRow.Cells[0].Value;
                _sql.Delete(Convert.ToInt32(index));
            }
            catch (Exception)
            {
                Delete_button.BackColor = Color.Red;
                throw;
            }
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            try
            {
                _stu.Name = Name_textBox1.Text;
                //_stu.Age = Convert.ToInt32(Age_textBox1.Text);
                _stu.Subject = Subject_textBox1.Text;
                //_stu.Score = Convert.ToInt32(Score_textBox1.Text);
                _ds = _sql.Search(_stu.Name, Age_textBox1.Text, _stu.Subject,Score_textBox1.Text);
                dataGridView1.DataSource = _ds.Tables["studentinfo1"];//Tables[]为Dictionary类型，key默认为0,1,2,3
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            Add_button.BackColor = Color.Gainsboro;
        }
    }

    class StudentClass
    {
        private int _age;
        private int _score;
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value >= 15 && value <= 50)
                {
                    _age = value;
                }
                else
                {
                    throw new Exception("Wrong age!It should betwwen 15 to 50.");
                }
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _score = value;
                }
                else
                {
                    throw new Exception("Wrong Score!It should betwwen 0 to 100.");
                }
            }
        }

        public string Subject { get; set; }
    }
}
