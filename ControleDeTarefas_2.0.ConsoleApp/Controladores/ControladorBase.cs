using ControleDeTarefas_2._0.ConsoleApp.Dominio;
using ControleDeTarefas_2._0.ConsoleApp.Telas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.Controladores
{
    public abstract class Controlador<T> where T : EntidadeBase
    {
        public abstract string InserirNovo(T registro);

        public abstract T SelecionarRegistroPorId(int id);

        public abstract string EditarRegistro(int id, T registro);

        public abstract string ExcluirRegistro(T registro);
    }
}
