public class AnalyticsIP : AnalyticsVariableUpdater
{
    protected override void UpdateVariable()
    {
        NetworkConnection.GetPublicIP(OnGetIP);
        
        void OnGetIP(string ip)
        {
            _variableReference.SetValueAsync(ip);
        }
    }
}
