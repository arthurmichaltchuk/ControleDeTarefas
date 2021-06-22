using ControleDeTarefas_2._0.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.ConsoleApp
{
    public class Tarefa : EntidadeBase
    {
        public Tarefa(string titulo, int prioridade, DateTime dataCriacao, DateTime dataConclusao, int percentualConcluido)
        {
            Titulo = titulo;
            Prioridade = prioridade;
            DataCriacao = dataCriacao;
            DataConclusao = dataConclusao;
            PercentualConcluido = percentualConcluido;
        }

        public int Id { get; set; }

        public string Titulo { get; set; }

        public int Prioridade { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataConclusao { get; set; }

        public int PercentualConcluido { get; set; }

    }
}
