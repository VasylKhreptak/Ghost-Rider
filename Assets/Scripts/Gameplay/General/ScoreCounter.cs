using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreCounter : MonoBehaviour
{
	[Header("Preferences")]
	[SerializeField] private int _minScore;
	[SerializeField] private int _maxScore;

	private SafeInt _score;

	public int Score => _score;
	
	public Action<int> onScoreUpdated;
	public Action<int> onScoreAdded;
	public Action<int> onScoreReset;

	private int RandomScore => Random.Range(_minScore, _maxScore + 1);
	
	private void AddRandomScore()
	{
		AddScore(RandomScore);
	}

	public void AddScoreWithBonuses(int score)
	{
		///new score with bonuses
		
		AddScore(score);
	}
	
	private void AddScore(int score)
	{
		SetScore(_score + score);
		
		onScoreAdded?.Invoke(_score);
	}

	private void SetScore(int value)
	{
		_score = value;

		onScoreUpdated?.Invoke(_score);
	}
	
	public void ResetScore()
	{
		onScoreReset?.Invoke(_score);
		
		SetScore(0);
	}
}
