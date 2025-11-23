using Locadora.Controller;
using Locadora.Models;

LocacaoController locacaoController = new LocacaoController();

Locacao locacao = new Locacao(2, 1, 200.00m, 6);

try
{
    locacaoController.AdicionarLocacao(locacao);
}
catch (Exception ex)
{
    Console.WriteLine("Erro: " + ex.Message);
}