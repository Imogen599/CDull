namespace TokenizingPractice
{
	public class Definitions
	{
		public delegate float BuiltInFunctionDelegate(List<Expression> args);

		internal readonly struct BuiltInFunction(BuiltInFunctionDelegate functionDelegate, int argCount)
		{
			public readonly BuiltInFunctionDelegate FunctionDelegate = functionDelegate;

			public readonly int ArgCount = argCount;
		}

		public const string DefinitionKeyword = "let";

		internal static readonly Dictionary<string, float> ExistingConstants = new()
		{
			["pioverfour"] = MathF.PI / 4f,
			["piovertwo"] = MathF.PI / 2f,
			["pi"] = MathF.PI,
			["tau"] = MathF.Tau,
			["twopi"] = MathF.Tau,
			["euler"] = MathF.E
		};

		internal static readonly Dictionary<string, BuiltInFunction> ExistingFunctionsTable = new()
		{
			["sin"] = new BuiltInFunction(args => MathF.Sin(args[0].Evaluate()), 1),
			["cos"] = new BuiltInFunction(args => MathF.Cos(args[0].Evaluate()), 1),
			["tan"] = new BuiltInFunction(args => MathF.Tan(args[0].Evaluate()), 1),
			["loge"] = new BuiltInFunction(args => MathF.Log(args[0].Evaluate()), 1),
			["log"] = new BuiltInFunction(args => MathF.Log(args[0].Evaluate(), args[1].Evaluate()), 2),
			["logtwo"] = new BuiltInFunction(args => MathF.Log2(args[0].Evaluate()), 1),
			["logten"] = new BuiltInFunction(args => MathF.Log10(args[0].Evaluate()), 1),
			["abs"] = new BuiltInFunction(args => MathF.Abs(args[0].Evaluate()), 1),
			["floor"] = new BuiltInFunction(args => MathF.Floor(args[0].Evaluate()), 1),
			["ceiling"] = new BuiltInFunction(args => MathF.Ceiling(args[0].Evaluate()), 1),
			["pow"] = new BuiltInFunction(args => MathF.Pow(args[0].Evaluate(), args[1].Evaluate()), 2),
			["atan"] = new BuiltInFunction(args => MathF.Atan(args[0].Evaluate()), 1),
			["atantwo"] = new BuiltInFunction(args => MathF.Atan2(args[0].Evaluate(), args[1].Evaluate()), 2),
		};

		private static readonly Dictionary<string, Ref<float>> UserVariables = [];

		internal static bool IsUserDefined(string name, out Ref<float> value)
		{
			value = null;
			if (!UserVariables.TryGetValue(name, out var result))
				return false;
			value = result;
			return true;
		}

		internal static void SetAsNewUserDefined(string name, Expression value)
		{
			if (IsUserDefined(name, out var _))
				throw new Exception($"Variable with identifier '{name}' is already defined!");

			UserVariables[name] = new(value.Evaluate());
		}
	}
}
