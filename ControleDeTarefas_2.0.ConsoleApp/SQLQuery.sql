insert into TBTarefas
	(
		[Titulo],
		[Prioridade],
		[DataCriacao],
		[DataConclusao],
		[PercentualConcluido]
	)
	values
	(
		'Correr',
		3,
		'06/17/2021',
		'06/18/2021',
		30
	)

update TBTarefas
	set	
		[Titulo] = 'andar',
		[Prioridade] = 1,
		[DataCriacao] = '01/01/2020',
		[DataConclusao] = '02/02/2020',
		[PercentualConcluido] = 100
	where
		[Id] = 18

Delete from TBTarefas
	where
		[Id] = 1

select [Id], [Titulo], [Prioridade] from TBTarefas

select * from TBTarefas
	where
		[Id] = 4

select * from TBTarefas