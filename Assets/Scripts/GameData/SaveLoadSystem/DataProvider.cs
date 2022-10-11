using System;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
	#region MonoBehaviour

	protected virtual void Awake()
	{
		Load();
	}

	protected virtual void OnApplicationQuit()
	{
		Save();
	}


	protected virtual void OnApplicationPause(bool pauseStatus)
	{
		Save();
	}

	#endregion

	protected virtual void Save()
	{
		throw new NotImplementedException();
	}

	protected virtual void Load()
	{
		throw new NotImplementedException();
	}
}
