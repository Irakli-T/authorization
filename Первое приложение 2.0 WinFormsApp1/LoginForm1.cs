using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Первое_приложение_2._0_WinFormsApp1
{
    public partial class LoginForm1 : Form
    {
        public LoginForm1()
        {
            InitializeComponent();
            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Width, this.loginField.Height);
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }
        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.White;
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) 
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y; 
            }
        }
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            String loginUser = loginField.Text;
            String passUser = passField.Text;
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                this.Hide();
                MainForm1 mainForm1 = new MainForm1();
                mainForm1.Show();
            }
            else
                MessageBox.Show("Пошёл на хуй! Сначала создай аккаунт!");
        }
        private void registerlabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm1 registerForm1 = new RegisterForm1();
            registerForm1.Show();
        }
        private void registerlabel_MouseEnter(object sender, EventArgs e)
        {
            registerlabel.ForeColor = Color.Red;
        }
        private void registerlabel_MouseLeave(object sender, EventArgs e)
        {
            registerlabel.ForeColor = Color.Orange;
        }
    }
}
