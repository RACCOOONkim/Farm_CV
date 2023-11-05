using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader1 : MonoBehaviour
{
    public void LoadScene()
    {
        // 'Scene1'이라는 씬을 로드합니다.
        SceneManager.LoadScene("Scene1");
    }
}
