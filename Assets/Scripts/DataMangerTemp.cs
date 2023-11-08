using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataMangerTemp : MonoBehaviour
{
    public TextMeshProUGUI temperatureTextUI; // TextMeshPro 오브젝트에 연결
    public DataDisplay dataDisplay; // DataDisplay 스크립트에 연결
    public Image progressBar; // 진행 바 이미지에 연결
    private float updateInterval = 5f; // 데이터 업데이트 간격 (10초)
    private float timer = 0f;

    void Start()
    {
        dataDisplay.LoadDataFromCSV(); // 데이터를 .csv 파일에서 불러옵니다
        UpdateTemperatureText(); // 온도 텍스트 업데이트
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            UpdateTemperatureText();
            UpdateProgressBar();
            timer = 0f;
        }
    }

    void UpdateTemperatureText()
    {
        // 데이터 표시 스크립트로부터 온도 데이터를 가져옵니다.
        if (dataDisplay.GetCurrentIndex() < dataDisplay.dataEntries.Count)
        {
            DataDisplay.DataEntry entry = dataDisplay.dataEntries[dataDisplay.GetCurrentIndex()];

            // TextMeshPro 텍스트 UI에 온도 값을 표시합니다.
            temperatureTextUI.text = $"{entry.temperature}°C";
        }
        else
        {
            Debug.Log("더 이상 표시할 데이터가 없습니다.");
        }

        dataDisplay.DisplayNextData();
    }

    void UpdateProgressBar()
    {
        // 진행 바를 업데이트합니다.
        if (dataDisplay.GetCurrentIndex() < dataDisplay.dataEntries.Count)
        {
            DataDisplay.DataEntry entry = dataDisplay.dataEntries[dataDisplay.GetCurrentIndex()];

            // Temperature 값이 0에서 50 사이로 매핑하여 progressBar.fillAmount에 할당합니다.
            float normalizedTemperature = Mathf.InverseLerp(0f, 50f, entry.temperature);
            progressBar.fillAmount = Mathf.Lerp(0f, 1f, normalizedTemperature);
        }
    }
}