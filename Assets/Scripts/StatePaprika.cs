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
        // 초기 상태에 따라 오브젝트를 활성화
        UpdateObjectActivation();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // 10초마다 상태 변경
        if (elapsedTime >= 10f)
        {
            elapsedTime = 0f;

            // 현재 상태에 따라 다음 상태로 변경
            switch (currentState)
            {
                case PaprikaState.Green:
                    ChangeState(PaprikaState.Growing);
                    break;

                case PaprikaState.Growing:
                    ChangeState(PaprikaState.Red);
                    break;

                case PaprikaState.Red:
                    ChangeState(PaprikaState.Rotten);
                    break;

                case PaprikaState.Rotten:
                    ChangeState(PaprikaState.Green);
                    break;
            }
        }
    }

    private void UpdateObjectActivation()
    {
        // 모든 오브젝트를 비활성화
        greenObject.SetActive(false);
        growingObject.SetActive(false);
        redObject.SetActive(false);
        rottenObject.SetActive(false);

        // 현재 상태에 따라 해당하는 오브젝트를 활성화
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

    // 상태 변경을 처리하는 함수입
    public void ChangeState(PaprikaState newState)
    {
        currentState = newState;
        UpdateObjectActivation();
    }
}
