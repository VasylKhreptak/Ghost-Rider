using System;
using System.Diagnostics;
using Firebase.Database;
using Firebase.Extensions;

public class AnalyticsTimeSpan : AnalyticsVariableUpdater
{
    private Stopwatch _stopwatch;

    protected override void OnEnable()
    {
        base.OnEnable();
        
        _stopwatch = Stopwatch.StartNew();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        _stopwatch.Stop();
    }

    protected override void UpdateVariable()
    {
        _variableReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                if (task.Result.Exists)
                {
                    UpdatePlayTime(task.Result);
                }
                else
                {
                    SetDefaultValue();
                }
                
                _stopwatch.Restart();
            }
        });
    }

    private void UpdatePlayTime(DataSnapshot dataSnapshot)
    {
        string data = dataSnapshot.Value.ToString();
        TimeSpan playTime = TimeSpan.Parse(data);

        _variableReference.SetValueAsync((playTime + _stopwatch.Elapsed).ToString());
    }

    private void SetDefaultValue()
    {
        _variableReference.SetValueAsync(_stopwatch.Elapsed.ToString());
    }
}
