using System;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
	[Header("Preferences")]
	[SerializeField] protected string _playerPrefsKey = "PlayerData";
	
	#region MonoBehaviour

	private void Awake()
	{
		Load();
	}

	private void OnApplicationQuit()
	{
		Save();
	}


	private void OnApplicationPause(bool pauseStatus)
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
