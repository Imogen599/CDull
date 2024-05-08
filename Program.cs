namespace TokenizingPractice
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
		private const string fileName = "TestFile.code";

		static void Main()
		{
			if (!File.Exists(Environment.CurrentDirectory + $"/{fileName}"))
			{
				Console.WriteLine("File not found!");
				Console.ReadLine();
				return;
			}

			string codeAsText = File.ReadAllText(Environment.CurrentDirectory + $"/{fileName}");
			if (codeAsText is null or "")
			{
				Console.WriteLine("File is empty or invalid!");
				Console.ReadLine();
				return;
			}

			var lines = codeAsText.Split("\n");
			foreach (var line in lines)
			{
				List<Token> tokens = Tokenizer.GetTokensFromText(line.Replace("\r", ""));
				bool debug = true;

				Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
				var result = Parser.ParseTokens(tokens);

				// Very lazy and bad debug
				if (debug)
					Console.WriteLine(result.ToString());
				Console.WriteLine(result.Evaluate());
			}
			Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			Console.WriteLine("Press enter to exit.");
			Console.ReadLine();
		}
	}
}
