using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Первое_приложение_2._0_WinFormsApp1
{
    public partial class RegisterForm1 : Form
    {
        public RegisterForm1()
        {
            InitializeComponent();
            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Width, this.loginField.Height);
            UserNameField.Text = "Введите имя";
            UserNameField.ForeColor = Color.Gray;
            SoreNemeField.Text = "Введите фамилию";
            SoreNemeField.ForeColor= Color.Gray;
            loginField.Text = "Login";
            loginField.ForeColor= Color.Gray;
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
            if (e.Button == MouseButtons.Left)
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

        private void UserNameField_Enter(object sender, EventArgs e)
        {
            if (UserNameField.Text == "Введите имя") 
            UserNameField.Text = "";
            UserNameField.ForeColor = Color.Black;
        }
        private void UserNameField_Leave(object sender, EventArgs e)
        {
            if (UserNameField.Text == "")
            {
                UserNameField.Text = "Введите имя";
                UserNameField.ForeColor = Color.Gray;
            }
        }
        private void SoreNemeField_Enter(object sender, EventArgs e)
        {
            if (SoreNemeField.Text == "Введите фамилию")
                SoreNemeField.Text = "";
            SoreNemeField.ForeColor = Color.Black;
        }
        private void SoreNemeField_Leave(object sender, EventArgs e)
        {
            if (SoreNemeField.Text == "")
            {
                SoreNemeField.Text = "Введите фамилию";
                SoreNemeField.ForeColor = Color.Gray;
            }

        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if(UserNameField.Text == "Введите имя")
            {
                MessageBox.Show("Дебил! Напиши имя!");
                return;
            }
            if (SoreNemeField.Text == "Введите фамилию")
            {
                MessageBox.Show("Дебил! Напиши фамилию!");
                return;
            }
            if (loginField.Text == "Login")
            {
                MessageBox.Show("Дебил! Напиши Логин!");
                return;
            }
            if (passField.Text == "")
            {
                MessageBox.Show("Дебил! Где пароль?!");
                return;
            }
            if (isUserExists())
                return;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) VALUES (@login, @pass, @name, @surname)",db.GetConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = UserNameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = SoreNemeField.Text;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {     
                MessageBox.Show("Заебись! Все получилось!");
            
                this.Hide();
                MainForm1 mainForm1 = new MainForm1();
                mainForm1.Show();
            }

            else
                MessageBox.Show("Ваш аккаунт не был создан!");
            db.closeConnection();
        }
        public Boolean isUserExists()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть!");
                return true;
            }
            else
                return false;   

        }

        private void registerlabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm1 loginForm1 = new LoginForm1();
            loginForm1.Show();
        }

        private void registerlabel_MouseEnter(object sender, EventArgs e)
        {
            registerlabel.ForeColor = Color.Red;
        }

        private void registerlabel_MouseLeave(object sender, EventArgs e)
        {
            registerlabel.ForeColor = Color.Orange;
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Login")
                loginField.Text = "";
            loginField.ForeColor = Color.Black;
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "Login";
                loginField.ForeColor = Color.Gray;
            }
        }
    }
    
}
