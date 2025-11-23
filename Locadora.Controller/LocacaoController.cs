using Locadora.Controller.Interfaces;
using Locadora.Models;
using Locadora.Models.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utils.Databases;

namespace Locadora.Controller
{
    public class LocacaoController : ILocacaoController
    {
        public void AdicionarLocacao(Locacao locacao)
        {
            var veiculoController = new VeiculoController();
            Veiculo veiculo = veiculoController.BuscarVeiculoPorId(locacao.VeiculoID);

            if (veiculo is null)
                throw new Exception("Veiculo não encontrado!");

            if (veiculo.StatusVeiculo != EStatusVeiculo.Disponivel)
                throw new Exception("Veículo não está disponível para a locação!");

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Locacao.INSERTLOCACAO, connection, transaction);

                    command.Parameters.AddWithValue("@ClienteID", locacao.ClienteID);
                    command.Parameters.AddWithValue("@VeiculoID", locacao.VeiculoID);
                    command.Parameters.AddWithValue("@DataLocacao", locacao.DataLocacao);
                    command.Parameters.AddWithValue("@DataDevolucaoPrevista", locacao.DataDevolucaoPrevista);
                    command.Parameters.AddWithValue("@ValorDiaria", locacao.ValorDiaria);
                    command.Parameters.AddWithValue("@DiasLocacao", locacao.DiasLocacao);
                    command.Parameters.AddWithValue("@Status", locacao.Status.ToString());

                    int locacaoId = Convert.ToInt32(command.ExecuteScalar());
                    locacao.setLocacaoId(locacaoId);

                    veiculoController.AtualizarStatusVeiculo(EStatusVeiculo.Alugado.ToString(), veiculoController.BuscarVeiculoPorId(locacao.VeiculoID).Placa);
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar locação: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro insperado ao adicionar locação: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Locacao> ListarTodasLocacoes()
        {
            throw new NotImplementedException();
        }
        public Locacao BuscarLocacaoPorID(int locacaoID)
        {
            using(SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                Locacao locacao = null;
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(Locacao.BUSCARLOCACAOPORID, connection);
                    command.Parameters.AddWithValue("@LocacaoID", locacaoID);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        locacao = new Locacao(
                             Convert.ToInt32(reader["ClienteID"]),
                             Convert.ToInt32(reader["VeiculoID"]),
                             Convert.ToDecimal(reader["ValorDiaria"]),
                             Convert.ToInt32(reader["DiasLocacao"])
                        );
                        locacao.setLocacaoId(Convert.ToInt32(reader["LocacaoID"]));

                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar locação por id: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro insperado ao buscar locação por id: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return locacao;
            }
        }
        public void AtualizarLocacao(Locacao locacao)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(Locacao.ATUALIZARLOCACAO, connection);

                    command.Parameters.AddWithValue("@Locacao", locacao.LocacaoID);
                    command.Parameters.AddWithValue("@DataDevolucaoReal", locacao.DataDevolucaoReal
                                                    ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ValorTotal", locacao.Va);
                    command.Parameters.AddWithValue("@Multa", locacao.)
                }
            }
        }
        public void DeletarLocacao(int locacaoID)
        {
            throw new NotImplementedException();
        }
    }
}
