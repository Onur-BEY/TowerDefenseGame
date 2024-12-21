using UnityEngine;

public class Endgame : MonoBehaviour
{
    public GameObject endGameUI;
    public GameObject completeLevelUI;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    public void endGameMethod()
    {
        completeLevelUI.SetActive(false);
        endGameUI.SetActive(true);
        enabled = false;
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
