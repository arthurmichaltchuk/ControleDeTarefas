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
    public class TelaCompromisso : TelaCadastros<Compromisso>, ICadastravel
    {
        readonly ControladorCompromisso controladorCompromisso;
        static ControladorContatos controladorContatos = new ControladorContatos();
        TelaContato TelaContato = new TelaContato(controladorContatos);

        public TelaCompromisso(ControladorCompromisso controlador) : base("Cadastro de Contatos", controlador)
        {
            controladorCompromisso = controlador;
        }


        public override string ObterOpcao()
        {
            ConfigurarTitulo("MENU DE COMPROMISSOS");

            Console.WriteLine("Digite 1 para inserir novo compromisso");
            Console.WriteLine("Digite 2 para visualizar compromissos");
            Console.WriteLine("Digite 3 para editar compromisso");
            Console.WriteLine("Digite 4 para excluir um compromisso");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public override Compromisso ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite o assunto do compromisso: ");
            string assunto = Console.ReadLine();

            Console.Write("Digite o local do encontro: ");
            string local = Console.ReadLine();

            Console.Write("Digite a data do compromisso (xx/xx/xxxx): ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite a hora de inicio (xx:xx): ");
            string horaInicio = Console.ReadLine();

            Console.Write("Digite a hora de termino (xx:xx): ");
            string horaTermino = Console.ReadLine();

            int idContato = 0;
            if (tipoAcao == TipoAcao.Inserindo)
            {
                TelaContato.Visualizar(TipoAcao.Inserindo);
                Console.Write("Digite o ID do contato: ");
                idContato = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Digite o link da reuniao: ");
            string link = Console.ReadLine();

            if (Validar(data, horaInicio, horaTermino, controladorCompromisso.ObterTodosCompromissos()) != "VALIDO")
            {
                ApresentarMensagem("Hora Invalida! tente novamente...", TipoMensagem.Erro);
                ObterRegistro(TipoAcao.Inserindo);       
            }
                   
            Compromisso C = new Compromisso(assunto, local, data, horaInicio, horaTermino, idContato, link);

            return C;
        }

        public override void Visualizar(TipoAcao tipoAcao)
        {
            Console.Clear();
            ConfigurarTela("Visualizando todos compromissos...");
            List<Compromisso> compromissos = controladorCompromisso.ObterTodosCompromissos();

            string opcao = "";
            if (tipoAcao != TipoAcao.Editando)
            {
                Console.WriteLine("Digite 1 para filtrar por data...");
                Console.WriteLine("Digite 2 visualizar todos...");
                opcao = Console.ReadLine();
            }
            else
                opcao = "2";
            Console.Clear();

            if (compromissos?.Any() != true)
                ApresentarMensagem("SEM COMPROMISSOS CADASTRADOS", TipoMensagem.Atencao);
            else if (opcao == "1")
            {
                Console.WriteLine("Digite a data inicial do filtro...");
                DateTime dataInicial = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Digite a data final do filtro...");
                DateTime dataFinal = Convert.ToDateTime(Console.ReadLine());

                foreach (Compromisso item in compromissos)
                {
                    if (item.Data > dataInicial && item.Data < dataFinal)
                    {
                        Console.WriteLine("ID: " + item.Id);
                        Console.WriteLine("Assunto: " + item.Assunto);
                        Console.WriteLine("Local: " + item.Local);
                        Console.WriteLine("Data: " + item.Data);
                        Console.WriteLine("Hora Inicio: " + item.HoraInicio);
                        Console.WriteLine("Hora Termino: " + item.HoraTermino);
                        Console.WriteLine("Link: " + item.Link);
                        Console.WriteLine("\nContato: ");
                        Contatos contatos = controladorContatos.SelecionarRegistroPorId(item.IdContato);
                        Console.WriteLine("Nome: " + contatos.Nome);
                        Console.WriteLine("Telefone: " + contatos.Telefone);
                        Console.WriteLine("Email: " + contatos.Email);
                        Console.WriteLine();
                    }
                }
            }
            else if (opcao == "2")
            {
                foreach (Compromisso item in compromissos)
                {
                    Console.WriteLine("ID: " + item.Id);
                    Console.WriteLine("Assunto: " + item.Assunto);
                    Console.WriteLine("Local: " + item.Local);
                    Console.WriteLine("Data: " + item.Data);
                    Console.WriteLine("Hora Inicio: " + item.HoraInicio);
                    Console.WriteLine("Hora Termino: " + item.HoraTermino);
                    Console.WriteLine("Link: " + item.Link);
                    Console.WriteLine("\nContato: ");
                    Contatos contatos = controladorContatos.SelecionarRegistroPorId(item.IdContato);
                    Console.WriteLine("Nome: " + contatos.Nome);
                    Console.WriteLine("Telefone: " + contatos.Telefone);
                    Console.WriteLine("Email: " + contatos.Email);

                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }

        public string Validar(DateTime data, string horaInicio, string horaTermino, List<Compromisso> compromissos)
        {
            if (!horaInicio.Contains(":") && !horaTermino.Contains(":"))
                return "INVALIDO0";

            DateTime horaInicial = Convert.ToDateTime(horaInicio);
            DateTime horaFinal = Convert.ToDateTime(horaTermino);

            foreach (Compromisso item in compromissos)
            {
                DateTime horaInicialParaComparar = Convert.ToDateTime(item.HoraInicio);
                DateTime horaFinalParaComparar = Convert.ToDateTime(item.HoraTermino);

                if (data == item.Data)
                {
                    if (horaInicial > horaInicialParaComparar && horaInicial < horaFinalParaComparar)
                    {
                        return "INVALIDO1";
                    }
                    else if (horaFinal > horaInicialParaComparar)
                    {
                        return "INVALIDO2";
                    }
                }

            }    
            return "VALIDO";
        }
    }
}
