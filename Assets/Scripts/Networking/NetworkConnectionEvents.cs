using System;
using UnityEngine;

public class NetworkConnectionEvents : MonoBehaviour
{
	[Header("Preferences")]
	[SerializeField] private float _checkDelay = 1;

	private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();

	private bool _isConnected;

	public bool IsConnected => _isConnected;
	
	public Action onConnected;
	public Action onDisconnected;

	#region MonoBehaviour

	private void Awake()
	{
		_configurableUpdate.Init(this, _checkDelay, CheckConnection);
	}

	private void OnEnable()
	{
		_configurableUpdate.StartUpdating();
	}

	private void OnDisable()
	{
		_configurableUpdate.StopUpdating();	
	}

	#endregion

	private void CheckConnection()
	{
		NetworkConnection.CheckAsync(NetworkConnectionCallback);

		void NetworkConnectionCallback(bool isConnected)
		{
			if (_isConnected == false && isConnected)
			{
				onConnected?.Invoke();
			}
			else if(_isConnected && isConnected == false)
			{
				onDisconnected?.Invoke();
			}

			_isConnected = isConnected;
		}
	}
}
