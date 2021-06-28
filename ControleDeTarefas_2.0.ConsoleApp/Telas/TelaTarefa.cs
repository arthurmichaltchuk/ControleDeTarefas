using ControleDeTarefas_2._0.ConsoleApp.ConsoleApp;
using ControleDeTarefas_2._0.ConsoleApp.ConsoleApp.Telas;
using ControleDeTarefas_2._0.ConsoleApp.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.Telas
{
    public class TelaTarefa : TelaCadastros<Tarefa>, ICadastravel
    {
        readonly ControladorTarefa controladorTarefa;

        public TelaTarefa(ControladorTarefa controlador) : base("Cadastro de Caixas", controlador)
        {
            controladorTarefa = controlador;
        }

        public override string ObterOpcao()
        {
            ConfigurarTitulo("MENU DE TAREFAS");

            Console.WriteLine("Digite 1 para inserir nova tarefa");
            Console.WriteLine("Digite 2 para visualizar tarefas");
            Console.WriteLine("Digite 3 para editar tarefas");
            Console.WriteLine("Digite 4 para excluir uma tarefa");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public override Tarefa ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a prioridade da tarefa: \n 1- Baixa\n 2-Média\n 3-Alta\n");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            DateTime dataCriacao = new DateTime();
            if (tipoAcao == TipoAcao.Inserindo)
            {
                Console.Write("Digite a data de criação tarefa:    (dd/mm/aaaa)\n");
                dataCriacao = Convert.ToDateTime(Console.ReadLine());
            }

            Console.Write("Digite o percentual concluido da tarefa:    (0% até 100%)\n");
            int percentualConcluido = Convert.ToInt32(Console.ReadLine());

            DateTime dataConclusao;
            if (percentualConcluido >= 100)
            {
                Console.Write("Digite a data de conclusão tarefa:    (dd/mm/aaaa)\n");
                dataConclusao = Convert.ToDateTime(Console.ReadLine());
            }
            else
                dataConclusao = Convert.ToDateTime("1/1/2000");

            Tarefa tarefa = new Tarefa(titulo, prioridade, dataConclusao, dataCriacao, percentualConcluido);

            return tarefa;
        }

        public override void Visualizar(TipoAcao tipoAcao)
        {
            Console.Clear();

            ConfigurarTela("Visualizando todas tarefas...");


            List<Tarefa> tarefas = controladorTarefa.ObterTodasTarefas();
            List<Tarefa> tarefasConcluidas = new List<Tarefa>();
            List<Tarefa> tarefasPorFazer = new List<Tarefa>();

            foreach (Tarefa tarefa in tarefas)
            {
                if (tarefa.PercentualConcluido >= 100)
                    tarefasConcluidas.Add(tarefa);
                else if (tarefa.PercentualConcluido < 100)
                    tarefasPorFazer.Add(tarefa);
            }

            Console.WriteLine(" TAREFAS PRONTAS...\n");
            if (EscreverTarefas(tarefasConcluidas, "concluidas"))            
                ApresentarMensagem("SEM TAREFAS CONCLUÍDAS", TipoMensagem.Atencao);

            Console.WriteLine(" TAREFAS EM ABERTO...\n");
            if (EscreverTarefas(tarefasPorFazer, "abertas"))
                ApresentarMensagem("SEM TAREFAS EM ABERTO", TipoMensagem.Atencao);
            Console.ReadKey();
        }

        //PRIVADOS
        private bool EscreverTarefas(List<Tarefa> tarefas, string tipo)
        {
            if (tarefas?.Any() != true)
                return true;
            foreach (Tarefa item in tarefas)
            {
                if (item.Prioridade == 3)
                    EscreverItens(item, tipo);
            }
            foreach (Tarefa item in tarefas)
            {
                if (item.Prioridade == 2)
                    EscreverItens(item, tipo);
            }
            foreach (Tarefa item in tarefas)
            {
                if (item.Prioridade == 1)
                    EscreverItens(item, tipo);
            }
            return false;
        }

        private static void EscreverItens(Tarefa item, string tipo)
        {
            Console.WriteLine("ID: " + item.Id);
            Console.WriteLine("Titulo: " + item.Titulo);
            Console.WriteLine("Prioridade: " + item.Prioridade);
            Console.WriteLine("Data Criação: " + item.DataCriacao.ToString("dd'/'MM'/'yyyy"));
            if(tipo == "concluidas")
                Console.WriteLine("Data Conclusão: " + item.DataConclusao.ToString("dd'/'MM'/'yyyy"));
            Console.WriteLine();
        }
    }
}
