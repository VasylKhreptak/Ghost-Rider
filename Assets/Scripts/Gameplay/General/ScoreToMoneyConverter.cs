using UnityEngine;

public class ScoreToMoneyConverter : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private ScoreCounter _scoreCounter;
	[SerializeField] private Bank _bank;

	#region MonoBehaviour

	private void OnEnable()
	{
		_scoreCounter.onScoreReset += SaveScore;
	}

	private void OnDisable()
	{
		_scoreCounter.onScoreReset -= SaveScore;
	}

	#endregion

	private void SaveScore(int score)
	{
		_bank.AddMoney(score);
	}
}
