using UnityEngine;

public class TorreBotoesManager : MonoBehaviour
{
    private void Update()
    {
        // CRIANDO TIME
        if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.GetComponent<TorreManager>().inventarioManager.mostrandoInventario)
        {
            gameObject.GetComponent<TorreManager>().inventarioManager.ProximaCartaGeral();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.GetComponent<TorreManager>().inventarioManager.mostrandoInventario)
        {
            gameObject.GetComponent<TorreManager>().inventarioManager.CartaAnteriorGeral();
        }

        // ACOES DA TORRE
        if (Input.GetKeyDown(KeyCode.A) && gameObject.GetComponent<TorreManager>().torreEmAndamento)
        {
            gameObject.GetComponent<CombateTorre>().AtaquePlayer();
        }

        if (Input.GetKeyDown(KeyCode.C) && gameObject.GetComponent<TorreManager>().torreEmAndamento)
        {
            gameObject.GetComponent<CombateTorre>().CuraPlayer();

        }

        // FORA DA TORRE
        if (Input.GetKeyDown(KeyCode.Backspace) && !gameObject.GetComponent<TorreManager>().torreEmAndamento)
        {
            MenuManager.LoadMenuScene();
        }
    }

    public void ProximaCartaTorre()
    {
        gameObject.GetComponent<TorreManager>().inventarioManager.ProximaCartaGeral();
    }

    public void CartaAnterioraTorre()
    {
        gameObject.GetComponent<TorreManager>().inventarioManager.CartaAnteriorGeral();
    }

    public void ProxTorre()
    {
        gameObject.GetComponent<TorreManager>().i++;

    }

    public void TorreAnteriora()
    {
        gameObject.GetComponent<TorreManager>().i--;

    }
}
