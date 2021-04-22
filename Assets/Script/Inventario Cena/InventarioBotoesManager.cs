
using UnityEngine;
using UnityEngine.SceneManagement;

// Comentado por: Matheus Carvalho
public class InventarioBotoesManager : MonoBehaviour
{
    private void Update()
    {
        // Fecha o invent�rio se a tecla ESC for clicada
        if (Input.GetKeyUp(KeyCode.Escape) && gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().FecharInventarioCartas();
        }

        // Sai do invent�rio e troca de cena para o menu
        if (Input.GetKeyDown(KeyCode.Escape) && !gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            Inventory.SaveGame();
            SceneManager.LoadScene("Menu");
        }

        // Visualiza a pr�xima carta caso clique na seta direita
        if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().ProximaCarta();

        }

        // Visualiza a carta anterior caso clique na seta esquerda
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().CartaAnterior();

        }

        // Abre o invent�rio e visualiza as cartas, uma por uma
        if (Input.GetKeyDown(KeyCode.C) && !gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().InventarioCartas();
        }
    }
}
