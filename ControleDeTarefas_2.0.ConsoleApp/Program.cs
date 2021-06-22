using ControleDeTarefas_2._0.ConsoleApp.ConsoleApp;
using ControleDeTarefas_2._0.ConsoleApp.Telas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp
{
    class Program
    {
        static TelaPrincipal telaPrincipal = new TelaPrincipal();

        static void Main(string[] args)
        {
            while (true)
            {
                ICadastravel telaSelecionada = (ICadastravel)telaPrincipal.ObterTela();

                if (telaSelecionada == null)
                    break;

                Console.Clear();

                string opcao = telaSelecionada.ObterOpcao();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    continue;

                    if (opcao == "1")
                        telaSelecionada.InserirNovoRegistro();

                    else if (opcao == "2")
                        telaSelecionada.VisualizarRegistros();

                    else if (opcao == "3")
                        telaSelecionada.EditarRegistro();

                    else if (opcao == "4")
                        telaSelecionada.ExcluirRegistro();
                
                Console.Clear();
            }
        }
    }
}