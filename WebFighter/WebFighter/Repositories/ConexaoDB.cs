using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebFighter.Repositories
{
    public class ConexaoDB
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=db_TorneioLuta;Integrated Security=True";
        protected SqlConnection connection;

        protected SqlCommand command;

        protected SqlDataReader dataReader;

        //Método para abrir a conexão com o SqlServer
        protected void AbrirConexao()
        {
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao abrir a conexão: " + e.Message);
            }
        }

        //Método para fechar a conexão:
        protected void FecharConexao()
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao fechar a conexão: " + e.Message);
            }
        }
    }
}
