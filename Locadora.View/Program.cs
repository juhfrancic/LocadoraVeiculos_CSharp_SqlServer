using Locadora.Controller;
using Locadora.Models;

Cliente cliente = new Cliente("Novo Cliente Agora com Documento", "xp@email.com.br");
Documento documento = new Documento("RG", "555567434", new DateOnly(2020, 1, 1), new DateOnly(2030, 1, 1));

//Console.WriteLine(cliente);

var clienteController = new ClienteController();

//documento.setClienteID(8);
//var documentoController = new DocumentoController();
//documentoController.AdicionarDocumento(documento);


try
{
    clienteController.AdicionarCliente(cliente, documento);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

//try
//{

//    var listadeClientes = clienteController.ListarTodosClientes();

//    foreach (var clientedaLista in listadeClientes)
//    {
//        Console.WriteLine(clientedaLista);

//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//clienteController.AtualizarTelefoneCliente("99999-9999", "novo@email.com.br");
//Console.WriteLine(clienteController.BuscaClientePorEmail("novo@email.com.br"));

//try
//{
//    clienteController.DeletarClientePorEmail("a@a.com");
//    Console.WriteLine("Cliente deletado com sucesso!");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}