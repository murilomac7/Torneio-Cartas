using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebFighter.Models;

namespace WebFighter.Repositories.LutadorRepository
{
    public class LutadorRepository : ConexaoDB, ILutadorRepository
    {
        public bool AtualizarLutador(LutadorViewModel lutador)
        {
            try
            {
                AbrirConexao();
                command = new SqlCommand("AtualizarLutador", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", lutador.Id);
                command.Parameters.AddWithValue("@TotalLutas", lutador.Lutas);
                command.Parameters.AddWithValue("@Vitorias", lutador.Vitorias);
                command.Parameters.AddWithValue("@Derrotas", lutador.Derrotas);

                int i = command.ExecuteNonQuery();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar lutador: " + e.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        public LutadorViewModel BuscarLutadorPorID(int id)
        {
            try
            {
                AbrirConexao();
                command = new SqlCommand("BuscarLutadorPorID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                dataReader = command.ExecuteReader();

                LutadorViewModel lutadorEncontrado = new LutadorViewModel();

                while (dataReader.Read())
                {
                    lutadorEncontrado = new LutadorViewModel()
                    {
                        Id = Convert.ToInt32(dataReader["id"]),
                        Nome = Convert.ToString(dataReader["nome"]),
                        Idade = Convert.ToInt32(dataReader["idade"]),
                        ArtesMarciais = Convert.ToInt32(dataReader["artes_marciais"]),
                        Lutas = Convert.ToInt32(dataReader["total_lutas"]),
                        Vitorias = Convert.ToInt32(dataReader["vitorias"]),
                        Derrotas = Convert.ToInt32(dataReader["derrotas"])
                    };                    
                }

                return lutadorEncontrado;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar lutador: " + e.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<LutadorViewModel> ListarLutadores()
        {
            try
            {
                AbrirConexao();

                string sql = "SELECT * FROM Lutadores";

                command = new SqlCommand(sql, connection);

                dataReader = command.ExecuteReader();

                var listaLutadores = new List<LutadorViewModel>();

                while (dataReader.Read())
                {
                    LutadorViewModel lutador = new LutadorViewModel
                    {
                        Id = Convert.ToInt32(dataReader["id"]),
                        Nome = Convert.ToString(dataReader["nome"]),
                        Idade = Convert.ToInt32(dataReader["idade"]),
                        ArtesMarciais = Convert.ToInt32(dataReader["artes_marciais"]),
                        Lutas = Convert.ToInt32(dataReader["total_lutas"]),
                        Vitorias = Convert.ToInt32(dataReader["vitorias"]),
                        Derrotas = Convert.ToInt32(dataReader["derrotas"])
                    };

                    listaLutadores.Add(lutador);
                }

                return listaLutadores;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar lutadores: " + e.Message);
            }
            finally
            {
                FecharConexao();
            }

        }
    }
}
