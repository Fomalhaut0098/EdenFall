using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStartButton() //���� ���� ��ư�� �������� 
    {
        SceneManager.LoadScene("T1"); //�� ������ �Ѿ��
    }

    public void SettingButton() //���� ��ư�� �������� 
    {
        SceneManager.LoadScene("Help"); //�� ������ �Ѿ��
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
