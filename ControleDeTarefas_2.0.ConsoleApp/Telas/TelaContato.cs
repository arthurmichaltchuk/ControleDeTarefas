using ControleDeTarefas_2._0.ConsoleApp.ConsoleApp.Telas;
using ControleDeTarefas_2._0.ConsoleApp.Controladores;
using ControleDeTarefas_2._0.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.Telas
{
    public class TelaContato : TelaCadastros<Contatos>, ICadastravel
    {
        readonly ControladorContatos controladorContatos;

        public TelaContato(ControladorContatos controlador) : base("Cadastro de Contatos", controlador)
        {
            controladorContatos = controlador;
        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo contato");
            Console.WriteLine("Digite 2 para visualizar contatos");
            Console.WriteLine("Digite 3 para editar contato");
            Console.WriteLine("Digite 4 para excluir um contato");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public override Contatos ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email do contato: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone do contato: (xxxxxxxx)");
            string telefone = Console.ReadLine();

            Console.Write("Digite a empresa do contato: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo do contato: ");
            string cargo = Console.ReadLine();

            if (Validar(email, telefone) != "VALIDO")
            {
                ApresentarMensagem("Email ou telefone Invalido! tente novamente...", TipoMensagem.Erro);
                ObterRegistro(TipoAcao.Inserindo);
            }
            
            Contatos C = new Contatos(nome, email, telefone, empresa, cargo);

            return C;
        }

        public override void Visualizar(TipoAcao tipoAcao)
        {
            Console.Clear();

            ConfigurarTela("Visualizando todos contatos...");

            List<Contatos> contatos = controladorContatos.ObterTodosContatos();

            if (contatos?.Any() != true)
                ApresentarMensagem("SEM CONTATOS CADASTRADOS", TipoMensagem.Atencao);

            foreach (Contatos contato in contatos)
            {
                Console.WriteLine("ID: " + contato.Id);
                Console.WriteLine("Nome: " + contato.Nome);
                Console.WriteLine("Email: " + contato.Email);
                Console.WriteLine("Telefone: " + contato.Telefone);
                Console.WriteLine("Empresa: " + contato.Empresa);
                Console.WriteLine("Cargo: " + contato.Cargo);
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        public string Validar(string email, string telefone)
        {
            if (ValidarEmail(email) && ValidarTelefone(telefone))
                return "VALIDO";
            return "INVALIDO";

        }
        private bool ValidarEmail(string email)
        {
            if (email.Contains("@") && email.Contains("."))
                return true;
            return false;
        }
        private bool ValidarTelefone(string telefone)
        {
            if (telefone.Length > 8 && telefone.Length < 13)
                return true;
            return false;
        }
    }
}
