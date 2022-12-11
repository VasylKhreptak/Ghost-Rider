using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class AnalyticsVariableIncrementer : AnalyticsVariableUpdater
{
    [Header("Preferences")]
    [SerializeField] private int _defaultValue = 1;
    
    protected override void UpdateVariable()
    {
        _variableReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                if (task.Result.Exists)
                {
                    IncrementValue(task.Result);
                }
                else
                {
                    SetDefaultValue();
                }
            }
        });
    }

    private void IncrementValue(DataSnapshot dataSnapshot)
    {
        int value = int.Parse(dataSnapshot.Value.ToString());

        _variableReference.SetValueAsync(value + 1);
    }

    private void SetDefaultValue()
    {
        _variableReference.SetValueAsync(_defaultValue);
    }
}
