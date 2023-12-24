using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBGM : MonoBehaviour
{
    public static TitleBGM Instance;

    public AudioSource AudioSource;
    public AudioClip TitleMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "MainScreen" || SceneManager.GetActiveScene().name == "Ending" || SceneManager.GetActiveScene().name == "Help")
        {
            if (!AudioSource.isPlaying)
            {
                AudioSource.clip = TitleMusic;
                AudioSource.Play();
                AudioSource.loop = true;
            }
        }
        else
        {
            AudioSource.Stop();
        }
    }
}
