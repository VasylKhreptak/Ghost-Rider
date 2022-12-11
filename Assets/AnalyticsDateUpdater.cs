using System;
using System.Globalization;

public class AnalyticsDateUpdater : AnalyticsVariableUpdater
{
    protected override void UpdateVariable()
    {
        _variableReference.SetValueAsync(DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
    }
}
