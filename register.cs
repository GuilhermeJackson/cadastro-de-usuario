using Cadastro_de_usuario.Model;
using Cadastro_de_usuario.Repository;
using System;
using System.Windows.Forms;

namespace Cadastro_de_usuario
{
    public partial class Register : Form
    {
        private int uuid;
        private User user;
        public Register()
        {
            user = new User();
            InitializeComponent();
        }

        public Register(int uuid)
        {
            InitializeComponent();
            this.uuid = uuid;
            user = new UserRepository().GetUuid(uuid);
            FillField(user);
        }

        private void FillField(User user)
        {
            txtName.Text = user.Name;
            txtEmail.Text = user.Email;
            txtID.Text = Convert.ToString(user.Uuid);
            txtAge.Text = Convert.ToString(user.Age);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            user.Name = txtName.Text;
            user.Email = txtEmail.Text;
            user.Age = Convert.ToInt32(txtAge.Text);
            user.Uuid = Convert.ToInt32(txtID.Text);

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Preencha os campo corretamente");
                return;
            }

            var userExist = new UserRepository().GetUuid(uuid);
            if (userExist == null)
            {
                _ = new UserRepository().Insert(user);

                MessageBox.Show("Cadastrado com sucesso");
            } else
            {
                _= new UserRepository().Update(user);
            }
        }
    }
}
