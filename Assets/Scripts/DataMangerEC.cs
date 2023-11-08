using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataMangerEC : MonoBehaviour
{
    public TextMeshProUGUI ecTextUI; // EC (전기전도도)를 표시할 TextMeshPro 오브젝트에 연결
    public DataDisplay dataDisplay; // DataDisplay 스크립트에 연결
    public Image progressBar; // 진행 바 이미지에 연결

    private float minEC = 0f; // 최소 EC 값 설정
    private float maxEC = 10f; // 최대 EC 값 설정

    private float updateInterval = 5f; // 데이터 업데이트 간격 (1초)
    private float timer = 0f;

    void Start()
    {
        dataDisplay.LoadDataFromCSV(); // 데이터를 .csv 파일에서 불러옵니다
        UpdateECText(); // EC 텍스트 업데이트
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            UpdateECText();
            UpdateProgressBar();
            timer = 0f;
        }
    }

    void UpdateECText()
    {
        // 데이터 표시 스크립트로부터 EC 데이터를 가져옵니다.
        if (dataDisplay.GetCurrentIndex() < dataDisplay.dataEntries.Count)
        {
            DataDisplay.DataEntry entry = dataDisplay.dataEntries[dataDisplay.GetCurrentIndex()];

            // TextMeshPro 텍스트 UI에 EC 값을 표시합니다.
            ecTextUI.text = $"{entry.ec} dS/m";
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

            // EC 값이 원하는 범위로 매핑하여 progressBar.fillAmount에 할당합니다.
            float normalizedEC = Mathf.InverseLerp(minEC, maxEC, entry.ec);
            progressBar.fillAmount = normalizedEC;
        }
    }
}
