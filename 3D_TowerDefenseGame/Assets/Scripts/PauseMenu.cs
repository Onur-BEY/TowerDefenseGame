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
            // E�er Escape veya P tu�lar�na bas�l�rsa, Toggle() metodunu �al���r.
            Toggle();
        }
    }

    public void Toggle() // Pause men�s�n� a��p kapat�r.
    {
        //  Pause men�s�n�n aktifli�ini tersine �evirir.
        //  Yani aktifse pasif hale getirir, pasifse aktif hale getirir
        ui.SetActive(!ui.activeSelf);

        if(ui.activeSelf)
        {
            Time.timeScale = 0f; // Oyunun zaman ak���n� durdurur.
        }
        else
        {
            Time.timeScale = 1f; // Oyunun zaman ak���n� tekrar ba�lat�r.
        }
    }
    // Time.timeScale'� 0 yaparak oyun zaman�n� durdurur.
    // Bu, oyundaki t�m zamanla ilgili i�lemlerin durmas�na neden olur.
    // �rne�in, karakterler hareket etmeyi b�rak�r, animasyonlar durur.

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
