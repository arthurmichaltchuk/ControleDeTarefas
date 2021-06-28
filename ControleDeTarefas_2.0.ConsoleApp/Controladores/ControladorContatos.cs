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
    public class ControladorContatos : Controlador<Contatos>
    {
        public string enderecoDBContatos = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";
        public List<Contatos> ObterTodosContatos()
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBContatos;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
		                [NOME],
		                [EMAIL],
		                [TELEFONE],
		                [EMPRESA],
		                [CARGO]
                    FROM 
                        TBCONTATOS
                    ORDER BY [CARGO]";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            List<Contatos> contatos = new List<Contatos>();

            while (leitorContatos.Read())
            {
                int id = Convert.ToInt32(leitorContatos["ID"]);
                string nome = Convert.ToString(leitorContatos["NOME"]);
                string email = Convert.ToString(leitorContatos["EMAIL"]);
                string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
                string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
                string cargo = Convert.ToString(leitorContatos["CARGO"]);

                Contatos C = new Contatos(nome, email, telefone, empresa, cargo);
                C.Id = id;

                contatos.Add(C);
            }

            conexaoComBanco.Close();

            return contatos;
        }
        public override string EditarRegistro(int idSelecionado, Contatos registro)
        {
            string sqlAtualizacao =
                @"UPDATE TBCONTATOS 
	                SET	
                        [NOME] = @NOME,
                        [EMAIL] = @EMAIL,
                        [TELEFONE] = @TELEFONE,
                        [EMPRESA] = @EMPRESA,
                        [CARGO] = @CARGO
	                WHERE 
		                [ID] = @ID";
            try
            {
                ExecutarCodigo(sqlAtualizacao, registro, TipoExecucao.Editando, idSelecionado);
                return "Atualizado com sucesso!";
            }
            catch
            {
                return "Erro ao atualizar!";
            }
        }

        public override string ExcluirRegistro(Contatos registro)
        {
            string sqlExclusao =
                @"DELETE FROM TBCONTATOS 	                
	                WHERE 
		                [ID] = @ID";
            try
            {
                ExecutarCodigo(sqlExclusao, registro, TipoExecucao.Excluindo, 0);
                return "Excluído com sucesso!";
            }
            catch
            {
                return "Erro ao Excluir";
            }
        }

        public override string InserirNovo(Contatos registro)
        {
            string sqlInsercao =
            @"INSERT INTO TBCONTATOS
                (                
		        [NOME],
		        [EMAIL],
		        [TELEFONE],
		        [EMPRESA],
		        [CARGO]
                )
            VALUES
                (
                @NOME,
                @EMAIL,
                @TELEFONE,
                @EMPRESA,
                @CARGO
                );";

            try
            {
                ExecutarCodigo(sqlInsercao, registro, TipoExecucao.Inserindo, 0);
                return "Registrado com sucesso!";
            }
            catch
            {
                return "Erro ao registrar!";
            }
        }

        public override Contatos SelecionarRegistroPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBContatos;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
		                [NOME],
		                [EMAIL],
		                [TELEFONE],
		                [EMPRESA],
		                [CARGO]
                    FROM 
                        TBCONTATOS
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            if (leitorContatos.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorContatos["ID"]);
            string nome = Convert.ToString(leitorContatos["NOME"]);
            string email = Convert.ToString(leitorContatos["EMAIL"]);
            string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
            string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
            string cargo = Convert.ToString(leitorContatos["CARGO"]);

            Contatos C = new Contatos(nome, email, telefone, empresa, cargo);
            C.Id = id;

            conexaoComBanco.Close();

            return C;
        }

        private void ExecutarCodigo(string comando, Contatos registro, TipoExecucao tipoExecucao, int idSelecionado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBContatos;
            conexaoComBanco.Open();

            SqlCommand comandoExecucao = new SqlCommand();
            comandoExecucao.Connection = conexaoComBanco;

            if (tipoExecucao == TipoExecucao.Excluindo)
            {
                comandoExecucao.CommandText = comando;

                comandoExecucao.Parameters.AddWithValue("ID", registro.Id);

                comandoExecucao.ExecuteNonQuery();
            }
            else if (tipoExecucao == TipoExecucao.Editando)
            {
                comandoExecucao.CommandText = comando;

                comandoExecucao.Parameters.AddWithValue("ID", idSelecionado);
                comandoExecucao.Parameters.AddWithValue("NOME", registro.Nome);
                comandoExecucao.Parameters.AddWithValue("EMAIL", registro.Email);
                comandoExecucao.Parameters.AddWithValue("TELEFONE", registro.Telefone);
                comandoExecucao.Parameters.AddWithValue("EMPRESA", registro.Empresa);
                comandoExecucao.Parameters.AddWithValue("CARGO", registro.Cargo);

                comandoExecucao.ExecuteNonQuery();
            }
            else if (tipoExecucao == TipoExecucao.Inserindo)
            {
                comando +=
                @"SELECT SCOPE_IDENTITY();";

                comandoExecucao.CommandText = comando;
                comandoExecucao.Parameters.AddWithValue("NOME", registro.Nome);
                comandoExecucao.Parameters.AddWithValue("EMAIL", registro.Email);
                comandoExecucao.Parameters.AddWithValue("TELEFONE", registro.Telefone);
                comandoExecucao.Parameters.AddWithValue("EMPRESA", registro.Empresa);
                comandoExecucao.Parameters.AddWithValue("CARGO", registro.Cargo);

                object id = comandoExecucao.ExecuteScalar();

                registro.Id = Convert.ToInt32(id);
            }
            conexaoComBanco.Close();
        }
    }
}
