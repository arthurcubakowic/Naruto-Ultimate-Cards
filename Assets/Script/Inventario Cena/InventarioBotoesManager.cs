
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventarioBotoesManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().FecharInventarioCartas();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            Inventory.SaveGame();
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().ProximaCarta();

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().CartaAnterior();

        }
        if (Input.GetKeyDown(KeyCode.C) && !gameObject.GetComponent<InventarioCenaManager>().mostrandoInventario)
        {
            gameObject.GetComponent<InventarioCenaManager>().InventarioCartas();

        }


    }
}
