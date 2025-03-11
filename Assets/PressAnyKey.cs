using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PressAnyKey : MonoBehaviour
{
    public Text pressAnyKeyText;
    public string gameSceneName = "Game";

    void Start()
    {
        // 텍스트 깜빡임 효과 시작
        StartCoroutine(BlinkText());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            // 천천히 사라졌다가 나타나는 효과
            pressAnyKeyText.CrossFadeAlpha(0f, 1f, false);
            yield return new WaitForSeconds(1f);
            pressAnyKeyText.CrossFadeAlpha(1f, 1f, false);
            yield return new WaitForSeconds(1f);
        }
    }

    void OnEnable()
    {
        pressAnyKeyText.canvasRenderer.SetAlpha(1f);
        StartCoroutine(BlinkText());
    }
}
