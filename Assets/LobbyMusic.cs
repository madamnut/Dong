using UnityEngine;

public class LobbyMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // Inspector에서 할당
    private AudioSource audioSource;

    void Awake()
    {
        // AudioSource 컴포넌트를 가져오거나 없으면 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;       // 무한 반복 설정
        audioSource.playOnAwake = true;  // 씬 시작시 자동 재생
        audioSource.Play();
    }
}
