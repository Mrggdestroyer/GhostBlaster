using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clipsToPlay;

    AudioSource AudioSource;

    private int currentScene;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        AudioSource = gameObject.GetComponent<AudioSource>();
        PlayAudios();
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene <= 2 || currentScene >= 13)
        {
            AudioSource.Stop();
        }
        
    }

    public void PlayAudios()
    {
        StartCoroutine("PlayOneByOne");
    }

    IEnumerator PlayOneByOne()
    {
        yield return null;

        for (int i = 0; i < clipsToPlay.Length; i++)
        {
            AudioSource.clip = clipsToPlay[i]; 
            AudioSource.Play();

            while(AudioSource.isPlaying)
            {
                yield return null;
            }
        }
    }
}
