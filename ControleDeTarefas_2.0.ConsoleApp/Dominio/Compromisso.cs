using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.Dominio
{
    public class Compromisso : EntidadeBase
    {
        public Compromisso(string assunto, string local, DateTime data, string horaInicio, string horaTermino, int idContato, string link)
        {
            Assunto = assunto;
            Local = local;
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            IdContato = idContato;
            Link = link;
        }

        public int Id { get; set; }

        public string Assunto { get; set; }

        public string Local { get; set; }

        public DateTime Data { get; set; }

        public string HoraInicio { get; set; }

        public string HoraTermino { get; set; }

        public int IdContato { get; set; }

        public string Link { get; set; }
    }
}
