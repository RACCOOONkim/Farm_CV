using UnityEngine;

public class StatePaprika2 : MonoBehaviour
{
    public enum PaprikaState
    {
        Leaf1,
        Leaf2,
        Flowering,
        Growing1,
        Growing2,
        Growing3,
        Growing4,
        Ripe1,
        Ripe2
    }

    public PaprikaState currentState = PaprikaState.Leaf1;
    public GameObject leaf1Object;
    public GameObject leaf2Object;
    public GameObject floweringObject;
    public GameObject growing1Object;
    public GameObject growing2Object;
    public GameObject growing3Object;
    public GameObject growing4Object;
    public GameObject ripe1Object;
    public GameObject ripe2Object;

    private void Start()
    {
        // 초기 상태에 따라 객체를 활성화합니다.
        UpdateObjectActivation();
    }

    private void Update()
    {
        // 현재 상태에 따라 다음 상태로 변경합니다.
        switch (currentState)
        {
            case PaprikaState.Leaf1:
                if (IsLeaf2())
                    ChangeState(PaprikaState.Leaf2);
                break;

            case PaprikaState.Leaf2:
                if (IsFlowering())
                    ChangeState(PaprikaState.Flowering);
                break;

            case PaprikaState.Flowering:
                if (IsGrowing1())
                    ChangeState(PaprikaState.Growing1);
                break;

            case PaprikaState.Growing1:
                if (IsGrowing2())
                    ChangeState(PaprikaState.Growing2);
                break;

            case PaprikaState.Growing2:
                if (IsGrowing3())
                    ChangeState(PaprikaState.Growing3);
                break;

            case PaprikaState.Growing3:
                if (IsGrowing4())
                    ChangeState(PaprikaState.Growing4);
                break;

            case PaprikaState.Growing4:
                if (IsRipe1())
                    ChangeState(PaprikaState.Ripe1);
                break;

            case PaprikaState.Ripe1:
                if (IsRipe2())
                    ChangeState(PaprikaState.Ripe2);
                break;

            case PaprikaState.Ripe2:
                if (IsLeaf1())
                    ChangeState(PaprikaState.Leaf1);
                break;
        }
    }

    private void UpdateObjectActivation()
    {
        // 모든 객체를 비활성화합니다.
        leaf1Object.SetActive(false);
        leaf2Object.SetActive(false);
        floweringObject.SetActive(false);
        growing1Object.SetActive(false);
        growing2Object.SetActive(false);
        growing3Object.SetActive(false);
        growing4Object.SetActive(false);
        ripe1Object.SetActive(false);
        ripe2Object.SetActive(false);

        // 현재 상태에 따라 해당하는 객체를 활성화합니다.
        switch (currentState)
        {
            case PaprikaState.Leaf1:
                leaf1Object.SetActive(true);
                break;

            case PaprikaState.Leaf2:
                leaf2Object.SetActive(true);
                break;

            case PaprikaState.Flowering:
                floweringObject.SetActive(true);
                break;

            case PaprikaState.Growing1:
                growing1Object.SetActive(true);
                break;

            case PaprikaState.Growing2:
                growing2Object.SetActive(true);
                break;

            case PaprikaState.Growing3:
                growing3Object.SetActive(true);
                break;

            case PaprikaState.Growing4:
                growing4Object.SetActive(true);
                break;

            case PaprikaState.Ripe1:
                ripe1Object.SetActive(true);
                break;

            case PaprikaState.Ripe2:
                ripe2Object.SetActive(true);
                break;
        }
    }

    // 상태 변경을 처리하는 함수
    public void ChangeState(PaprikaState newState)
    {
        currentState = newState;
        UpdateObjectActivation();
    }

    // 해당 상태 조건이 충족되는지 확인하는 함수들입니다.
    private bool IsLeaf1() // 일사량(insolation)이 50 이하이고 토양습도(soilMoisture)가 20 미만인 경우
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.insolation <= 50f && dataEntry.soilMoisture < 20f;
    }

    private bool IsLeaf2() // 일사량(insolation)이 50 이하이고 토양습도(soilMoisture)가 20 이상인 경우
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.insolation <= 50f && dataEntry.soilMoisture >= 20f;
    }

    private bool IsFlowering() // 일사량(insolation)이 50 초과이고 100 이하이며 토양습도(soilMoisture)가 20 이상인 경우
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.insolation > 50f && dataEntry.insolation <= 100f && dataEntry.soilMoisture >= 20f;
    }

    private bool IsGrowing1() // 일사량(insolation)이 100 초과이고 200 이하이며 pH가 7.5 이상인 경우
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.insolation > 100f && dataEntry.insolation <= 200f && dataEntry.ph > 7.5f;
    }

    private bool IsGrowing2() // 일사량(insolation)이 200 초과이고 300 이하이며 pH가 7.5 이상인 경우
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.insolation > 200f && dataEntry.insolation <= 300f && dataEntry.ph > 7.5f;
    }

    private bool IsGrowing3() // 일사량(insolation)이 300 초과이고 400 이하이며 pH가 7.5 이상인 경우
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.insolation > 300f && dataEntry.insolation <= 400f && dataEntry.ph > 7.5f;
    }

    private bool IsGrowing4() // 일사량(insolation)이 400 초과이고 500 이하이며 pH가 7.5 이상인 경우
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.insolation > 400f && dataEntry.insolation <= 500f && dataEntry.ph > 7.5f;
    }

    private bool IsRipe1() // 일사량(insolation)이 500 초과이고 pH가 7.5 이상인 경우
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.insolation > 500f && dataEntry.ph > 7.5f;
    }

    private bool IsRipe2() // 일사량(insolation)이 600 초과이고 pH가 7.5 이상인 경우
    {
        DataDisplay.DataEntry dataEntry = GetComponent<DataDisplay>().dataEntries[GetComponent<DataDisplay>().currentIndex];
        return dataEntry.insolation > 600f && dataEntry.ph > 7.5f;
    }
}
