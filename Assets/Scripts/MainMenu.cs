using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject thingsToHide;
    public GameObject Instruction;
    public GameObject credits;

    private void Start()
    {
        thingsToHide.SetActive(true);
        Instruction.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Instructions()
    {
        thingsToHide.SetActive(false);
        Instruction.SetActive(true);
    }

    public void GoBack()
    {
        thingsToHide.SetActive(true);
        Instruction.SetActive(false);
        credits.SetActive(false);
    }

    public void Credits()
    {
        thingsToHide.SetActive(false);
        credits.SetActive(true); 
    }
}
