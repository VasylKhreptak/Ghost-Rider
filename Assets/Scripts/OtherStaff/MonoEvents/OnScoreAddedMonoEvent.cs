using System;
using UnityEngine;

public class OnScoreAddedMonoEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private ScoreCounter _scoreCounter;

	#region MonoBehaviour

	private void OnEnable()
	{
		_scoreCounter.onScoreAdded += Invoke;
	}

	private void OnDisable()
	{
		_scoreCounter.onScoreAdded -= Invoke;
	}

	#endregion

	private void Invoke(int score) => Invoke();
}
