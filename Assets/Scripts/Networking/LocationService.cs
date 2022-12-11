using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public static class LocationService
{
    public static async void GetLocationAsync(Action<LocationInfo> onSuccess, Action<string> onError = null)
    {
        if (Input.location.isEnabledByUser == false)
        {
            onError?.Invoke("Location service is disabled on device!");

            return;
        }

        Input.location.Start();

        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(1f));

            maxWait--;
        }

        if (maxWait <= 0)
        {
            onError?.Invoke("Location timed out!");

            return;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            onError?.Invoke("Unable to get location!");

            return;
        }

        onSuccess?.Invoke(Input.location.lastData);

        Input.location.Stop();
    }

    public static void GetGeoDataAsync(Action<GeoData> onSuccess, Action<string> onError = null)
    {
        WebRequests.Text.GetAsync(new Uri("http://ip-api.com/json"), OnGetJson, OnError);

        void OnGetJson(string json)
        {
            GeoData geoData = JsonUtility.FromJson<GeoData>(json);

            OnSuccess(geoData);
        }

        void OnSuccess(GeoData geoData)
        {
            onSuccess?.Invoke(geoData);
        }

        void OnError(string error)
        {
            onError?.Invoke(error);
        }
    }
}

[System.Serializable]
public class GeoData
{
    public string status;
    public string country;
    public string countryCode;
    public string region;
    public string regionName;
    public string city;
    public string zip;
    public double lat;
    public double lon;
    public string timezone;
    public string isp;
    public string org;
    public string @as;
    public string query;
}
