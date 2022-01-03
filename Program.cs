using System;

namespace DIO.Series
{
    internal static class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        public static void Main()
        {
            int opcaoUsuario = ObterOpcaoUsuario();

			while (!opcaoUsuario.Equals(7))
			{
				var casos = new Dictionary<int,Action>();
				casos[1] = ListarSeries;
				casos[2] = InserirSerie;
				casos[3] = AtualizarSerie;
				casos[4] = ExcluirSerie;
				casos[5] = VisualizarSerie;
				casos[6] = Console.Clear;

				casos[opcaoUsuario].Invoke();
				opcaoUsuario = ObterOpcaoUsuario();
			}
			Console.WriteLine("Obrigado. Volte sempre!");
			Console.ReadLine();
        }

        private static int ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("Digite a opção desejada: ");

			Console.WriteLine("1 - Listar séries");
			Console.WriteLine("2 - Inserir nova série");
			Console.WriteLine("3 - Atualizar série");
			Console.WriteLine("4 - Excluir série");
			Console.WriteLine("5 - Visualizar série");
			Console.WriteLine("6 - Limpar Console");
			Console.WriteLine("7 - Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine();
			Console.WriteLine();
			return Convert.ToInt32(opcaoUsuario);

		}

        private static void ListarSeries()
		{
			Console.WriteLine("Listar séries");

			var lista = repositorio.Listar();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.RetornarExcluido();
				Console.WriteLine("#ID {0}: - {1} {2}", serie.RetornarId(), serie.RetornarTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova série");

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}

			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(repositorio.ProximoId(),(Genero)entradaGenero,entradaTitulo, entradaDescricao, entradaAno);

			repositorio.Inserir(novaSerie);
		}

        private static void AtualizarSerie()
		{
			Console.Write("Digite o ID da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}

			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(indiceSerie,(Genero)entradaGenero,entradaTitulo,entradaDescricao, entradaAno);

			repositorio.Atualizar(indiceSerie, atualizaSerie);
		}

        private static void ExcluirSerie()
		{
			Console.Write("Digite o ID da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			repositorio.Excluir(indiceSerie);
		}

        private static void VisualizarSerie()
		{
			Console.Write("Digite o ID da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornarPorId(indiceSerie);
			Console.WriteLine(serie);
		}
    }
}
