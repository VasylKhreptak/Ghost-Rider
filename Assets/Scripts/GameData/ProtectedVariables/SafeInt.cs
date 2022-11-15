[System.Serializable]
public struct SafeInt
{
	public int value;
	public int salt;

	public SafeInt(int value)
	{
		System.Random random = new System.Random();
		salt = random.Next(int.MinValue/4, int.MaxValue/4);
		this.value = value ^ salt;
	}

	public override bool Equals(object obj)
	{
		return (int)this == (int)obj;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override string ToString()
	{
		return ((int)this).ToString();
	}

	public string ToString(string format)
	{
		return ((int)this).ToString(format);
	}
	
	public static implicit operator int(SafeInt safeInt)
	{
		return safeInt.value ^ safeInt.salt;
	}

	public static implicit operator SafeInt(int normalInt)
	{
		return new SafeInt(normalInt);
	}
}
