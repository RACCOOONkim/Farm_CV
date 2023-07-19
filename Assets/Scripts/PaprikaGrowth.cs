using System.Collections;
using UnityEngine;

public class PaprikaGrowth : MonoBehaviour
{
    [SerializeField] private GameObject[] childObjects; // 자식 객체들을 배열로 받습니다.
    private int currentIndex = 0; // 현재 활성화할 자식 객체의 인덱스

    private void Start()
    {
        // 처음에 모든 자식 객체를 비활성화합니다.
        foreach (GameObject obj in childObjects)
        {
            obj.SetActive(false);
        }

        // 자식 객체들을 일정 간격으로 활성화시키는 코루틴을 시작합니다.
        StartCoroutine(ActivateChildObjects());
    }

    private IEnumerator ActivateChildObjects()
    {
        while (currentIndex < childObjects.Length)
        {
            // 현재 인덱스에 해당하는 자식 객체를 활성화합니다.
            childObjects[currentIndex].SetActive(true);

            // 0.1초 대기
            yield return new WaitForSeconds(0.1f);

            // 다음 인덱스로 이동합니다.
            currentIndex++;
        }
    }
}
