using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    public void ButtonClick()
    {
        Debug.Log("Chamou a funcao ButtonClick");
        FindObjectOfType<AudioManager>().Play("Button Sound");
    }

    // Funcoes Estaticas
    public static void LoadMenuScene()
    {
        Inventory.SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public static void LoadGachaScene()
    {
        Inventory.SaveGame();
        SceneManager.LoadScene("GachaScene");
    }

    public static void LoadInventarioScene()
    {
        Inventory.SaveGame();
        SceneManager.LoadScene("Inventario");
    }

    public static void LoadTorreScene()
    {
        Inventory.SaveGame();
        SceneManager.LoadScene("Torre");
    }

    public static void FechaJogo()
    {
        Inventory.SaveGame();
        Application.Quit();
    }

}
