using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;
using System;

public class DataDisplay : MonoBehaviour
{
    public TextAsset csvData; // .csv 데이터 참조
    public TMP_Text dataText; // 데이터 표시에 사용할 TextMeshPro 텍스트 UI 요소

    public List<DataEntry> dataEntries; // 데이터 항목의 목록
    public int currentIndex; // 현재 인덱스
    private float timer; // 타이머

    public void Start()
    {
        LoadDataFromCSV(); // .csv 데이터 불러오기
        currentIndex = 0; // 초기 인덱스 설정
        timer = 0f; // 타이머 초기화
    }

    public void Update()
    {
        timer += Time.deltaTime; // 타이머 업데이트

        if (timer >= 10f) // 5초마다 실행
        {
            timer = 0f; // 타이머 재설정
            DisplayNextData(); // 다음 데이터 표시
        }
    }

    public void LoadDataFromCSV()
    {
        dataEntries = new List<DataEntry>(); // 데이터 항목 목록 초기화

        string[] csvLines = csvData.text.Split('\n'); // .csv 데이터의 모든 줄 분할

        for (int i = 1; i < csvLines.Length; i++) // 첫 번째 줄은 헤더이므로 무시하고 두 번째 줄부터 시작
        {
            string[] values = csvLines[i].Split(','); // 쉼표로 구분된 값 분할

            if (values.Length >= 11) // 데이터가 유효한지 확인
            {
                // 데이터 항목 생성 및 값 설정
                DataEntry entry = new DataEntry();

                // 날짜와 시간을 별도로 파싱
                string dateString = values[0];
                string timeString = values[1];
                string[] dateParts = dateString.Split('-');
                string[] timeParts = timeString.Split(':');

                if (dateParts.Length >= 3 && timeParts.Length >= 2)
                {
                    // 년, 월, 일을 int로 파싱
                    int year, month, day;
                    if (int.TryParse(dateParts[0], out year) &&
                        int.TryParse(dateParts[1], out month) &&
                        int.TryParse(dateParts[2], out day))
                    {
                        // 날짜를 YYYYMMDD 형식으로 설정
                        entry.date = year * 10000 + month * 100 + day;
                    }
                    else
                    {
                        Debug.Log($"날짜 형식이 올바르지 않습니다: {dateString}");
                        continue; // 다음 데이터로 이동
                    }

                    // 시간을 int로 파싱
                    int hour, minute;
                    if (int.TryParse(timeParts[0], out hour) &&
                        int.TryParse(timeParts[1], out minute))
                    {
                        // 시간을 HHmm 형식으로 설정
                        entry.time = hour * 100 + minute;
                    }
                    else
                    {
                        Debug.Log($"시간 형식이 올바르지 않습니다: {timeString}");
                        continue; // 다음 데이터로 이동
                    }
                }
                else
                {
                    Debug.Log($"올바른 날짜 및 시간 형식이 아닙니다: {dateString}, {timeString}");
                    continue; // 다음 데이터로 이동
                }

                // 나머지 값들을 float로 파싱
                if (float.TryParse(values[2], out entry.temperature) &&
                    float.TryParse(values[3], out entry.humidity) &&
                    float.TryParse(values[4], out entry.co2) &&
                    float.TryParse(values[5], out entry.soilTemperature) &&
                    float.TryParse(values[6], out entry.soilEC) &&
                    float.TryParse(values[7], out entry.soilMoisture) &&
                    float.TryParse(values[8], out entry.insolation) &&
                    float.TryParse(values[9], out entry.ec) &&
                    float.TryParse(values[10], out entry.ph))
                {
                    // 데이터 항목을 목록에 추가
                    dataEntries.Add(entry);
                }
                else
                {
                    Debug.Log($"데이터 값 형식이 올바르지 않습니다: {csvLines[i]}");
                }
            }
            else
            {
                Debug.Log($"데이터 형식이 올바르지 않습니다: {csvLines[i]}");
            }
        }
    }

    public void DisplayNextData()
    {
        if (currentIndex < dataEntries.Count) // 현재 인덱스가 데이터 항목 수보다 작을 경우
        {
            // 현재 인덱스의 데이터 항목 가져오기
            DataEntry entry = dataEntries[currentIndex];
            currentIndex++; // 인덱스 증가

            // 데이터를 UI에 표시
            string date = $"{entry.date / 10000}-{entry.date % 10000 / 100}-{entry.date % 100:00}";
            string time = $"{entry.time / 100:00}:{entry.time % 100:00}";
            string displayText = $"Date: {entry.date}, Time: {entry.time}\n Temperature: {entry.temperature}Celsius, Humid: {entry.humidity}%\n CO2: {entry.co2}ppm, SoilTemperature: {entry.soilTemperature}Celsius\n Soil EC: {entry.soilEC}dS/m, Soil Humid: {entry.soilMoisture}%\n Isloation: {entry.insolation}w/m2, EC: {entry.ec}dS/m\n pH: {entry.ph}pH";
            dataText.text = displayText;
        }
        else
        {
            Debug.Log("더 이상 표시할 데이터가 없습니다.");
        }
    }

    public class DataEntry
    {
        public int date;
        public int time;
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
    public int GetCurrentIndex()
    {
        return currentIndex;
    }


}
