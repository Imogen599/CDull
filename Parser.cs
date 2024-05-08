namespace TokenizingPractice
{
	public class Parser
	{
		private static Tokenizer Tokenizer;
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
					throw new Exception("Unbalanced parentheses!");
				else
					Tokenizer.AdvanceToken();
				return term;
			}
			if (firstToken.Type is TokenType.Function)
			{
				//Tokenizer.AdvanceToken();
				var term = ParseUnary();
				var function = GetFunctionFromName(firstToken.Value);
				return new Function(function, term);
			}
			throw new Exception($"Unexpected token: {firstToken}");
		}

		private static Func<float, float> GetFunctionFromName(string name)
		{
			return name switch
			{
				"sin" => MathF.Sin,
				"cos" => MathF.Cos,
				"tan" => MathF.Tan,
				"log" => MathF.Log,
				"abs" => MathF.Abs,
				"floor" => MathF.Floor,
				"ceiling" => MathF.Ceiling,
				_ => input => input
			};
		}
	}
}
