using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

// Comentado por Gustavo da Silva Oliveira
public class MenuManager : MonoBehaviour
{
    public GameObject videoCanvas;
    public VideoPlayer video;
    public bool videoRodando;

    private void Start()
    {
        videoCanvas.SetActive(true);
        videoRodando = true;
        gameObject.GetComponent<AudioSource>().Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !videoRodando)
        {
            LoadGachaScene();
        }
        if (Input.GetKeyDown(KeyCode.I) && !videoRodando)
        {
            LoadInventarioScene();
        }
        if (Input.GetKeyDown(KeyCode.T) && !videoRodando)
        {
            LoadTorreScene();
        }

        if (Input.anyKeyDown && videoRodando)
        {
            video.Stop();
        }

        if (!video.isPlaying)
        {
            videoCanvas.SetActive(false);
            videoRodando = false;
            if (!gameObject.GetComponent<AudioSource>().isPlaying)
                gameObject.GetComponent<AudioSource>().Play();

        }
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


}
