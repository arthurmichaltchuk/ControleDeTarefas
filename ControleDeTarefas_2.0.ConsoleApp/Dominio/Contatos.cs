using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.Dominio
{
    public class Contatos : EntidadeBase
    {
        public Contatos(string nome, string email, string telefone, string empresa, string cargo)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Empresa = empresa;
            Cargo = cargo;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Empresa { get; set; }

        public string Cargo { get; set; }

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
            if (telefone.Length > 8 && telefone.Length < 11)
                return true;
            return false;
        }
    }
}
