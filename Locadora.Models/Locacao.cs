using Locadora.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Locacao
    {
       public static readonly string INSERTLOCACAO = @"INSERT INTO tblLocacoes 
                                                      (ClienteID, VeiculoID, DataLocacao, DataDevolucaoPrevista, ValorDiaria, DiasLocacao, Status)
                                                      VALUES (@ClienteID, @VeiculoID, @DataLocacao, @DataDevolucaoPrevista, @ValorDiaria, @DiasLocacao, @Status);
                                                      SELECT SCOPE_IDENTITY();";
        public static readonly string BUSCARLOCACAOPORID = @"SELECT * FROM tblLocacoes WHERE LocacaoID = @LocacaoID";

        public static readonly string ATUALIZARLOCACAO = @"UPDATE tblLocacoes 
                                                           SET DataDevolucaoReal = @DataDevolucaoReal, 
                                                           ValorTotal = @ValorTotal, 
                                                           Multa = @Multa, 
                                                           Status = @Status
                                                           WHERE LocacaoID = @LocacaoID";

        public int LocacaoID { get; private set; }
        public int ClienteID { get; private set; }
        public int VeiculoID { get; private set; }
        public DateTime DataLocacao { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }
        public decimal ValorDiaria { get; private set; }
        public decimal ValorTotal { get; private set; }
        public int DiasLocacao { get; private set; }    
        public decimal Multa { get; private set; }
        public EStatusLocacao Status { get; private set; }
        public decimal MultaDiaria = 50.0m;

        public Locacao(int clienteID, int veiculoID, decimal valorDiaria, int diasLocacao)
        {
            ClienteID = clienteID;
            VeiculoID = veiculoID;
            DataLocacao = DateTime.Now;
            ValorDiaria = valorDiaria;
            DiasLocacao = diasLocacao;
            ValorTotal = valorDiaria * diasLocacao;
            DataDevolucaoPrevista = DateTime.Now.AddDays(diasLocacao);
            Status = EStatusLocacao.Ativa;
        }

        //TODO: Definir os valores de cliente e veiculo como nome e modelo respectivamente

        public void setLocacaoId(int idLocacao)
        {
            LocacaoID = idLocacao;
        }

        public void RegistrarDevolucao(DateTime dataDevolucaoReal)
        {
            DataDevolucaoReal = dataDevolucaoReal;
            Status = EStatusLocacao.Concluida;

            if (DataDevolucaoReal > DataDevolucaoPrevista)
            {
                int diasAtraso = (DataDevolucaoReal.Value - DataDevolucaoPrevista).Days;
                Multa = diasAtraso * MultaDiaria;
                ValorTotal += Multa;
                Console.WriteLine($"Devolução com atraso de {diasAtraso} dias. Multa aplicada: {Multa:C}");
            }
            else
            {
                Multa = 0;
            }
        } 

        public override string ToString()
        {
            return $"Cliente: {ClienteID}\nVeiculo: {VeiculoID}\n" +
                $"DataLocacao: {DataLocacao}\n" +
                $"DataDevolucaoPrevista: {DataDevolucaoPrevista}\n" +
                $"DataDevolucaoReal: {DataDevolucaoReal}\n" +
                $"ValorDiaria: {ValorDiaria}\nValorTotal: {ValorTotal}\n" +
                $"Multa: {Multa}\nStatus: {Status}";
        }
    }
}
