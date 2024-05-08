namespace TokenizingPractice
{
	public abstract class Expression
	{
		public abstract float Evaluate();
	}

	public class Number(float value) : Expression 
	{
		public float Value = value;

		public override float Evaluate() => Value;

		public override string ToString() => Value.ToString();
	}

	public abstract class Operand(Expression left, Expression right) : Expression
	{
		public Expression Left = left;

		public Expression Right = right;

		public override string ToString() => $"{GetType().Name}({Left}, {Right})";
	}

	public class Addition(Expression left, Expression right) : Operand(left, right)
	{
		public override float Evaluate() => Left.Evaluate() + Right.Evaluate();
	}

	public class Subtraction(Expression left, Expression right) : Operand(left, right)
	{
		public override float Evaluate() => Left.Evaluate() - Right.Evaluate();
	}

	public class Multiplication(Expression left, Expression right) : Operand(left, right)
	{
		public override float Evaluate() => Left.Evaluate() * Right.Evaluate();
	}

	public class Division(Expression left, Expression right) : Operand(left, right)
	{
		public override float Evaluate() => Left.Evaluate() / Right.Evaluate();
	}

	public class Negative(Expression value) : Expression
	{
		public Expression Value = value;

		public override float Evaluate() => -Value.Evaluate();

		public override string ToString() => $"(-{Value})";

	}

	public class Function(string name, List<Expression> parameter) : Expression
	{
		public string Name = name;

		public List<Expression> Parameter = parameter;

		public override float Evaluate() => Definitions.ExistingFunctionsTable[Name].FunctionDelegate(Parameter);

		public override string ToString() => $"{Name}({Parameter[..^1]})";
	}

	public class Variable(string name) : Expression
	{
		public string Name = name;

		public override float Evaluate()
		{
			if (Definitions.IsUserDefined(Name, out var value))
				return value.Value;

			return 0;
		}
	}
}
