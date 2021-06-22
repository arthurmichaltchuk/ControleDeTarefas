using ControleDeTarefas_2._0.ConsoleApp.ConsoleApp;
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
    public abstract class TelaCadastros<T> : TelaBase where T : EntidadeBase
    {

        protected Controlador<T> controlador;


        public TelaCadastros(string titulo, Controlador<T> controlador)
        {
            this.controlador = controlador;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo registro...");

            T registro = ObterRegistro(TipoAcao.Inserindo);

            string VerificarSucesso = controlador.InserirNovo(registro);

            if (VerificarSucesso == "Registrado com sucesso!")
                ApresentarMensagem(VerificarSucesso, TipoMensagem.Sucesso);
            else
                ApresentarMensagem(VerificarSucesso, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando registro...");
            Visualizar();

            Console.WriteLine("Digite o ID do registro que deseja editar...");
            int idPesquisado = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            T registro = ObterRegistro(TipoAcao.Editando);
            string VerificarSucesso = controlador.EditarRegistro(idPesquisado, registro);
            
            if (VerificarSucesso == "Atualizado com sucesso!")
                ApresentarMensagem(VerificarSucesso, TipoMensagem.Sucesso);
            else
                ApresentarMensagem(VerificarSucesso, TipoMensagem.Erro);
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo registro...");
            Visualizar();

            Console.WriteLine("Digite o ID do registro que deseja excluir...");
            int idPesquisado = Convert.ToInt32(Console.ReadLine());

            T registro = controlador.SelecionarRegistroPorId(idPesquisado);
            string VerificarSucesso = controlador.ExcluirRegistro(registro);

            if (VerificarSucesso == "Excluído com sucesso!")
                ApresentarMensagem(VerificarSucesso, TipoMensagem.Sucesso);
            else
                ApresentarMensagem(VerificarSucesso, TipoMensagem.Erro);
        }

        public bool VisualizarRegistros()
        {
            Visualizar();
            return true;
        }

        public abstract T ObterRegistro(TipoAcao tipoAcao);

        public abstract void Visualizar();
    }
}

