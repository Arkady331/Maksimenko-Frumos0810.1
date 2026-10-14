using prak0810.Forms;
using prak0810.ModelEF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prak0810
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private List<int> pictures = new List<int>() { 3, 4, 1, 2 };

        private ModelEF.ModelEF model = new ModelEF.ModelEF();

        private Accounts accounts;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            accounts = model.Accounts.FirstOrDefault(
                x => x.Login == textBox1.Text
                && x.Password == textBox2.Text);

            if (accounts != null)
            {
                if (accounts.StatusID == 1)
                {
                    panelCaptch.Visible = true;
                }
                else
                {
                    MessageBox.Show("Вы заблокированы. Обратитесь к администратору");
                }
                return;
            }
            else
            {
                accounts = model.Accounts.FirstOrDefault(
                    x => x.Login == textBox1.Text);

                if (accounts != null)
                {
                    if (accounts.StatusID == 2)
                    {
                        MessageBox.Show("Вы заблокированы. Обратитесь к администратору");
                        return;
                    }

                    accounts.BadLoginTry += 1;
                    model.SaveChanges();
                    MessageBox.Show($"Вы не правильно ввели пароль. У вас осталось попыток {3 - accounts.BadLoginTry}");

                    if (accounts.BadLoginTry == 3)
                    {
                        accounts.StatusID = 2;
                        model.SaveChanges();
                        MessageBox.Show("Вы заблокированы. Обратитесь к администратору");
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
                }
            }
        }

        private void LoadPictures()
        {
            pictureBox1.Image = Image.FromFile($@"Pictures\{pictures[0]}.png");
            pictureBox2.Image = Image.FromFile($@"Pictures\{pictures[1]}.png");
            pictureBox3.Image = Image.FromFile($@"Pictures\{pictures[2]}.png");
            pictureBox4.Image = Image.FromFile($@"Pictures\{pictures[3]}.png");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadPictures();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelCaptch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReadyButton_Click(object sender, EventArgs e)
        {
            if (pictures[0] == 1)
            {
                panelCaptch.Visible = false;

                if (accounts.RoleID == 1)
                {
                    AdminForm form = new AdminForm();
                    form.Show();
                    Hide();
                }
                else if (accounts.RoleID == 2)
                {
                    UserForm form = new UserForm();
                    form.Show();
                    Hide();
                }

                accounts.BadLoginTry = 0;
                model.SaveChanges();
                MessageBox.Show("Вы успешно авторизовались");
            }
            else
            {
                accounts.BadLoginTry += 1;
                model.SaveChanges();
                MessageBox.Show($"Капча не правильная осталось попыток {3 - accounts.BadLoginTry}");

                if (accounts.BadLoginTry >= 3)
                {
                    accounts.StatusID = 2;
                    model.SaveChanges();
                    MessageBox.Show("Вы заблокированы. Обратитесь к администратору");
                    panelCaptch.Visible = false;
                }
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            int first = pictures[0];
            pictures[0] = pictures[1];
            pictures[1] = pictures[2];
            pictures[2] = pictures[3];
            pictures[3] = first;
            LoadPictures();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
