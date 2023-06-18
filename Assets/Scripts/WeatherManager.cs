using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class WeatherManager : MonoBehaviour
{
    public string weatherAPIURL = "http://api.openweathermap.org/data/2.5/weather";
    public string city = "Gongneung-dong, Seoul";
    public string apiKey = "c24241bbe061c9364bd71f448ae2cb80";
    public TMP_Text temperatureText; // Canvas의 TextMeshPro Text UI 요소

    private float requestInterval = 60f; // 요청 간격 (초 단위)
    private float lastRequestTime; // 마지막 요청 시간

    private void Start()
    {
        lastRequestTime = -requestInterval; // 시작 시간 초기화
    }

    private void Update()
    {
        // 요청 간격을 충족하는지 확인
        if (Time.time - lastRequestTime >= requestInterval)
        {
            StartCoroutine(GetWeatherData());
            lastRequestTime = Time.time; // 마지막 요청 시간 갱신
        }
    }

    private IEnumerator GetWeatherData()
    {
        string url = $"{weatherAPIURL}?q={city}&appid={apiKey}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to retrieve weather data: " + webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                ProcessWeatherData(json);
            }
        }
    }

    private void ProcessWeatherData(string json)
    {
        WeatherData weatherData = JsonUtility.FromJson<WeatherData>(json);

        if (weatherData != null)
        {
            float temperatureKelvin = weatherData.main.temp;
            float temperatureCelsius = temperatureKelvin - 273.15f;
            temperatureText.text = "Current temperature in " + city + ": " + temperatureCelsius.ToString("0.00") + "°C";
        }
        else
        {
            Debug.LogError("Failed to parse weather data.");
        }
    }
}

[System.Serializable]
public class WeatherData
{
    public MainData main;
}

[System.Serializable]
public class MainData
{
    public float temp;
}
