/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
//Comentários feitos por: Arthur Von Peer Cubakowic
public class MenuManager : MonoBehaviour
{
    public GameObject videoCanvas;      //gameobject que guarda os objetos de vídeo 
    public VideoPlayer video;           //vídeo inicial

    private void Start()
    {
        videoCanvas.SetActive(true);                           //garante que o jogo começa com o vídeo tocando
        gameObject.GetComponent<AudioSource>().Stop();         //pausa a música de fundo
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !video.isPlaying)  //atalho para a troca de cena 
        {
            LoadGachaScene();
        }
        if (Input.GetKeyDown(KeyCode.I) && !video.isPlaying)  //atalho para a troca de cena 
        {
            LoadInventarioScene();
        }
        if (Input.GetKeyDown(KeyCode.T) && !video.isPlaying)  //atalho para a troca de cena 
        {
            LoadTorreScene();
        }

        if (Input.anyKeyDown && video.isPlaying)            //pula o vídeo se alguma tecla for pressionada
        {
            video.Stop();
        }

        if (!video.isPlaying)                               //esconde o gameobject do vídeo e começa a música de fundo
        {
            videoCanvas.SetActive(false);

            if (!gameObject.GetComponent<AudioSource>().isPlaying)
                gameObject.GetComponent<AudioSource>().Play();

        }
    }

    // Funções Estaticas que gerenciam a troca de cena
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
