using UnityEngine;

public class AnalyticsLocation : AnalyticsVariableUpdater
{
    protected override void UpdateVariable()
    {
        LocationService.GetGeoDataAsync(OnSuccess, OnError);
        
        void OnSuccess(GeoData geoData)
        {
            _variableReference.SetRawJsonValueAsync(JsonUtility.ToJson(geoData));
        }
        
        void OnError(string error)
        {
            Debug.Log(error);
        }
    }
}
