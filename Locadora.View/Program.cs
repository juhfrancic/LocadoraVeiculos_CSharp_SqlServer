using Locadora.Controller;
using Locadora.Controller.Interfaces;
using Locadora.Models;
using Locadora.Models.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

//-----------------------------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------MENUS---------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------
int opcao;
do
{
    Console.WriteLine("\n---- MENU PRINCIPAL ----");
    Console.WriteLine("Para qual área gostaria de ir?");
    Console.WriteLine("1 - Clientes");
    Console.WriteLine("2 - Categorias e Veículos");
    Console.WriteLine("3 - Funcionários");
    Console.WriteLine("4 - Locações");
    Console.WriteLine("0 - Sair");
    Console.Write("Opção: ");
    opcao = int.Parse(Console.ReadLine());
    switch (opcao)
    {
        case 1:
            MenuCliente();
            break;
        case 2:
            MenuVeiculo();
            break;
        case 3:
            MenuFuncionario();
            break;
        case 4:
            MenuLocacao();
            break;
    }
} while (opcao != 0);
void MenuCliente()
{
    int opcaoCliente;
    do
    {
        Console.WriteLine("\n---- MENU CLIENTE ----");
        Console.WriteLine("1 - Cadastrar cliente");
        Console.WriteLine("2 - Listar todos os clientes");
        Console.WriteLine("3 - Buscar cliente por email");
        Console.WriteLine("4 - Atualizar telefone cliente");
        Console.WriteLine("5 - Atualizar documento cliente");
        Console.WriteLine("6 - Deletar cliente");
        Console.WriteLine("0 - Voltar ao menu principal");
        Console.WriteLine("Opção: ");
        opcaoCliente = int.Parse(Console.ReadLine());
        ClienteController clienteController = new ClienteController();
        string email;
        switch (opcaoCliente)
        {
            case 1:
                Console.WriteLine("Qual o nome do cliente?");
                string nome = Console.ReadLine();
                Console.WriteLine("Qual o email do cliente?");
                email = Console.ReadLine();
                var cliente = new Cliente(nome, email);
                Console.WriteLine("Qual o tipo do documento do cliente?");
                string tipoDocumento = Console.ReadLine();
                Console.WriteLine("Qual o número do documento do cliente?");
                string numeroDocumento = Console.ReadLine();
                Console.WriteLine("Qual a data de emissão do documento?");
                DateOnly dataEmissao = DateOnly.Parse(Console.ReadLine());
                Console.WriteLine("Qual a data de validade do documento?");
                DateOnly dataValidade = DateOnly.Parse(Console.ReadLine());
                var documento = new Documento(tipoDocumento, numeroDocumento, dataEmissao, dataValidade);
                try
                {
                    clienteController.AdicionarCliente(cliente, documento);
                    Console.WriteLine("Cliente adicionado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao cadastrar cliente: " + ex.Message);
                }
                break;
            case 2:
                try
                {
                    Console.WriteLine("---Lista de Clientes---");
                    List<Cliente> clientes = clienteController.ListarTodosClientes();
                    foreach (var c in clientes)
                    {
                        Console.WriteLine(c.ToString());
                        Console.WriteLine("-----------------------\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao listar clientes: " + ex.Message);
                }
                break;
            case 3:
                Console.WriteLine("Qual o email do cliente que deseja buscar?");
                email = Console.ReadLine();
                try
                {
                    cliente = clienteController.BuscaClientePorEmail(email);
                    Console.WriteLine(cliente);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar cliente: " + ex.Message);
                }
                break;
            case 4:
                Console.WriteLine("Qual o email do cliente que atualizar o telefone?");
                email = Console.ReadLine();
                Console.WriteLine("Qual o novo telefone do cliente?");
                string telefone = Console.ReadLine();
                try
                {
                    clienteController.AtualizarTelefoneCliente(telefone, email);
                    Console.WriteLine("Telefone atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar telefone: " + ex.Message);
                }
                break;
            case 5:
                Console.WriteLine("Qual o email do cliente que deseja atualizar o documento?");
                email = Console.ReadLine();
                Console.WriteLine("Qual o tipo do documento do cliente?");
                tipoDocumento = Console.ReadLine();
                Console.WriteLine("Qual o número do documento do cliente?");
                numeroDocumento = Console.ReadLine();
                Console.WriteLine("Qual a data de emissão do documento?");
                dataEmissao = DateOnly.Parse(Console.ReadLine());
                Console.WriteLine("Qual a data de validade do documento?");
                dataValidade = DateOnly.Parse(Console.ReadLine());
                documento = new Documento(tipoDocumento, numeroDocumento, dataEmissao, dataValidade);
                try
                {
                    clienteController.AtualizarDocumentoCliente(email, documento);
                    Console.WriteLine("Documento atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar documento: " + ex.Message);
                }
                break;
            case 6:
                Console.WriteLine("Qual o email do cliente que deseja deletar?");
                email = Console.ReadLine();
                try
                {
                    clienteController.DeletarClientePorEmail(email);
                    Console.WriteLine("Cliente deletado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao deletar cliente: " + ex.Message);
                }
                break;
        }
    } while (opcaoCliente != 0);
}

void MenuVeiculo()
{
    VeiculoController veiculoController = new VeiculoController();
CategoriaController categoriaController = new CategoriaController();
int opcaoVeiculo;
do
{
    Console.WriteLine("\n---- MENU VEÍCULO ----");
    Console.WriteLine("1 - Cadastrar Categoria");
    Console.WriteLine("2 - Listar Categorias com Veículos");
    Console.WriteLine("3 - Atualizar Categoria");
    Console.WriteLine("4 - Deletar Categoria");
    Console.WriteLine("5 - Cadastrar Veículo");
    Console.WriteLine("6 - Consultar Veículos por Categoria");
    Console.WriteLine("7 - Atualizar Status do Veículo");
    Console.WriteLine("0 - Voltar ao Menu Principal");
    Console.WriteLine("Opção: ");
    opcaoVeiculo = int.Parse(Console.ReadLine());
    string placa;
    string status;
    switch (opcaoVeiculo)
    {
        case 1:
            Console.WriteLine("Qual o nome do categoria?");
            string nome = Console.ReadLine();
            Console.WriteLine("Qual é a diária da categoria?");
            decimal diaria = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Gostaria de adicionar uma descrição para a categoria?\nAperte 1 para Sim e qualquer outra tecla para Não");
            int escolha = int.Parse(Console.ReadLine());
            string descricao = null;
            if (escolha == 1)
            {
                Console.WriteLine("Escreve uma breve descrição da categoria.\n");
                descricao = Console.ReadLine();
            }
            var categoria = new Categoria(nome, diaria, descricao);
            try
            {
                categoriaController.AdicionarCategoria(categoria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar categoria: " + ex.Message);
            }
            break;
        case 2:
            try
            {
                var veiculos = veiculoController.ListarTodosVeiculos();
                foreach (var veiculoLido in veiculos)
                {
                    Console.WriteLine(veiculoLido);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar veículos: " + ex.Message);
            }
            break;
        case 3:
            Console.WriteLine("Qual o id da categoria a ser atualizada?");
            int categoriaIdAtualizada = int.Parse(Console.ReadLine());
            Console.WriteLine("Qual o nome do categoria?");
            string nomeAtualizada = Console.ReadLine();
            Console.WriteLine("Qual é a diária da categoria?");
            decimal diariaAtualizada = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Gostaria de adicionar uma descrição para a categoria?\nAperte 1 para Sim e qualquer outra tecla para Não");
            int escolha2 = int.Parse(Console.ReadLine());
            string descricaoAtualizada = null;
            if (escolha2 == 1)
            {
                Console.WriteLine("Escreve uma breve descrição da categoria.\n");
                descricaoAtualizada = Console.ReadLine();
            }
            categoria = new Categoria(nomeAtualizada, diariaAtualizada, descricaoAtualizada);
            try
            {
                categoriaController.UpdateCategoria(categoria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar categoria: " + ex.Message);
            }
            break;
        case 4:
            Console.WriteLine("Qual o id da categoria a ser atualizada?");
            int categoriaIdDeletado = int.Parse(Console.ReadLine());
            try
            {
                categoriaController.DeleteCategoria(categoriaIdDeletado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao deletar categoria: " + ex.Message);
            }
            break;
        case 5:
            Console.WriteLine("Qual o id da categoria do veículo?");
            int categoriaId = int.Parse(Console.ReadLine());
            Console.WriteLine("Qual é a placa do veículo?");
            placa = Console.ReadLine();
            Console.WriteLine("Qual é a marca do veículo?");
            string marca = Console.ReadLine();
            Console.WriteLine("Qual é o modelo do veículo?");
            string modelo = Console.ReadLine();
            Console.WriteLine("Qual é o ano do veículo?");
            int ano = int.Parse(Console.ReadLine());
            Console.WriteLine("Qual é o status do veículo");
            EStatusVeiculo eStatus = (EStatusVeiculo)Enum.Parse(typeof(EStatusVeiculo), Console.ReadLine());
            try
            {
                var veiculo = new Veiculo(categoriaId, placa, marca, modelo, ano, eStatus);
                veiculoController.AdicionarVeiculo(veiculo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar veículo: " + ex.Message);
            }
            break;
        case 6:
            Console.WriteLine("Qual é o id categoria a ser consultada?");
            int categoriaIdBuscar = int.Parse(Console.ReadLine());
            try
            {
                var veiculosPorCategoria = veiculoController.BuscarVeiculosPorCategoria(categoriaIdBuscar);
                foreach (var veiculo in veiculosPorCategoria)
                {
                    Console.WriteLine(veiculo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar veículos por categoria: " + ex.Message);
            }
            break;
        case 7:
            Console.WriteLine("Qual a placa do veículo a atualizar?");
            placa = Console.ReadLine();
            Console.WriteLine("Qual o novo status do veículo?");
            status = Console.ReadLine();
            try
            {
                veiculoController.AtualizarStatusVeiculo(status, placa);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar status do veículo: " + ex.Message);
            }
            break;
    }
} while (opcaoVeiculo != 0);
}

void MenuFuncionario()
{
    FuncionarioController funcionarioController = new FuncionarioController();
int opcaoFuncionario;
do
{
    Console.WriteLine("\n---- MENU FUNCIONÁRIO ----");
    Console.WriteLine("1 - Cadastrar funcionário");
    Console.WriteLine("2 - Listar todos os funcionários");
    Console.WriteLine("3 - Atualizar funcionário");
    Console.WriteLine("4 - Deletar funcionário");
    Console.WriteLine("0 - Voltar ao menu principal");
    Console.WriteLine("Opção: ");
    opcaoFuncionario = int.Parse(Console.ReadLine());
    switch (opcaoFuncionario)
    {
        case 1:
            Console.WriteLine("Qual o nome do funcionário?");
            string nome = Console.ReadLine();
            Console.WriteLine("Qual o CPF do funcionário?");
            string cpf = Console.ReadLine();
            Console.WriteLine("Qual o email do funcionário?");
            string email = Console.ReadLine();
            Console.WriteLine("Qual o salário do funcionário?");
            decimal salario = decimal.Parse(Console.ReadLine());
            var funcionario = new Funcionario(nome, cpf, email, salario);
            try
            {
                funcionarioController.AdicionarFuncionario(funcionario);
                Console.WriteLine("Funcionário adicionado com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar funcionário: " + ex.Message);
            }
            break;
        case 2:
            try
            {
                Console.WriteLine("---Lista de Funcionários---");
                List<Funcionario> funcionarios = funcionarioController.ListarTodosFuncionarios();
                foreach (var f in funcionarios)
                {
                    Console.WriteLine(f.ToString());
                    Console.WriteLine("-----------------------\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar funcionários: " + ex.Message);
            }
            break;
        case 3:
            Console.WriteLine("Qual email do funcionário que desja atualizar?");
            email = Console.ReadLine();
            Console.WriteLine("Qual o novo nome do funcionário?");
            nome = Console.ReadLine();
            Console.WriteLine("Qual o novo salário do funcionário?");
            salario = decimal.Parse(Console.ReadLine());
            try
            {
                funcionarioController.AtualizarFuncionario(email, nome, salario);
                Console.WriteLine("Funcionário atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar funcionário: " + ex.Message);
            }
            break;
        case 4:
            Console.WriteLine("Qual email do funcionário que desja deletar?");
            email = Console.ReadLine();
            try
            {
                funcionarioController.DeletarFuncionario(email);
                Console.WriteLine("Funcionário deletado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao deletar funcionário: " + ex.Message);
            }
            break;
    }
} while (opcaoFuncionario != 0);
}
void MenuLocacao()
{
    LocacaoFuncionarioController locacaoFuncionarioController = new LocacaoFuncionarioController();
LocacaoController locacaoController = new LocacaoController();
int opcaoLocacao;
do
{
    Console.WriteLine("\n---- MENU LOCAÇÃO ----");
    Console.WriteLine("1 - Registrar nova locação");
    Console.WriteLine("2 - Associar funcionário a locação");
    Console.WriteLine("3 - Consultar locações ativas");
    Console.WriteLine("4 - Finalizar locação");
    Console.WriteLine("5 - Listar locações por cliente");
    Console.WriteLine("6 - Listar locações por funcionário");
    Console.WriteLine("7 - Listar funcionários de uma locação");
    Console.WriteLine("8 - Histórico de locações");
    Console.WriteLine("0 - Voltar ao menu principal");
    Console.WriteLine("Opção: ");
    opcaoLocacao = int.Parse(Console.ReadLine());
    switch (opcaoLocacao)
    {
        case 1:
            Console.WriteLine("Qual o ID do cliente que vai realizar a locação?");
            int clienteID = int.Parse(Console.ReadLine());
            Console.WriteLine("Qual o ID do veículo que será locado?");
            int veiculoID = int.Parse(Console.ReadLine());
            Console.WriteLine("Qual o valor da diária?");
            decimal valorDiaria = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Por quantos dias será a locação?");
            int diasLocacao = int.Parse(Console.ReadLine());
            var locacao = new Locacao(clienteID, veiculoID, valorDiaria, diasLocacao);
            List<string> emailsFuncionarios = new List<string>();
            Console.WriteLine("Digite os emails dos funcionários envolvidos (digite 'fim' para encerrar):");
            while (true)
            {
                string email = Console.ReadLine();
                if (email.ToLower() == "fim")
                    break;
                emailsFuncionarios.Add(email);
            }
            try
            {
                locacaoController.AdicionarLocacao(locacao, emailsFuncionarios);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao registrar nova locação: " + ex.Message);
            }
            break;
        case 2:
            Console.WriteLine("Qual o id do funcionário que deseja associar?");
            int funcionarioID = int.Parse(Console.ReadLine());
            Console.WriteLine("Qual o id da locação que deseja associar?");
            int locacaoID = int.Parse(Console.ReadLine());
            try
            {
                locacaoFuncionarioController.AdicionarLocacaoFuncionario(locacaoID, funcionarioID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao associar funcionário à locação:" + ex);
            }
            break;
        case 3:
            //Consultar locações ativas
            break;
        case 4:
            //Finalizar locação
            break;
        case 5:
            //Listar locações por cliente
            break;
        case 6:
            Console.WriteLine("Qual email do funcionário que deseja listar suas locações?");
            string emailFuncionario = Console.ReadLine();
            try
            {
                Console.WriteLine("---Lista de Locações por Funcionário---");
                List<Locacao> locacoesPorFuncionario = locacaoFuncionarioController.ListarLocacoesPorFuncionario(emailFuncionario);
                foreach (var l in locacoesPorFuncionario)
                {
                    Console.WriteLine(l.ToString());
                    Console.WriteLine("-----------------------\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar locações: " + ex.Message);
            }
            break;
        case 7:
            Console.WriteLine("Qual o id da locação que deseja listar os funcionários??");
            int idLocacao = int.Parse(Console.ReadLine());
            try
            {
                Console.WriteLine("---Lista de Funcionários por Locação---");
                List<Funcionario> funcionariosPorLocacao = locacaoFuncionarioController.ListarFuncionariosPorLocacao(idLocacao);
                foreach (var f in funcionariosPorLocacao)
                {
                    Console.WriteLine(f.ToString());
                    Console.WriteLine("-----------------------\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar locações: " + ex.Message);
            }
            break;
        case 8:
            try
            {
                Console.WriteLine("---Lista de Locações---");
                List<Locacao> locacoes = locacaoController.ListarTodasLocacoes();
                foreach (var l in locacoes)
                {
                    Console.WriteLine(l.ToString());
                    Console.WriteLine("-----------------------\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar locações: " + ex.Message);
            }
            break;
    }
} while (opcaoLocacao != 0);
}



//-----------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------

#region Testes CRUD Locação
LocacaoController locacaoController = new LocacaoController();

//locacaoController.CancelarLocacao(1);
Locacao locacao = new Locacao(2005, 3, 350.00m, DateTime.Parse("2025-11-25"));

//try
//{
//    locacaoController.ProcessarDevolucao(2004, DateTime.Parse("2025-11-29"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao processar devolução: " + ex.Message);
//}

//List<string> emailFuncionarios = new List<string>()
//{
//    "ana.costa@locadora.com",
//    "pedro.santos@locadora.com"
//};


//try
//{
//    locacaoController.AdicionarLocacao(locacao, emailFuncionarios);
//    Console.WriteLine($"Locação adicionada com sucesso! ID da Locação: {locacao.LocacaoID}");
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao adicionar locação: " + ex.Message);
//}

//try
//{
//    Console.WriteLine("---Lista de Locacoes---");
//    List<Locacao> locacoes = locacaoController.ListarTodasLocacoes();
//    foreach (var l in locacoes)
//    {
//        Console.WriteLine(l.ToString());
//        Console.WriteLine("-----------------------\n");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao listar locações: " + ex.Message);
//}
#endregion

//CancelarLocacao(int locacaoID, string novoStatus

var locacaoFuncionarioController = new LocacaoFuncionarioController();

//locacaoFuncionarioController.ListarLocacoesPorFuncionario();

//try
//{
//    Console.WriteLine("-----Lista de todas as Locacoes-----");
//    List<Locacao> locacoes = locacaoController.ListarTodasLocacoes();
//    foreach (var l in locacoes)
//    {
//        Console.WriteLine(l.ToString());
//        Console.WriteLine("-----------------------\n");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao listar todas as locações: " + ex.Message);
//}


//try
//{
//    Console.WriteLine("-----Lista de Locacoes Ativas-----");
//    List<Locacao> locacoes = locacaoController.ListarLocacoesAtivas();
//    foreach (var l in locacoes)
//    {
//        Console.WriteLine(l.ToString());
//        Console.WriteLine("-----------------------\n");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao listar locações ativas: " + ex.Message);
//}


//try
//{
//    Console.WriteLine("-----Lista de Locacoes por Cliente-----");
//    List<Locacao> locacoes = locacaoController.BuscarLocacoesPorClienteID(2005);
//    foreach (var l in locacoes)
//    {
//        Console.WriteLine(l.ToString());
//        Console.WriteLine("-----------------------\n");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao listar locações por cliente: " + ex.Message);
//}

//try
//{
//    var controllerLocacaoFucionario = new LocacaoFuncionarioController();
//    controllerLocacaoFucionario.ListarLocacoesPorFuncionario("ana.costa@locadora.com");
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao listar locações por funcionário: " + ex.Message);
//}

//try
//{
//    var controllerLocacaoFucionario = new LocacaoFuncionarioController();
//    controllerLocacaoFucionario.ListarFuncionariosPorLocacao(2007);
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao listar locações por funcionário: " + ex.Message);
//}

//try
//{
//    var controllerLocacaoFucionario = new LocacaoFuncionarioController();
//    controllerLocacaoFucionario.DeletarLocacaoFuncionario(2007);
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao listar locações por funcionário: " + ex.Message);
//}

