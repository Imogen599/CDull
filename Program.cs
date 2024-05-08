namespace CDull
{
	public enum TokenType
	{
		Add,
		Subtract,
		Multiply,
		Divide,
		OpenParentheses,
		CloseParentheses,
		Number,
		Function,
		Identifier,
		Invalid
	}

	public readonly struct Token(TokenType type, string value)
	{
		public readonly TokenType Type = type;

		public readonly string Value = value;

		public override string ToString() => $"Type: {Type}, Value: {Value}";
	}

	internal class Program
	{
		public const string fileName = "MyProgram.cdull";

		public static string FilePath => Environment.CurrentDirectory + Path.DirectorySeparatorChar + $"{fileName}";

		internal static int CurrentLineNumber
		{
			get;
			private set;
		}

		static void Main()
		{
			if (!File.Exists(FilePath))
			{
				Console.WriteLine($"File not found! Path: {FilePath}");
				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
				return;
			}

			string codeAsText = File.ReadAllText(FilePath);
			if (codeAsText is null or "")
			{
				Console.WriteLine("File is empty or invalid!");
				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
				return;
			}

			var lines = codeAsText.Split("\n");
			List<Expression> expressions = [];
			foreach (var line in lines)
			{
				if (line.Length == 0)
					continue;
				CurrentLineNumber++;
				Tokenizer tokenizer = new(line.Replace("\r", "").Replace("\n", "\n"));
				expressions.Add(Parser.ParseTokens(tokenizer));
			}
			Console.WriteLine("Result: " + expressions.Last().Evaluate());
			Console.WriteLine("Press enter to exit.");
			Console.ReadLine();
		}
	}
}
