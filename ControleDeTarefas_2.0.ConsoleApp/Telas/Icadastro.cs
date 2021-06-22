using ControleDeTarefas_2._0.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.Telas
{
    public interface ICadastravel
    {
        void InserirNovoRegistro();

        void EditarRegistro();

        void ExcluirRegistro();

        bool VisualizarRegistros();

        string ObterOpcao();
    }
}
