/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;
using UnityEngine.UI;

// Comentado por: Arthur Von Peer Cubakowic
public class InventarioCenaManager : MonoBehaviour
{

    // UI
    public GameObject templateCarta;     // Referencia do Prefab da carta 
    public GameObject botaoCartas;
    public GameObject interfaceCartas;

    // Variaveis locais
    public int i;
    public bool mostrandoInventario;

    private void Awake()
    {
        i = 0;
    }

    public void InventarioCartas()
    {
        // Define que o inventario está sendo mostrado na UI
        mostrandoInventario = true;

        // Define a UI de inventario
        botaoCartas.SetActive(false); // esconde o botao pra ativar a UI de inventario
        interfaceCartas.SetActive(true);

        // Ordena o inventario do player
        Inventory.playerData.inventarioCartas.Sort();

        // Instancia a carta com menor ID do inventario do jogador
        InstanciaCarta(Inventory.playerData.inventarioCartas[i]);

        // Atualiza o Game Object com o Level da carta
        AtualizaLevel();

    }

    // Fecha a UI de inventario
    public void FecharInventarioCartas()
    {
        Destroy(GameObject.Find("Carta"));
        botaoCartas.SetActive(true);
        interfaceCartas.SetActive(false);

        mostrandoInventario = false;
    }

    // Passa para a proxima carta, na Cena de inventario ele atualiza o level da carta
    public void ProximaCarta()
    {
        ProximaCartaGeral();
        AtualizaLevel();
    }

    // Passa para a proxima carta
    public void ProximaCartaGeral()
    {
        i++;
        if (i == Inventory.playerData.inventarioCartas.Count)
            i = 0;

        Destroy(GameObject.Find("Carta"));
        InstanciaCarta(Inventory.playerData.inventarioCartas[i]);
    }

    // volta para a carta anteriora, na Cena de inventario ele atualiza o level da carta
    public void CartaAnterior()
    {
        CartaAnteriorGeral();
        AtualizaLevel();
    }

    // volta para a carta anteriora
    public void CartaAnteriorGeral()
    {
        i--;
        if (i < 0)
            i = Inventory.playerData.inventarioCartas.Count - 1;

        Destroy(GameObject.Find("Carta"));
        InstanciaCarta(Inventory.playerData.inventarioCartas[i]);
    }

    // Instancia uma Prefab de carta através de um dataCarta
    public void InstanciaCarta(InventoryDataCarta dataCarta)
    {
        GameObject novaCarta = Instantiate(templateCarta, GameObject.Find("Canvas").transform.position - new Vector3(150,0,0), Quaternion.identity);
        novaCarta.transform.parent = GameObject.Find("Canvas").transform;


        novaCarta.name = "Carta";

        // Instancia a carta do componente CardDisplay de cartaGacha, com uma carta sorteada
        novaCarta.GetComponent<CardDisplay>().dataCarta = dataCarta;
        novaCarta.GetComponent<CardDisplay>().LoadDataCarta();

    }

    // Atualiza a UI de level da carta
    public void AtualizaLevel()
    {
        GameObject.Find("Lvl").GetComponent<Text>().text = Inventory.playerData.inventarioCartas[i].lvl.ToString();
        GameObject.Find("InterfaceCartas").transform.SetAsLastSibling();
    }
}
