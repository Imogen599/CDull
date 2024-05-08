namespace TokenizingPractice
{
	public class CompileException(string message) : Exception($"Error at line {Program.CurrentLineNumber}, position {Parser.Tokenizer.Index}: " + message)
	{
	}
}
