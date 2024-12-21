using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            // Eðer Escape veya P tuþlarýna basýlýrsa, Toggle() metodunu çalýþýr.
            Toggle();
        }
    }

    public void Toggle() // Pause menüsünü açýp kapatýr.
    {
        //  Pause menüsünün aktifliðini tersine çevirir.
        //  Yani aktifse pasif hale getirir, pasifse aktif hale getirir
        ui.SetActive(!ui.activeSelf);

        if(ui.activeSelf)
        {
            Time.timeScale = 0f; // Oyunun zaman akýþýný durdurur.
        }
        else
        {
            Time.timeScale = 1f; // Oyunun zaman akýþýný tekrar baþlatýr.
        }
    }
    // Time.timeScale'ý 0 yaparak oyun zamanýný durdurur.
    // Bu, oyundaki tüm zamanla ilgili iþlemlerin durmasýna neden olur.
    // Örneðin, karakterler hareket etmeyi býrakýr, animasyonlar durur.

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
