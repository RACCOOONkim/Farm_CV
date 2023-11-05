using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader2 : MonoBehaviour
{
    public void LoadScene()
    {
        // 'Scene2'이라는 씬을 로드합니다.
        SceneManager.LoadScene("Scene2");
    }
}
