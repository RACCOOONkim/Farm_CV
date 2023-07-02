using UnityEngine;

public class StatePaprika : MonoBehaviour
{
    public enum PaprikaState
    {
        Green,
        Growing,
        Red,
        Rotten
    }

    public PaprikaState currentState = PaprikaState.Green;
    public GameObject greenObject;
    public GameObject growingObject;
    public GameObject redObject;
    public GameObject rottenObject;

    private float elapsedTime;

    private void Start()
    {
        // 초기 상태에 따라 객체 활성화
        UpdateObjectActivation();
    }

    private void Update()
    {
        // 현재 상태에 따라 다음 상태로 변경
        switch (currentState)
        {
            case PaprikaState.Green:
                if (IsRotten())
                    ChangeState(PaprikaState.Rotten);
                else if (IsGrowing())
                    ChangeState(PaprikaState.Growing);
                break;

            case PaprikaState.Growing:
                if (IsRotten())
                    ChangeState(PaprikaState.Rotten);
                else if (IsRed())
                    ChangeState(PaprikaState.Red);
                break;

            case PaprikaState.Red:
                if (IsRotten())
                    ChangeState(PaprikaState.Rotten);
                else if (IsGreen())
                    ChangeState(PaprikaState.Green);
                break;

            case PaprikaState.Rotten:
                if (IsGreen())
                    ChangeState(PaprikaState.Green);
                break;
        }
    }

    private void UpdateObjectActivation()
    {
        // 모든 객체 비활성화
        greenObject.SetActive(false);
        growingObject.SetActive(false);
        redObject.SetActive(false);
        rottenObject.SetActive(false);

        // 현재 상태에 따라 해당하는 객체 활성화
        switch (currentState)
        {
            case PaprikaState.Green:
                greenObject.SetActive(true);
                break;

            case PaprikaState.Growing:
                growingObject.SetActive(true);
                break;

            case PaprikaState.Red:
                redObject.SetActive(true);
                break;

            case PaprikaState.Rotten:
                rottenObject.SetActive(true);
                break;
        }
    }

    // 상태 변경 처리 함수
    public void ChangeState(PaprikaState newState)
    {
        currentState = newState;
        UpdateObjectActivation();
    }

    // 해당 상태 조건 충족 여부 확인
    private bool IsGreen()/*습도(Humid)가 80% 이상이고 전기 전도도(EC)가 4 dS/m 이상인 경우: 초록색(green)
                            온도(Temperature)가 25°C 이상이고 습도(Humid)가 70% 이상인 경우: 초록색(green)*/
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.temperature > 25f && dataEntry.humidity > 70f;
    }

    private bool IsGrowing() /*그 외의 경우: 성장 중(growing) = default*/
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return !IsGreen() && !IsRed() && !IsRotten();
    }

    private bool IsRed()/*이산화탄소 농도(CO2)가 500 ppm 이상인 경우: 빨간색(red)*/
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.co2 > 500f;
    }

    private bool IsRotten() /*토양 온도(SoilTemperature)가 20°C 미만이거나 30°C 이상인 경우: 썩은(rotten),
                                토양 습도(SoilMoisture)가 10% 미만인 경우: 썩은(rotten)*/
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.soilTemperature < 20f || dataEntry.soilTemperature > 30f || dataEntry.soilMoisture < 10f;
    }
}
