using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levels;
    public GameObject capters;
    public GameObject chapter1;

    public void OpenLevels()
    {
        levels.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseLevels()
    {
        levels.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenChapter1()
    {
        chapter1.SetActive(true);
    }

    public void CloseChapter1()
    {
        chapter1.SetActive(false);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Scene1");
    }
}
