using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataMangerSoilEC : MonoBehaviour
{
    public TextMeshProUGUI soilECTextUI; // 토양 EC 값을 표시할 TextMeshPro 오브젝트에 연결
    public DataDisplay dataDisplay; // DataDisplay 스크립트에 연결
    public Image progressBar; // 진행 바 이미지에 연결

    private float minSoilEC = 0f; // 최소 토양 EC 값 설정
    private float maxSoilEC = 5f; // 최대 토양 EC 값 설정

    private float updateInterval = 5f; // 데이터 업데이트 간격 (1초)
    private float timer = 0f;

    void Start()
    {
        dataDisplay.LoadDataFromCSV(); // 데이터를 .csv 파일에서 불러옵니다
        UpdateSoilECText(); // 토양 EC 텍스트 업데이트
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            UpdateSoilECText();
            UpdateProgressBar();
            timer = 0f;
        }
    }

    void UpdateSoilECText()
    {
        // 데이터 표시 스크립트로부터 토양 EC 데이터를 가져옵니다.
        if (dataDisplay.GetCurrentIndex() < dataDisplay.dataEntries.Count)
        {
            DataDisplay.DataEntry entry = dataDisplay.dataEntries[dataDisplay.GetCurrentIndex()];

            // TextMeshPro 텍스트 UI에 토양 EC 값을 표시합니다.
            soilECTextUI.text = $"{entry.soilEC} dS/m";
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

            // 토양 EC 값이 원하는 범위로 매핑하여 progressBar.fillAmount에 할당합니다.
            float normalizedSoilEC = Mathf.InverseLerp(minSoilEC, maxSoilEC, entry.soilEC);
            progressBar.fillAmount = normalizedSoilEC;
        }
    }
}
