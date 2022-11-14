using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[Header("Preferences")]
	[SerializeField] private int _minScore;
	[SerializeField] private int _maxScore;
	
	[Header("Events")]
	[SerializeField] private MonoEvent _addScoreEvent;

	private int Score => Random.Range(_minScore, _maxScore + 1);
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_addScoreEvent ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_addScoreEvent.onMonoCall += AddScore;
	}

	private void OnDisable()
	{
		_addScoreEvent.onMonoCall -= AddScore;
	}

	#endregion

	private void AddScore()
	{
		Debug.Log("Add Score");
	}
}
