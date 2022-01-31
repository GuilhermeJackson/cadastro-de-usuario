using Cadastro_de_usuario.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Cadastro_de_usuario.Repository
{
    class UserRepository
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Guilherme.Lamim\Documents\users.mdf;Integrated Security=True;Connect Timeout=30";
        private SqlConnection connection = null;
        public UserRepository()
        {
            connection = new SqlConnection(connectionString);
        }

        public int Insert(User user)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"INSERT INTO users (nome, email, age) OUTPUT INSERTED.uuid VALUES (@NAME, @EMAIL, @AGE)";
            command.Parameters.AddWithValue("@NAME", user.Name);
            command.Parameters.AddWithValue("@EMAIL", user.Email);
            command.Parameters.AddWithValue("@AGE", user.Age);
            int uuid = Convert.ToInt32(command.ExecuteScalar().ToString());
            connection.Close();
            return uuid;
        }

        public List<User> getAll()
        {
            List<User> users = new List<User>();
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT uuid, nome, age, email FROM users";
            DataTable memoryTable = new DataTable();
            memoryTable.Load(command.ExecuteReader());

            if (memoryTable.Rows.Count == 0)
            {
                return null;
            }
            for (int i = 0; i < memoryTable.Rows.Count; i++)
            {
                User user = new User();
                user.Uuid = Convert.ToInt32(memoryTable.Rows[i][0].ToString());
                user.Name = memoryTable.Rows[i][1].ToString();
                user.Email = memoryTable.Rows[i][1].ToString();
                user.Age= Convert.ToInt32(memoryTable.Rows[i][2].ToString());
                users.Add(user);
            }
            connection.Close();
            return users;
        }

        public User GetUuid(int uuid)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT uuid, nome, email, age FROM users WHERE uuid = @ID";
            command.Parameters.AddWithValue("@ID", uuid);
            DataTable memoryTable = new DataTable();
            memoryTable.Load(command.ExecuteReader());
            if (memoryTable.Rows.Count == 0) 
            {
                connection.Close();
                return null;
            }
            User user = new User
            {
                Uuid = int.Parse(memoryTable.Rows[0][0]?.ToString()),
                Name = memoryTable.Rows[0][1].ToString(),
                Email = memoryTable.Rows[0][1].ToString(),
                Age = int.Parse(memoryTable.Rows[0][3].ToString())
            };
            connection.Close();
            return user;
        }

        public bool Delete(int uuid)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM users WHERE uuid = @ID";
            command.Parameters.AddWithValue("@ID", uuid);
            int count = command.ExecuteNonQuery();
            connection.Close();
            return count == 1;
        }

        public bool Update(User user)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"UPDATE users SET nome = @NOME,
age = @AGE,
email = @EMAIL
WHERE uuid = @ID";
            command.Parameters.AddWithValue("@NOME", user.Name);
            command.Parameters.AddWithValue("@AGE", user.Age);
            command.Parameters.AddWithValue("@EMAIL", user.Email);
            command.Parameters.AddWithValue("@ID", user.Uuid);
            int changeCount = command.ExecuteNonQuery();
            connection.Close();
            return changeCount == 1;
        }
    }
}
