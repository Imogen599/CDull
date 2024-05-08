namespace TokenizingPractice
{
	public class Tokenizer
	{
		public Token CurrentToken
		{
			get;
			private set;
		}

		public bool AtEnd => Index >= line.Length - 1;

		private readonly string line;

		public int Index
		{
			get;
			private set;
		}

		public Tokenizer(string codeLine)
		{
			line = codeLine;
			Index = 0;
			AdvanceToken();
		}

		public bool AdvanceToken()
		{
			if (Index >= line.Length)
				return false;

			for (; Index < line.Length; Index++)
			{
				var character = line[Index];
				var text = character.ToString();

				if (character == ' ')
					continue;

				if (char.IsNumber(character))
				{
					CurrentToken = ProcessNumber(line);
					return true;

				}
				else if (char.IsLetter(character))
				{
					CurrentToken = ProcessText(line);
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
					Index++;
					return true;
				}
			}
			return false;
		}

		private Token ProcessNumber(string codeAsText)
		{
			var numberText = string.Empty;
			bool numberIsDecimal = false;
			for (; Index < codeAsText.Length; Index++)
			{
				var character2 = codeAsText[Index];

				if (char.IsDigit(character2) || (character2 == '.' && !numberIsDecimal))
					numberText += character2;
				else
					break;

				if (character2 == '.')
					numberIsDecimal = true;
			}
			return new(TokenType.Number, numberText);
		}

		private Token ProcessText(string codeAsText)
		{
			var firstWord = string.Empty;
			for (; Index < codeAsText.Length; Index++)
			{
				var character = codeAsText[Index];

				if (char.IsLetter(character))
					firstWord += character;
				else
					break;
			}
			
			if (firstWord == Definitions.DefinitionKeyword)
			{
				var variableName = string.Empty;
				for (; Index < codeAsText.Length; Index++)
				{
					var character = codeAsText[Index];

					if (char.IsLetter(character))
						variableName += character;
					else if (character is ' ' && variableName == string.Empty)
						continue;
					else
						break;
				}

				if (Definitions.IsUserDefined(variableName, out var _))
					throw new CompileException($"Variable '{variableName}' is already defined!");

				if (Definitions.ExistingConstants.TryGetValue(variableName, out var _))
					throw new CompileException($"'{variableName}' is ");

				while (Index < codeAsText.Length)
				{
					Index++;
					var character = codeAsText[Index];
					if (character == ' ')
						continue;
					if (character != '=')
						throw new CompileException($"Missing '=' after variable declaration!");
					break;
				}
				return new(TokenType.Identifier, variableName);
			}

			if (Definitions.IsUserDefined(firstWord, out var value))
				return new(TokenType.Number, value.Value.ToString());

			if (Definitions.ExistingConstants.TryGetValue(firstWord, out var value2))
				return new(TokenType.Number, value2.ToString());

			if (!Definitions.ExistingFunctionsTable.TryGetValue(firstWord, out var _))
				throw new CompileException($"The name '{firstWord}' does not exist in the current context!");
			return new(TokenType.Function, firstWord);
		}
	}
}
