/// <summary>
/// Projeto Final Programaao Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhao, Nicolas Borges
/// Data: 22/04/2021
/// Versao do Unity: 2020.3.0f1
/// </summary>
 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
//Comentários feitos por: Gustavo da Silva Oliveira
public class ButtonManager : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);        //sempre que o gameobject com essa classe for clicado, ele soltara um som 
    }

    public void ButtonClick()
    {
        Debug.Log("Chamou a funcao ButtonClick");
        FindObjectOfType<AudioManager>().Play("Button Sound");      //toca o som
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
