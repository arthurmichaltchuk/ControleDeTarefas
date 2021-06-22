using ControleDeTarefas_2._0.ConsoleApp.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.Controladores
{
    public class ControladorTarefa : Controlador<Tarefa>
    {
        public string enderecoDBTarefas = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";
        public List<Tarefa> ObterTodasTarefas()
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                [ID],
                [TITULO],
                [PRIORIDADE],
                [DATACRIACAO],
                [DATACONCLUSAO],
                [PERCENTUALCONCLUIDO]
                    FROM 
                        TBTAREFAS";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string nome = Convert.ToString(leitorTarefas["TITULO"]);
                int prioridade = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                int percentualConcluido = Convert.ToInt32(leitorTarefas["PERCENTUALCONCLUIDO"]);

                Tarefa Ta = new Tarefa(nome, prioridade, dataCriacao, dataConclusao, percentualConcluido);
                Ta.Id = id;

                tarefas.Add(Ta);
            }

            conexaoComBanco.Close();

            return tarefas;
        }

        public override string InserirNovo(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
            @"INSERT INTO TBTAREFAS
                (                
                [TITULO],
                [PRIORIDADE],
                [DATACRIACAO],
                [DATACONCLUSAO],
                [PERCENTUALCONCLUIDO]
                )
            VALUES
                (
                @TITULO,
                @PRIORIDADE,
                @DATACRIACAO,
                @DATACONCLUSAO,
                @PERCENTUALCONCLUIDO
                );";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;
            try
            {
                comandoInsercao.Parameters.AddWithValue("TITULO", tarefa.Titulo);
                comandoInsercao.Parameters.AddWithValue("PRIORIDADE", tarefa.Prioridade);
                comandoInsercao.Parameters.AddWithValue("DATACRIACAO", tarefa.DataCriacao);
                comandoInsercao.Parameters.AddWithValue("DATACONCLUSAO", tarefa.DataConclusao);
                comandoInsercao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", tarefa.PercentualConcluido);

                object id = comandoInsercao.ExecuteScalar();

                tarefa.Id = Convert.ToInt32(id);

                conexaoComBanco.Close();
                return "Registrado com sucesso!";
            }
            catch
            {
                return "Erro ao registrar!";
            }
        }

        public override Tarefa SelecionarRegistroPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [TITULO],
                        [PRIORIDADE],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUALCONCLUIDO]
                    FROM 
                        TBTAREFAS
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            if (leitorTarefas.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorTarefas["ID"]);
            string nome = Convert.ToString(leitorTarefas["TITULO"]);
            int prioridade = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);
            DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
            DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
            int percentualConcluido = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);

            Tarefa Ta = new Tarefa(nome, prioridade, dataCriacao, dataConclusao, percentualConcluido);
            Ta.Id = id;

            conexaoComBanco.Close();

            return Ta;
        }

        public override string EditarRegistro(int idSelecionado, Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TBTAREFAS 
	                SET	
                        [TITULO] = @TITULO,
                        [PRIORIDADE] = @PRIORIDADE,
                        [DATACRIACAO] = @DATACRIACAO,
                        [PERCENTUALCONCLUIDO] = @PERCENTUALCONCLUIDO
	                WHERE 
		                [ID] = @ID";
            try
            {
                comandoAtualizacao.CommandText = sqlAtualizacao;

                comandoAtualizacao.Parameters.AddWithValue("ID", idSelecionado);
                comandoAtualizacao.Parameters.AddWithValue("TITULO", tarefa.Titulo);
                comandoAtualizacao.Parameters.AddWithValue("PRIORIDADE", tarefa.Prioridade);
                comandoAtualizacao.Parameters.AddWithValue("DATACRIACAO", tarefa.DataCriacao);
                comandoAtualizacao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", tarefa.PercentualConcluido);

                comandoAtualizacao.ExecuteNonQuery();

                conexaoComBanco.Close();
                return "Atualizado com sucesso!";
            }
            catch
            {
                return "Erro ao atualizar!";
            }
        }

        public override string ExcluirRegistro(Tarefa registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBTAREFAS 	                
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
    }
}
