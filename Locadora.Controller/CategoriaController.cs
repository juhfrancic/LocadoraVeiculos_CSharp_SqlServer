using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;


namespace Locadora.Controller
{
    public class CategoriaController
    {
        public void AdicionarCategoria(Categoria categoria)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            try
            {

                SqlCommand command = new SqlCommand(Categoria.INSERTCATEGORIA, connection);

                command.Parameters.AddWithValue("@Nome", categoria.Nome);
                command.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Diaria", categoria.Diaria);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao adicionar categoria: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao adicionar categoria: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public string BuscarNomeCategoriaPorId(int id)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand(Categoria.SELECTNOMECATEGORIAPORID, connection);
                command.Parameters.AddWithValue("@Id", id);

                string nomecategoria = String.Empty;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    nomecategoria = reader["Nome"].ToString() ?? string.Empty;
                }
                return nomecategoria;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar categoria." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao buscar categoria." + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
