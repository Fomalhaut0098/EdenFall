using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tutorial : MonoBehaviour
{
    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;

    public void SetTutorialImage(int index)
    {
        switch(index)
        {
            case 1:
                T1.SetActive(true);
                T2.SetActive(false);
                T3.SetActive(false);
                T4.SetActive(false);
                break;
            case 2:
                T1.SetActive(false);
                T2.SetActive(true);
                T3.SetActive(false);
                T4.SetActive(false);
                break;
            case 3:
                T1.SetActive(false);
                T2.SetActive(false);
                T3.SetActive(true);
                T4.SetActive(false);
                break;
            case 4:
                T1.SetActive(false);
                T2.SetActive(false);
                T3.SetActive(false);
                T4.SetActive(true);
                break;
        }
    }

}
