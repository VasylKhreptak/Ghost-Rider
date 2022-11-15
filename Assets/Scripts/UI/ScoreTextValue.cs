using TMPro;
using UnityEngine;

public class ScoreTextValue : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_Text _tmp;
	[SerializeField] private ScoreCounter _scoreCounter;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_tmp ??= GetComponent<TMP_Text>();
	}

	private void OnEnable()
	{
		_scoreCounter.onScoreUpdated += UpdateText;
	}

	private void OnDisable()
	{
		_scoreCounter.onScoreUpdated -= UpdateText;
	}

	#endregion

	private void UpdateText(int score)
	{
		_tmp.text = score.ToString();
	}
}
