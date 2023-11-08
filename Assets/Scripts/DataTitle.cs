using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataTitle : MonoBehaviour
{
    public TextMeshProUGUI title; 
    public DataDisplay dataDisplay; // DataDisplay 스크립트에 연결

    private float updateInterval = 5f; // 데이터 업데이트 간격 (1초)
    private float timer = 0f;

    void Start()
    {
        dataDisplay.LoadDataFromCSV(); // 데이터를 .csv 파일에서 불러옵니다
        UpdateTitleText(); // 텍스트 업데이트
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            UpdateTitleText();
            timer = 0f;
        }
    }

    void UpdateTitleText()
    {
        // 데이터 표시 스크립트로부터 Title 데이터를 가져옵니다.
        if (dataDisplay.GetCurrentIndex() < dataDisplay.dataEntries.Count)
        {
            DataDisplay.DataEntry entry = dataDisplay.dataEntries[dataDisplay.GetCurrentIndex()];

            // TextMeshPro 텍스트 UI에 pH 값을 표시합니다.
            title.text = $"Date : {entry.date} Time : {entry.time}";
        }
        else
        {
            Debug.Log("더 이상 표시할 데이터가 없습니다.");
        }

        dataDisplay.DisplayNextData();
    }

}
