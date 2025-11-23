using Locadora.Controller.Interfaces;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Databases;

namespace Locadora.Controller
{
    public class LocacaoFuncionarioController : ILocacaoFuncionario
    {
        public void AdicionarLocacaoFuncionario(int locacaoId, int funcionarioId, SqlConnection connection, SqlTransaction transaction)
        {

            try
            {
                SqlCommand command = new SqlCommand(LocacaoFuncionario.INSERTLOCACAOFUNCIONARIO, connection, transaction);
                command.Parameters.AddWithValue("@LocacaoId", locacaoId);
                command.Parameters.AddWithValue("@FuncionarioId", funcionarioId);

                command.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao adicionar locação-funcionário: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao adicionar locação-funcionário: " + ex.Message);
            }
        }

        public void AdicionarLocacaoFuncionario(int locacaoId, int funcionarioId)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(LocacaoFuncionario.INSERTLOCACAOFUNCIONARIO, connection, transaction);
                    command.Parameters.AddWithValue("@LocacaoId", locacaoId);
                    command.Parameters.AddWithValue("@FuncionarioId", funcionarioId);

                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar locação-funcionário: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao adicionar locação-funcionário: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeletarLocacaoFuncionario(int funcionarioId)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(LocacaoFuncionario.DELETELOCACAOFUNCIONARIO, connection, transaction);
                    command.Parameters.AddWithValue("@FuncionarioID", funcionarioId);
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar locação-funcionário: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao deletar locação-funcionário: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Locacao> ListarLocacoesPorFuncionario(string email)
        {
            var funcionarioController = new FuncionarioController();    
            var funcionario = funcionarioController.BuscarFuncionarioPorEmail(email);

            if(funcionario is null)
                throw new Exception("Funcionário não encontrado!");


            using (SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                connection.Open();

                List<Locacao> locacoes = new List<Locacao>();
                List<int> locacoesIDs = new List<int>();
                try
                {
                    SqlCommand command = new SqlCommand(LocacaoFuncionario.SELECTLOCACAOPORFUNCIONARIO, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        locacoesIDs.Add(Convert.ToInt32(reader["LocacaoID"]));
                    }

                    reader.Close();
                    var locacaoController = new LocacaoController();
                    foreach (int locacaoID in locacoesIDs)
                    {
                        Locacao locacao = locacaoController.BuscarLocacaoPorID(locacaoID);
                        if (locacao != null)
                            locacoes.Add(locacao);
                    }

                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar locações por funcionario: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao listar locações por funcionario: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return locacoes;
            }
        }

        public List<Funcionario> ListarFuncionariosPorLocacao(int locacaoId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                connection.Open();
                List<Funcionario> funcionarios = new List<Funcionario>();
                List<string> emails = new List<string>();

                try
                {

                    SqlCommand command = new SqlCommand(LocacaoFuncionario.SELECTFUNCIONARIOPORLOCACAO, connection);
                    command.Parameters.AddWithValue("@LocacaoID", locacaoId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        emails.Add(reader["Email"].ToString());
                    }
                    reader.Close();

                    var funcionarioController = new FuncionarioController();
                    foreach(string email in emails)
                    {
                        Funcionario funcionario = funcionarioController.BuscarFuncionarioPorEmail(email);
                        if (funcionario != null)
                            funcionarios.Add(funcionario);
                    }
                    return funcionarios;

                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar funcionarios por locacao: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao listar funcionarios por loacao: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
