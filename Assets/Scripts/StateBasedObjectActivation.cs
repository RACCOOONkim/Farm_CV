using UnityEngine;

public class StateBasedObjectActivation : MonoBehaviour
{
    public enum TomatoState
    {
        Seedling,
        Growing,
        Withered
    }

    public TomatoState currentState = TomatoState.Seedling;
    public GameObject seedlingObject;
    public GameObject growingObject;
    public GameObject witheredObject;

    private float elapsedTime;

    private void Start()
    {
        // 초기 상태에 따라 오브젝트를 활성화합니다.
        UpdateObjectActivation();
    }

    private void Update()
    {
        // 시간을 측정하여 10초마다 상태를 변경합니다.
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 10f)
        {
            elapsedTime = 0f;

            // 현재 상태에 따라 다음 상태로 변경합니다.
            switch (currentState)
            {
                case TomatoState.Seedling:
                    ChangeState(TomatoState.Growing);
                    break;

                case TomatoState.Growing:
                    ChangeState(TomatoState.Withered);
                    break;

                case TomatoState.Withered:
                    ChangeState(TomatoState.Seedling);
                    break;
            }
        }
    }

    private void UpdateObjectActivation()
    {
        // 모든 오브젝트를 비활성화합니다.
        seedlingObject.SetActive(false);
        growingObject.SetActive(false);
        witheredObject.SetActive(false);

        // 현재 상태에 따라 해당하는 오브젝트를 활성화합니다.
        switch (currentState)
        {
            case TomatoState.Seedling:
                seedlingObject.SetActive(true);
                break;

            case TomatoState.Growing:
                growingObject.SetActive(true);
                break;

            case TomatoState.Withered:
                witheredObject.SetActive(true);
                break;
        }
    }

    // 상태 변경을 처리하는 함수
    public void ChangeState(TomatoState newState)
    {
        currentState = newState;
        UpdateObjectActivation();
    }
}
