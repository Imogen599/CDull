namespace TokenizingPractice
{
	public class Tokenizer
	{
		public Token CurrentToken
		{
			get;
			private set;
		}

		public bool AtEnd => index >= line.Length - 1;

		private string line;

		private int index;

		public void Initialize(string codeLine)
		{
			line = codeLine;
			index = 0;
			AdvanceToken();
		}

		public bool AdvanceToken()
		{
			if (index >= line.Length)
				return false;

			for (; index < line.Length; index++)
			{
				var character = line[index];
				var text = character.ToString();

				if (character == ' ')
					continue;

				if (char.IsNumber(character))
				{
					CurrentToken = ProcessNumber(line, ref index);
					return true;

				}
				else if (char.IsLetter(character))
				{
					CurrentToken = ProcessFunction(line, ref index);
					return true;
				}
				else
				{
					CurrentToken = character switch
					{
						'+' => new(TokenType.Add, text),
						'-' => new(TokenType.Subtract, text),
						'*' => new(TokenType.Multiply, text),
						'/' => new(TokenType.Divide, text),
						'(' => new(TokenType.OpenParentheses, text),
						')' => new(TokenType.CloseParentheses, text),
						_ => new(TokenType.Invalid, text),
					};
					index++;
					return true;
				}
			}
			return false;
		}

		public static List<Token> GetTokensFromText(string codeAsText)
		{
			var tokens = new List<Token>();

			for (int i = 0; i < codeAsText.Length; i++)
			{
				var character = codeAsText[i];
				var text = character.ToString();

				if (character == ' ')
					continue;

				Token token;

				if (char.IsNumber(character))
				{
					token = ProcessNumber(codeAsText, ref i);
				}
				else if (char.IsLetter(character))
				{
					token = ProcessFunction(codeAsText, ref i);
				}
				else
				{
					token = character switch
					{
						'+' => new(TokenType.Add, text),
						'-' => new(TokenType.Subtract, text),
						'*' => new(TokenType.Multiply, text),
						'/' => new(TokenType.Divide, text),
						'(' => new(TokenType.OpenParentheses, text),
						')' => new(TokenType.CloseParentheses, text),
						_ => new(TokenType.Invalid, text),
					};
				}
				tokens.Add(token);
			}
			return tokens;
		}

		private static Token ProcessNumber(string codeAsText, ref int index)
		{
			var numberText = string.Empty;
			bool numberIsDecimal = false;
			for (; index < codeAsText.Length; index++)
			{
				var character2 = codeAsText[index];

				if (char.IsDigit(character2) || (character2 == '.' && !numberIsDecimal))
					numberText += character2;
				else
					break;

				if (character2 == '.')
					numberIsDecimal = true;
			}
			return new(TokenType.Number, numberText);
		}

		private static Token ProcessFunction(string codeAsText, ref int index)
		{
			var functionText = string.Empty;
			for (; index < codeAsText.Length; index++)
			{
				var character2 = codeAsText[index];

				if (char.IsLetter(character2))
					functionText += character2;
				else
					break;
			}
			return new(TokenType.Function, functionText);
		}
	}
}
