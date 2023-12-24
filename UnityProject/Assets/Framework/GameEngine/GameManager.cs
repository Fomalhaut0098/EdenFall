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

    private float timer = 0; // �ð��� ������ ����

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

            //�������� �ҷ��� ���� �� ����
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

    //�÷��̾� ���� ����
    public int hp = 4; // HP
    public int weaponType = 1; //��� �ִ� ���� Ÿ��
    public List<int> leftAmmo = new List<int> { 0, 0, 0, 0 }; //���� �Ѿ� �� 
    public int Battery = 100;
    public int keyLevel = 0;


    //������ ����
    public int re_hp = 4; // HP
    public List<int> re_leftAmmo = new List<int> { 0, 0, 0, 0 }; //���� �Ѿ� �� 
    public int re_Battery = 100;
    public int re_keyLevel = 0;
    public Dictionary<string, List<string>> re_Interactives = new Dictionary<string, List<string>>();
    public Dictionary<string, int> re_Memory = new Dictionary<string, int>();

    //��������Ʈ ���̿��� ��ſ� ���̴� ����
    public string TriggeredEvent = "";

    public int leftMemoryInScene = 0;

    private void Update()
    {
        timer += Time.deltaTime; // �� �����Ӹ��� ��� �ð��� ����

        if (timer >= 2f) // 1�ʰ� ����ߴ��� Ȯ��
        {
            if(Battery > 0)
            {
                Battery -= 1; // �������� 1�� ��
            }
            timer = 0; // Ÿ�̸Ӹ� �ٽ� 0���� ����
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