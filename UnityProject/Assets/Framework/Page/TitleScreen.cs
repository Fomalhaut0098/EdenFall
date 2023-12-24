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

    public void GameStartButton() //게임 시작 버튼을 눌렀을때 
    {
        SceneManager.LoadScene("T1"); //이 신으로 넘어가기
    }

    public void SettingButton() //설정 버튼을 눌렀을때 
    {
        SceneManager.LoadScene("Help"); //이 신으로 넘어가기
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
