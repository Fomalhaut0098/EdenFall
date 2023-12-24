using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.ExceptionServices;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI_Info : MonoBehaviour
{
    public RectTransform uiElement; // 이동할 UI 요소

    public float inv_speed = 50.0f;

    public AudioSource AudioSource;
    public AudioClip Phone;

    public GameObject UI_Hp4;
    public GameObject UI_Hp3;
    public GameObject UI_Hp2;
    public GameObject UI_Hp1;

    public GameObject UI_Key1;
    public GameObject UI_Key2;
    public GameObject UI_Key3;
    public GameObject UI_Key4;
    public GameObject UI_Key5;

    public RectTransform UI_SelectedAmmo;

    public Text AmmoQuantity;
    private string inAmmoQuantity;

    public Text Memory;

    public Text Battery;
    private string inBattery;

    private void Update()
    {
        switch (GameManager.Instance.hp)
        {
            case 1:
                UI_Hp4.SetActive(false);
                UI_Hp3.SetActive(false);
                UI_Hp2.SetActive(false);
                UI_Hp1.SetActive(true);
                break;
            case 2:
                UI_Hp4.SetActive(false);
                UI_Hp3.SetActive(false);
                UI_Hp2.SetActive(true);
                UI_Hp1.SetActive(true);
                break;
            case 3:
                UI_Hp4.SetActive(false);
                UI_Hp3.SetActive(true);
                UI_Hp2.SetActive(true);
                UI_Hp1.SetActive(true);
                break;
            case 4:
                UI_Hp4.SetActive(true);
                UI_Hp3.SetActive(true);
                UI_Hp2.SetActive(true);
                UI_Hp1.SetActive(true);
                break;
        }

        switch (GameManager.Instance.keyLevel)
        {
            case 0:
                UI_Key1.SetActive(false);
                UI_Key2.SetActive(false);    
                UI_Key3.SetActive(false);
                UI_Key4.SetActive(false);
                UI_Key5.SetActive(false);
                break;

            case 1:
                UI_Key1.SetActive(true);
                UI_Key2.SetActive(false);
                UI_Key3.SetActive(false);
                UI_Key4.SetActive(false);
                UI_Key5.SetActive(false);
                break;

            case 2:
                UI_Key1.SetActive(true);
                UI_Key2.SetActive(true);
                UI_Key3.SetActive(false);
                UI_Key4.SetActive(false);
                UI_Key5.SetActive(false);
                break;
            case 3:
                UI_Key1.SetActive(true);
                UI_Key2.SetActive(true);
                UI_Key3.SetActive(true);
                UI_Key4.SetActive(false);
                UI_Key5.SetActive(false);
                break;
            case 4:
                UI_Key1.SetActive(true);
                UI_Key2.SetActive(true);
                UI_Key3.SetActive(true);
                UI_Key4.SetActive(true);
                UI_Key5.SetActive(false);
                break;
            case 5:
                UI_Key1.SetActive(true);
                UI_Key2.SetActive(true);
                UI_Key3.SetActive(true);
                UI_Key4.SetActive(true);
                UI_Key5.SetActive(true);
                break;
        }

        switch (GameManager.Instance.weaponType)
        {
            case 1:
                UI_SelectedAmmo.anchoredPosition = new Vector2(-20, 75);
                break;
            case 2:
                UI_SelectedAmmo.anchoredPosition = new Vector2(-20, 20);
                break;
            case 3:
                UI_SelectedAmmo.anchoredPosition = new Vector2(-20, -35);
                break;
            case 4:
                UI_SelectedAmmo.anchoredPosition = new Vector2(-20, -90);
                break;
        }

        inAmmoQuantity = GameManager.Instance.leftAmmo[0] + "\n" + GameManager.Instance.leftAmmo[1] + "\n" + GameManager.Instance.leftAmmo[2] + "\n" + GameManager.Instance.leftAmmo[3];
        AmmoQuantity.text = inAmmoQuantity.ToString();

        if (GameManager.Instance.leftMemoryInScene > 0)
        {
            Memory.text = "기억의 파편이 " + GameManager.Instance.leftMemoryInScene + "개 남았다...";
        }
        else
        {
            Memory.text = "모든 기억의 파편을 찾은 것 같다...";
        }

        inBattery = "Battery " + GameManager.Instance.Battery + "%";
        Battery.text = inBattery.ToString();
    }

    public void UI_Active (bool inActive)
    {
        if (inActive)
        {
            AudioSource.PlayOneShot(Phone);
            MoveToPosition(new Vector2(300, -0), inv_speed);
        }
        else
        {
            AudioSource.PlayOneShot(Phone);
            MoveToPosition(new Vector2(300, -750), inv_speed);
        }
    }


    // 이 함수를 호출하여 UI 요소를 이동시킵니다.
    // targetPosition: 이동할 위치, speed: 이동 속도
    public void MoveToPosition(Vector2 targetPosition, float speed)
    {
        StartCoroutine(MoveOverSpeed(uiElement, targetPosition, speed));
    }

    private IEnumerator MoveOverSpeed(RectTransform objectToMove, Vector2 end, float speed)
    {
        while (Vector2.Distance(objectToMove.anchoredPosition, end) > 0.01f)
        {
            objectToMove.anchoredPosition = Vector2.MoveTowards(objectToMove.anchoredPosition, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        objectToMove.anchoredPosition = end; // 최종 위치로 설정
    }
}

