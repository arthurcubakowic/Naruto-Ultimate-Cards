/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;
using UnityEngine.SceneManagement;

// Comentado por: Matheus Carvalho
public class InventarioBotoesManager : MonoBehaviour
{
    private void Update()
    {
        // Fecha o inventário se a tecla ESC for clicada
        if (Input.GetKeyUp(KeyCode.Escape) && gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().FecharInventarioCartas();
        }

        // Sai do inventário e troca de cena para o menu
        if (Input.GetKeyDown(KeyCode.Escape) && !gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            Inventory.SaveGame();
            SceneManager.LoadScene("Menu");
        }

        // Visualiza a próxima carta caso clique na seta direita
        if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().ProximaCarta();

        }

        // Visualiza a carta anterior caso clique na seta esquerda
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().CartaAnterior();

        }

        // Abre o inventário e visualiza as cartas, uma por uma
        if (Input.GetKeyDown(KeyCode.C) && !gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().InventarioCartas();
        }
    }
}
