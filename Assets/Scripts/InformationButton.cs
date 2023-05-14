using UnityEngine;
using UnityEngine.UI;

public class InformationButton : MonoBehaviour
{

    public GameObject uiElement;

    private void Start()
    {
        // UI 요소를 숨깁니다.
        uiElement.SetActive(false);
    }

}
