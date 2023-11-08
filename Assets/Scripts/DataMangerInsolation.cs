using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataMangerInsolation : MonoBehaviour
{
    public TextMeshProUGUI insolationTextUI; // 일사량을 표시할 TextMeshPro 오브젝트에 연결
    public DataDisplay dataDisplay; // DataDisplay 스크립트에 연결
    public Image progressBar; // 진행 바 이미지에 연결

    private float minInsolation = 0f; // 최소 일사량 값 설정
    private float maxInsolation = 1000f; // 최대 일사량 값 설정

    private float updateInterval = 5f; // 데이터 업데이트 간격 (1초)
    private float timer = 0f;

    void Start()
    {
        dataDisplay.LoadDataFromCSV(); // 데이터를 .csv 파일에서 불러옵니다
        UpdateInsolationText(); // 일사량 텍스트 업데이트
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            UpdateInsolationText();
            UpdateProgressBar();
            timer = 0f;
        }
    }

    void UpdateInsolationText()
    {
        // 데이터 표시 스크립트로부터 일사량 데이터를 가져옵니다.
        if (dataDisplay.GetCurrentIndex() < dataDisplay.dataEntries.Count)
        {
            DataDisplay.DataEntry entry = dataDisplay.dataEntries[dataDisplay.GetCurrentIndex()];

            // TextMeshPro 텍스트 UI에 일사량 값을 표시합니다.
            insolationTextUI.text = $"{entry.insolation} w/m²";
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

            // 일사량 값이 원하는 범위로 매핑하여 progressBar.fillAmount에 할당합니다.
            float normalizedInsolation = Mathf.InverseLerp(minInsolation, maxInsolation, entry.insolation);
            progressBar.fillAmount = normalizedInsolation;
        }
    }
}
