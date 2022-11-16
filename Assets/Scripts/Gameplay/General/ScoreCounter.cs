using System;
using DG.Tweening;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
	[Header("Bonus Preferences")]
	[SerializeField] private float[] _bonusCoefficients;
	[SerializeField] private float _cooldown;

	private SafeInt _score;

	private int _currentBonusIndex = 0;

	private Tween _waitTween;
	
	public int Score => _score;
	
	public Action<int> onScoreUpdated;
	public Action<int> onScoreAdded;
	public Action<int> onScoreReset;
	
	#region MonoBehaviour

	private void OnDisable()
	{
		_waitTween.Kill();
	}

	#endregion

	public void AddScoreWithBonuses(int score)
	{
		score = (int)(score * GetBonusCoefficient());
		
		AddScore(score);

		ProcessBonusSelection();
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

	private float GetBonusCoefficient()
	{
		if (_currentBonusIndex > _bonusCoefficients.Length - 1)
		{
			return _bonusCoefficients[_bonusCoefficients.Length - 1];
		}

		return _bonusCoefficients[_currentBonusIndex];
	}

	private void ProcessBonusSelection()
	{
		_currentBonusIndex++;

		_waitTween.Kill();
		_waitTween = this
			.DOWait(_cooldown)
			.OnComplete(() => _currentBonusIndex = 0);
	}
}
