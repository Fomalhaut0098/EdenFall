using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Spawner Spawner;
    public InteractiveManager InteractiveManager;
    public Doctor Doctor;

    public AudioSource AudioSource;
    public AudioClip G1;
    public AudioClip G2;
    public AudioClip BOSS;

    public string preScene;

    public bool notDead = true;

    private float timer = 0; // 시간을 추적할 변수

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

    private void Start()
    {
        AudioSource.loop = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name == "Ending")
        {
            Destroy(gameObject);
        }

        if(SceneManager.GetActiveScene().name != "Death")
        {
            Doctor = FindObjectOfType<Doctor>();
            Spawner = FindObjectOfType<Spawner>();
            InteractiveManager = FindObjectOfType<InteractiveManager>();

            //리스폰시 불러올 최종 값 저장
            preScene = SceneManager.GetActiveScene().name;

            re_hp = hp;
            re_Battery = Battery;
            re_keyLevel = keyLevel;
            re_Memory = InteractiveManager.Memory;
            re_Interactives = InteractiveManager.Interactives;

            for (int i = 0; i < 4; i++)
            {
                re_leftAmmo[i] = leftAmmo[i];
            }

            if(SceneManager.GetActiveScene().name == "G1")
            {
                AudioSource.Stop();
                AudioSource.clip = G1;
                AudioSource.Play();
            }
            else if (SceneManager.GetActiveScene().name == "G2")
            {
                AudioSource.Stop();
                AudioSource.clip = G2;
                AudioSource.Play();
            }
            else if(SceneManager.GetActiveScene().name == "Boss")
            {
                AudioSource.Stop();
                AudioSource.clip = BOSS;
                AudioSource.Play();
            }
            else if(SceneManager.GetActiveScene().name == "Final" || SceneManager.GetActiveScene().name == "Ending")
            {
                AudioSource.Stop();
            }
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

    //플레이어 관련 변수
    public int hp = 4; // HP
    public int weaponType = 1; //들고 있는 무기 타입
    public List<int> leftAmmo = new List<int> { 0, 0, 0, 0 }; //남은 총알 수 
    public int Battery = 100;
    public int keyLevel = 0;


    //리스폰 변수
    public int re_hp = 4; // HP
    public List<int> re_leftAmmo = new List<int> { 0, 0, 0, 0 }; //남은 총알 수 
    public int re_Battery = 100;
    public int re_keyLevel = 0;
    public Dictionary<string, List<string>> re_Interactives = new Dictionary<string, List<string>>();
    public Dictionary<string, int> re_Memory = new Dictionary<string, int>();

    //스프라이트 사이에서 통신에 쓰이는 변수
    public string TriggeredEvent = "";

    public int leftMemoryInScene = 0;

    private void Update()
    {
        timer += Time.deltaTime; // 매 프레임마다 경과 시간을 더함

        if (timer >= 2f) // 1초가 경과했는지 확인
        {
            if(Battery > 0)
            {
                Battery -= 1; // 변수에서 1을 뺌
            }
            timer = 0; // 타이머를 다시 0으로 설정
        }
    }

    private void LateUpdate()
    {
        if(hp < 0)
        {
            if(notDead)
            {
                SceneManager.LoadScene("Death");
                AudioSource.Stop();
                notDead = false;
            }
        }

        InteractiveManager.Memory.TryGetValue(SceneManager.GetActiveScene().name, out int LeftMemory);
        leftMemoryInScene = LeftMemory;
    }
}