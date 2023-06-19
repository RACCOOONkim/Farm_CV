using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.IO;

public class DataDisplay : MonoBehaviour
{
    public TextAsset csvData; // .csv 데이터 참조
    public TMP_Text dataText; // 데이터 표시를 위한 TextMeshPro Text UI 요소

    private List<DataEntry> dataEntries; // 데이터 엔트리 목록
    private int currentIndex; // 현재 인덱스
    private float timer; // 타이머

    private void Start()
    {
        LoadDataFromCSV(); // .csv 데이터 로드
        currentIndex = 0; // 초기 인덱스 설정
        timer = 0f; // 타이머 초기화
    }

    private void Update()
    {
        timer += Time.deltaTime; // 타이머 갱신

        if (timer >= 5f) // 5초마다 실행
        {
            timer = 0f; // 타이머 초기화
            DisplayNextData(); // 다음 데이터 표시
            Debug.Log(currentIndex);
            Debug.Log(dataEntries.Count);
        }
    }

    private void LoadDataFromCSV()
    {
        dataEntries = new List<DataEntry>(); // 데이터 엔트리 목록 초기화

        string[] csvLines = csvData.text.Split('\n'); // .csv 데이터의 모든 줄 분리

        for (int i = 1; i < csvLines.Length; i++) // 첫 번째 줄은 헤더이므로 무시하고 두 번째 줄부터 시작
        {
            string[] values = csvLines[i].Split(','); // 쉼표로 구분된 값 분리
            Debug.Log("data is separated");
            if (values.Length >= 10) // 유효한 데이터인지 확인
            {
                // 데이터 엔트리 생성 및 값 설정
                DataEntry entry = new DataEntry();
                entry.dateTime = DateTime.ParseExact(values[0], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                entry.temperature = float.Parse(values[1]);
                entry.humidity = float.Parse(values[2]);
                entry.co2 = float.Parse(values[3]);
                entry.soilTemperature = float.Parse(values[4]);
                entry.soilEC = float.Parse(values[5]);
                entry.soilMoisture = float.Parse(values[6]);
                entry.insolation = float.Parse(values[7]);
                entry.ec = float.Parse(values[8]);
                entry.ph = float.Parse(values[9]);
                Debug.Log(values[0]);
                dataEntries.Add(entry); // 데이터 엔트리 목록에 추가
                Debug.Log("Data Loaded.");
            }
        }
    }

    private void DisplayNextData()
    {
        if (currentIndex < dataEntries.Count)
        {
            DataEntry entry = dataEntries[currentIndex]; // 현재 인덱스의 데이터 엔트리 가져오기
            currentIndex++; // 인덱스 증가

            // 데이터 정보를 텍스트로 구성
            string dataInfo = $"Date: {entry.dateTime.ToString("yyyy-MM-dd HH:mm")}\n" +
                              $"Temperature: {entry.temperature}°C\n" +
                              $"Humidity: {entry.humidity}%\n" +
                              $"CO2: {entry.co2}ppm\n" +
                              $"Soil Temperature: {entry.soilTemperature}°C\n" +
                              $"Soil EC: {entry.soilEC}dS/m\n" +
                              $"Soil Moisture: {entry.soilMoisture}%\n" +
                              $"Insolation: {entry.insolation}w/m2\n" +
                              $"EC: {entry.ec}dS/m\n" +
                              $"pH: {entry.ph}";

            dataText.text = dataInfo; // 텍스트 업데이트
            Debug.Log("Data exist.");
        }
        else
        {
            Debug.Log("All data displayed."); // 모든 데이터 표시 완료
        }
    }

    [Serializable]
    public class DataEntry
    {
        public DateTime dateTime;
        public float temperature;
        public float humidity;
        public float co2;
        public float soilTemperature;
        public float soilEC;
        public float soilMoisture;
        public float insolation;
        public float ec;
        public float ph;
    }
}
