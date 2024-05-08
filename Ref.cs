namespace TokenizingPractice
{
	public class Ref<T>(T value) where T : struct
	{
		public T Value = value;

		public static implicit operator T(Ref<T> reference) => reference.Value;
	}
}
