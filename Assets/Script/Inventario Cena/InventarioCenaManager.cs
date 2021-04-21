using UnityEngine;
using UnityEngine.UI;

public class InventarioCenaManager : MonoBehaviour
{
    public GameObject templateCarta;

    public GameObject botaoCartas;
    public GameObject interfaceCartas;


    public int i;
    public bool mostrandoInventario;

    private void Awake()
    {

        i = 0;
    }

    public void InventarioCartas()
    {
        mostrandoInventario = true;

        botaoCartas.SetActive(false);
        interfaceCartas.SetActive(true);

        Inventory.playerData.inventarioCartas.Sort();
        InstanciaCarta(Inventory.playerData.inventarioCartas[i]);

        AtualizaLevel();

    }

    public void FecharInventarioCartas()
    {
        Destroy(GameObject.Find("Carta"));
        botaoCartas.SetActive(true);
        interfaceCartas.SetActive(false);

        mostrandoInventario = false;
    }

    public void ProximaCarta()
    {
        ProximaCartaGeral();
        AtualizaLevel();
    }

    public void ProximaCartaGeral()
    {
        i++;
        if (i == Inventory.playerData.inventarioCartas.Count)
            i = 0;

        Destroy(GameObject.Find("Carta"));
        InstanciaCarta(Inventory.playerData.inventarioCartas[i]);
    }

    public void CartaAnterior()
    {
        CartaAnteriorGeral();
        AtualizaLevel();
    }

    public void CartaAnteriorGeral()
    {
        i--;
        if (i < 0)
            i = Inventory.playerData.inventarioCartas.Count - 1;

        Destroy(GameObject.Find("Carta"));
        InstanciaCarta(Inventory.playerData.inventarioCartas[i]);
    }

    public void InstanciaCarta(InventoryDataCarta dataCarta)
    {
        GameObject novaCarta = Instantiate(templateCarta, GameObject.Find("Canvas").transform.position - new Vector3(150,0,0), Quaternion.identity);
        novaCarta.transform.parent = GameObject.Find("Canvas").transform;


        novaCarta.name = "Carta";

        // Instancia a carta do componente CardDisplay de cartaGacha, com uma carta sorteada
        novaCarta.GetComponent<CardDisplay>().dataCarta = dataCarta;
        novaCarta.GetComponent<CardDisplay>().LoadDataCarta();

    }

    public void AtualizaLevel()
    {
        GameObject.Find("Lvl").GetComponent<Text>().text = Inventory.playerData.inventarioCartas[i].lvl.ToString();
        GameObject.Find("InterfaceCartas").transform.SetAsLastSibling();
    }
}
