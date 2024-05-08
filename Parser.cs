namespace TokenizingPractice
{
	public class Parser
	{
		internal static Tokenizer Tokenizer;

		public static Expression ParseTokens(Tokenizer tokenizer)
		{
			Tokenizer = tokenizer;
			var result = ParseTerm();
			if (tokenizer.AdvanceToken())
				throw new Exception("Tokens still remained after parsing, debug!");
			return result;
		}

		private static Expression ParseTerm()
		{
			var leftOperand = ParseFactor();

			if (Tokenizer.AtEnd)
				return leftOperand;

			var firstToken = Tokenizer.CurrentToken;
			if (firstToken.Type is TokenType.Add)
			{
				Tokenizer.AdvanceToken();
				var rightOperand = ParseFactor();
				return new Addition(leftOperand, rightOperand);
			}
			else if (firstToken.Type is TokenType.Subtract)
			{
				Tokenizer.AdvanceToken();
				var rightOperand = ParseFactor();
				return new Subtraction(leftOperand, rightOperand);
			}
			return leftOperand;
		}

		private static Expression ParseFactor()
		{
			var leftOperand = ParseUnary();

			if (Tokenizer.AtEnd)
				return leftOperand;

			var firstToken = Tokenizer.CurrentToken;
			if (firstToken.Type is TokenType.Multiply)
			{
				Tokenizer.AdvanceToken();
				var rightOperand = ParseUnary();
				return new Multiplication(leftOperand, rightOperand);
			}
			else if (firstToken.Type is TokenType.Divide)
			{
				Tokenizer.AdvanceToken();
				var rightOperand = ParseUnary();
				return new Division(leftOperand, rightOperand);
			}
			return leftOperand;
		}

		private static Expression ParseUnary()
		{
			var firstToken = Tokenizer.CurrentToken;
			if (firstToken.Type is TokenType.Subtract)
			{
				Tokenizer.AdvanceToken();

				return new Negative(ParseBasic());
			}
			else
				return ParseBasic();
		}

		private static Expression ParseBasic()
		{
			var firstToken = Tokenizer.CurrentToken;
			Tokenizer.AdvanceToken();

			if (firstToken.Type is TokenType.Number)
				return new Number(float.Parse(firstToken.Value));

			if (firstToken.Type is TokenType.OpenParentheses)
			{
				var term = ParseTerm();
				firstToken = Tokenizer.CurrentToken;
				if (firstToken.Type != TokenType.CloseParentheses)
					throw new CompileException("Unbalanced parentheses!");
				else
					Tokenizer.AdvanceToken();
				return term;
			}

			if (firstToken.Type is TokenType.Function)
			{
				var secondTerm = Tokenizer.CurrentToken;
				if (secondTerm.Type is not TokenType.OpenParentheses)
					throw new CompileException("Error, missing parentheses after function name!");
				Tokenizer.AdvanceToken();
				List<Expression> parameters = [];
				while (true)
				{
					parameters.Add(ParseTerm());

					if (Tokenizer.CurrentToken.Type is TokenType.CloseParentheses)
						break;
				}
				Tokenizer.AdvanceToken();

				if (!Definitions.ExistingFunctionsTable.TryGetValue(firstToken.Value, out var value))
					throw new CompileException($"Function '{firstToken.Value}' does not exist!");
				if (value.ArgCount != parameters.Count)
					throw new CompileException($"Incorrect number of parameters passed to function '{firstToken.Value}'! {value.ArgCount} were expected but only {parameters.Count} were found.");
				return new Function(firstToken.Value, parameters);
			}

			if (firstToken.Type is TokenType.Identifier)
			{
				Tokenizer.AdvanceToken();
				var expression = ParseTerm();
				Definitions.SetAsNewUserDefined(firstToken.Value, expression);
				return new Variable(firstToken.Value);
			}
			throw new CompileException($"Unexpected token: {firstToken}");
		}
	}
}
