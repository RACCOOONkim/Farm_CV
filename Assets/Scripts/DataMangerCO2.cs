using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataMangerCO2 : MonoBehaviour
{
    public TextMeshProUGUI co2TextUI; // CO2를 표시할 TextMeshPro 오브젝트에 연결
    public DataDisplay dataDisplay; // DataDisplay 스크립트에 연결
    public Image progressBar; // 진행 바 이미지에 연결

    private float minCO2 = 0f; // 최소 CO2 값 설정
    private float maxCO2 = 1000f; // 최대 CO2 값 설정

    public float updateInterval = 5f; // 
    private float timer = 0f;

    void Start()
    {
        dataDisplay.LoadDataFromCSV(); // 데이터를 .csv 파일에서 불러옵니다
        UpdateCO2Text(); // CO2 텍스트 업데이트
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= dataDisplay.intervalTime)
        {
            
            UpdateCO2Text();
            UpdateProgressBar();
            timer = 0f;
        }
    }

    void UpdateCO2Text()
    {
        // 데이터 표시 스크립트로부터 CO2 데이터를 가져옵니다.
        if (dataDisplay.GetCurrentIndex() < dataDisplay.dataEntries.Count)
        {
            DataDisplay.DataEntry entry = dataDisplay.dataEntries[dataDisplay.GetCurrentIndex()];

            // TextMeshPro 텍스트 UI에 CO2 값을 표시합니다.
            co2TextUI.text = $"{(int)entry.co2} ppm";
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

            // CO2 값이 원하는 범위로 매핑하여 progressBar.fillAmount에 할당합니다.
            float normalizedCO2 = Mathf.InverseLerp(minCO2, maxCO2, entry.co2); // minCO2와 maxCO2를 적절한 값으로 대체해야 합니다.
            progressBar.fillAmount = normalizedCO2;
        }
    }
}
