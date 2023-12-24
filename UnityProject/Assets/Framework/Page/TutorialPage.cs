using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPage : MonoBehaviour
{
    public GameObject UI_Tutorial;
    private UI_Tutorial TutorialScript;

    private int Page = 1;

    // Start is called before the first frame update
    void Start()
    {
        TutorialScript = UI_Tutorial.GetComponent<UI_Tutorial>();
        TutorialScript.SetTutorialImage(1);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Next"))
        {
            Page++;
            if (Page > 4)
            {
                SceneManager.LoadScene("MainScreen");
            }
            TutorialScript.SetTutorialImage(Page);
        }
    }

    public void NextButton()
    {
        Page++;
        if (Page > 4)
        {
            SceneManager.LoadScene("MainScreen");
        }
        TutorialScript.SetTutorialImage(Page);
    }
}
