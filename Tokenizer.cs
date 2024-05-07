namespace TokenizingPractice
{
	public class Tokenizer
	{
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
				{
				    index--;
					break;
				}
			}
			return new(TokenType.Number, numberText);
		}
	}
}
