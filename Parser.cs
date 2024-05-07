namespace TokenizingPractice
{
	public class Parser
	{
		public static Expression ParseTokens(List<Token> tokens)
		{
			var result = ParseTerm(tokens);
			if (tokens.Count != 0)
				throw new Exception("Tokens still remained after parsing, debug!");
			return result;
		}

		private static Expression ParseTerm(List<Token> tokens)
		{
			var leftOperand = ParseFactor(tokens);

			if (tokens.Count == 0)
				return leftOperand;

			var firstToken = tokens.First();
			if (firstToken.Type is TokenType.Add)
			{
				tokens.RemoveAt(0);
				var rightOperand = ParseFactor(tokens);
				return new Addition(leftOperand, rightOperand);
			}
			else if (firstToken.Type is TokenType.Subtract)
			{
				tokens.RemoveAt(0);
				var rightOperand = ParseFactor(tokens);
				return new Subtraction(leftOperand, rightOperand);
			}
			return leftOperand;
		}

		private static Expression ParseFactor(List<Token> tokens)
		{
			var leftOperand = ParseUnary(tokens);

			if (tokens.Count == 0)
				return leftOperand;

			var firstToken = tokens.First();
			if (firstToken.Type is TokenType.Multiply)
			{
				tokens.RemoveAt(0);
				var rightOperand = ParseUnary(tokens);
				return new Multiplication(leftOperand, rightOperand);
			}
			else if (firstToken.Type is TokenType.Divide)
			{
				tokens.RemoveAt(0);
				var rightOperand = ParseUnary(tokens);
				return new Division(leftOperand, rightOperand);
			}
			return leftOperand;
		}

		private static Expression ParseUnary(List<Token> tokens)
		{
			var firstToken = tokens.First();
			if (firstToken.Type is TokenType.Subtract)
			{
				tokens.RemoveAt(0);
				return new Negative(ParseBasic(tokens));
			}
			else
				return ParseBasic(tokens);
		}

		private static Expression ParseBasic(List<Token> tokens)
		{
			var firstToken = tokens.First();
			tokens.RemoveAt(0);
			if (firstToken.Type is TokenType.Number)
				return new Number(float.Parse(firstToken.Value));

			if (firstToken.Type is TokenType.OpenParentheses)
			{
				var term = ParseTerm(tokens);
				firstToken = tokens.First();
				if (firstToken.Type != TokenType.CloseParentheses)
					throw new Exception("Unbalanced parentheses!");
				else
					tokens.RemoveAt(0);
				return term;
			}
			throw new Exception($"Unexpected token: {firstToken}");
		}
	}
}
