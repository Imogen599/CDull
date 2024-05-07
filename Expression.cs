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
	}

	public abstract class Operand(Expression left, Expression right) : Expression
	{
		public Expression Left = left;

		public Expression Right = right;
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
		public Expression Value;

		public override float Evaluate() => -value.Evaluate();
	}
}
