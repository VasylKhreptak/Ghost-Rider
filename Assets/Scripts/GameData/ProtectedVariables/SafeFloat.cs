public struct SafeFloat
{
	private int value;
	private int salt;

	public SafeFloat(float value)
	{
		System.Random random = new System.Random();
		salt = random.Next(int.MinValue/4, int.MaxValue/4);
		int intValue = System.BitConverter.ToInt32(System.BitConverter.GetBytes(value), 0);
		this.value = intValue ^ salt;
	}

	public override bool Equals(object obj)
	{
		return (float)this == (float)obj;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override string ToString()
	{
		return ((float)this).ToString();
	}

	public string ToString(string format)
	{
		return ((int)this).ToString(format);
	}
	
	public static implicit operator float(SafeFloat safeFloat)
	{
		return System.BitConverter.ToSingle(System.BitConverter.GetBytes(safeFloat.salt ^ safeFloat.value), 0);
	}

	public static implicit operator SafeFloat(float normalFloat)
	{
		return new SafeFloat(normalFloat);
	}
}
