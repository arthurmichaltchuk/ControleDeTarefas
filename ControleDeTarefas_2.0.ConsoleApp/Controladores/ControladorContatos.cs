using ControleDeTarefas_2._0.ConsoleApp.Dominio;
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
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBContatos;
            conexaoComBanco.Open();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

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
                comandoAtualizacao.CommandText = sqlAtualizacao;

                comandoAtualizacao.Parameters.AddWithValue("ID", idSelecionado);
                comandoAtualizacao.Parameters.AddWithValue("NOME", registro.Nome);
                comandoAtualizacao.Parameters.AddWithValue("EMAIL", registro.Email);
                comandoAtualizacao.Parameters.AddWithValue("TELEFONE", registro.Telefone);
                comandoAtualizacao.Parameters.AddWithValue("EMPRESA", registro.Empresa);
                comandoAtualizacao.Parameters.AddWithValue("CARGO", registro.Cargo);

                comandoAtualizacao.ExecuteNonQuery();

                conexaoComBanco.Close();
                return "Atualizado com sucesso!";
            }
            catch
            {
                return "Erro ao atualizar!";
            }
        }

        public override string ExcluirRegistro(Contatos registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBContatos;
            conexaoComBanco.Open();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBCONTATOS 	                
	                WHERE 
		                [ID] = @ID";
            try
            {
                comandoExclusao.CommandText = sqlExclusao;

                comandoExclusao.Parameters.AddWithValue("ID", registro.Id);

                comandoExclusao.ExecuteNonQuery();

                conexaoComBanco.Close();
                return "Excluído com sucesso!";
            }
            catch
            {
                return "Erro ao Excluir";
            }
        }

        public override string InserirNovo(Contatos registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBContatos;
            conexaoComBanco.Open();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

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

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;
            try
            {
                comandoInsercao.Parameters.AddWithValue("NOME", registro.Nome);
                comandoInsercao.Parameters.AddWithValue("EMAIL", registro.Email);
                comandoInsercao.Parameters.AddWithValue("TELEFONE", registro.Telefone);
                comandoInsercao.Parameters.AddWithValue("EMPRESA", registro.Empresa);
                comandoInsercao.Parameters.AddWithValue("CARGO", registro.Cargo);

                object id = comandoInsercao.ExecuteScalar();

                registro.Id = Convert.ToInt32(id);

                conexaoComBanco.Close();
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
    }
}
