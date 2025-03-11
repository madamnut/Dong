using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PressAnyKey : MonoBehaviour
{
    public Text pressAnyKeyText;
    public string gameSceneName = "Game";
    public AudioClip keyPressSound; // 효과음 AudioClip을 Inspector에서 할당
    private AudioSource audioSource;

    void Awake()
    {
        // AudioSource 컴포넌트를 가져오거나 없으면 추가합니다.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        // 텍스트 깜빡임 효과 시작
        StartCoroutine(BlinkText());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            // 효과음 재생
            if (keyPressSound != null)
            {
                audioSource.PlayOneShot(keyPressSound);
            }

            // 효과음을 들을 수 있도록 약간의 지연을 주고 씬 전환을 할 수도 있습니다.
            // 예: StartCoroutine(LoadSceneWithDelay(0.3f));
            // 여기서는 즉시 씬 전환하도록 합니다.
            StartCoroutine(LoadSceneWithDelay(0.3f));
            //SceneManager.LoadScene(gameSceneName);
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            pressAnyKeyText.CrossFadeAlpha(0f, 1f, false);
            yield return new WaitForSeconds(1f);
            pressAnyKeyText.CrossFadeAlpha(1f, 1f, false);
            yield return new WaitForSeconds(1f);
        }
    }

    // 옵션: 효과음이 재생될 시간을 주기 위한 씬 전환 지연 코루틴
    IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(gameSceneName);
    }

    void OnEnable()
    {
        pressAnyKeyText.canvasRenderer.SetAlpha(1f);
        StartCoroutine(BlinkText());
    }
}
