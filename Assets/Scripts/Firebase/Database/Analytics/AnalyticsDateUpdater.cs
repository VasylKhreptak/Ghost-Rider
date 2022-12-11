using System;
using System.Globalization;

public class AnalyticsDateUpdater : AnalyticsVariableUpdater
{
    protected override void UpdateVariable()
    {
        _variableReference.SetValueAsync(DateTime.Now.ToString(CultureInfo.InvariantCulture));
    }
}
