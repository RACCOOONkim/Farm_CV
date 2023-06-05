using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class TomatoObject : MonoBehaviour
{
    public enum TomatoState
    {
        Seedling,
        Growing,
        Withered
    }

    private TomatoState currentState;
    private float elapsedTime;
    private ToolTipSpawner  toolTipSpawner;

    private void Start()
    {
        currentState = TomatoState.Seedling;
        elapsedTime = 0f;

        // ToolTipSpawner 컴포넌트를 찾습니다.
        toolTipSpawner = GetComponent<ToolTipSpawner>();
        if (toolTipSpawner == null)
        {
            Debug.LogError("ToolTipSpawner 컴포넌트를 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // 시간에 따라 상태 변경
        if (elapsedTime >= 10f && currentState == TomatoState.Seedling)
        {
            ChangeState(TomatoState.Growing);
        }
        else if (elapsedTime >= 20f && currentState == TomatoState.Growing)
        {
            ChangeState(TomatoState.Withered);
        }
        Debug.Log(currentState);
    }

    public void ChangeState(TomatoState newState)
{
    currentState = newState;

    // 상태에 따라 ToolTip 정보 설정
    switch (currentState)
    {
        case TomatoState.Seedling:
            // 새싹 상태의 ToolTip 정보 설정
            SetToolTipInfo("Seedling");
            break;

        case TomatoState.Growing:
            // 성장중 상태의 ToolTip 정보 설정
            SetToolTipInfo("Growing");
            break;

        case TomatoState.Withered:
            // 시든 상태의 ToolTip 정보 설정
            SetToolTipInfo("Withered");
            break;
    }
}

private void SetToolTipInfo(string info)
{
    // ToolTipSpawner를 사용하여 ToolTip의 Text 설정
    if (toolTipSpawner != null)
    {
        ToolTip toolTip = toolTipSpawner.GetComponent<ToolTip>();
        if (toolTip != null)
        {
            // 토마토의 상태에 따라 ToolTip의 Text 설정
            toolTip.ToolTipText = info;
            Debug.Log("ToolTip의 Text를 설정했습니다: " + info);
        }
    }
}
}
