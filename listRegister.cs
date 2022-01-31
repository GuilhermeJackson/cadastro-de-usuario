using Cadastro_de_usuario.Model;
using Cadastro_de_usuario.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Cadastro_de_usuario
{
    public partial class listRegister : Form
    {
        public listRegister()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            new Register().ShowDialog();
            UpdateList();
        }

        private void UpdateList()
        {
            dataGridView1.Rows.Clear();
            List<User> users = new UserRepository().getAll();
            if (users != null)
            {
                foreach (User user in users)
                {
                    dataGridView1.Rows.Add(new object[]

                    {
                user.Uuid,
                user.Name,
                user.Email,
                    });
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index < dataGridView1.Rows.Count - 1)
            {
                int lineSelected = dataGridView1.CurrentRow.Index;


                int uuid = Convert.ToInt32(dataGridView1.Rows[lineSelected].Cells[0].Value.ToString());
                bool deleted = new UserRepository().Delete(uuid);
                if (deleted)
                {
                    dataGridView1.Rows.RemoveAt(lineSelected);
                    MessageBox.Show("Usuário deletedo com sucesso");
                }
            }
            else
            {
                MessageBox.Show("Não existem registros desse usuário");
            }
            UpdateList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index < dataGridView1.Rows.Count - 1)
            {
                int lineSelected = dataGridView1.CurrentRow.Index;
                int uuid = Convert.ToInt32(dataGridView1.Rows[lineSelected].Cells[0].Value.ToString());
                
                new Register(uuid).ShowDialog();
                UpdateList();
            }
            else
            {
                MessageBox.Show("Não existe registros desse usuário");
            }
        }
    }
}
