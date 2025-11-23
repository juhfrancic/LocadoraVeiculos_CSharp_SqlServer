using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class LocacaoFuncionario
    {
        public readonly static string INSERTLOCACAOFUNCIONARIO = @"INSERT INTO LocacaoFuncionario (LocacaoId, FuncionarioId) 
                                                              VALUES (@LocacaoId, @FuncionarioId);";

        public readonly static string DELETELOCACAOFUNCIONARIO = @"DELETE FROM LocacaoFuncionario 
                                                              WHERE FuncionarioID = @FuncionarioID;";

        public readonly static string SELECTLOCACAOPORFUNCIONARIO = @"SELECT  f.Nome, f.Email, l.DataLocacao, l.Status, c.NomeCliente, v.ModeloVeiculo
                                                                              FROM LocacaoFuncionario lf
                                                                              INNER JOIN Funcionario f ON lf.FuncionarioID = f.FuncionarioID
                                                                              INNER JOIN Locacao l ON lf.LocacaoID = l.LocacaoID
                                                                              INNER JOIN Cliente c ON l.ClienteID = c.ClienteID
                                                                              INNER JOIN Veiculo v ON l.VeiculoID = v.VeiculoID
                                                                              WHERE f.Email = @Email;";

        public readonly static string SELECTFUNCIONARIOPORLOCACAO = @"SELECT f.FuncionarioID, f.Email
                                                            FROM LocacaoFuncionario lf
                                                            INNER JOIN Funcionario f ON lf.FuncionarioID = f.FuncionarioID
                                                            INNER JOIN Locacao l ON lf.LocacaoID = l.LocacaoID
                                                            WHERE l.LocacaoID = @LocacaoID;";

        public readonly static string VERIFICARSEEXISTE = @"SELECT COUNT(*) FROM LocacaoFuncionario 
                                                      WHERE LocacaoId = @LocacaoId AND FuncionarioId = @FuncionarioId;";
        public int LocacaoFuncionarioId { get; private set; }
        public int LocacaoId { get; private set; }
        public int FuncionarioId { get; private set; }
        public LocacaoFuncionario(int locacaoId, int funcionarioId)
        {
            LocacaoId = locacaoId;
            FuncionarioId = funcionarioId;
        }

        
    }
}
