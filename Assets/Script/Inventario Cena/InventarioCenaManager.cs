/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;
using UnityEngine.UI;

//Comentarios feitos por: Gustavo da Silva Oliveira

public class InventarioCenaManager : MonoBehaviour
{
    public GameObject templateCarta;        //game object do template da carta

    public GameObject botaoCartas;          //game object do botão das cartas
    public GameObject interfaceCartas;      //game object da interface das cartas


    public int i;
    public bool mostrandoInventario;        //booleano que indica se o inventário está sendo mostrado

    private void Awake()
    {

        i = 0;
    }

    //visualizar as cartas do inventário
    public void InventarioCartas()
    {
        mostrandoInventario = true;

        botaoCartas.SetActive(false);           //some com o botão para visualizar as cartas
        interfaceCartas.SetActive(true);        //mostra as cartas contidas no inventário

        Inventory.playerData.inventarioCartas.Sort();               //embaralha as cartas do invenrário
        InstanciaCarta(Inventory.playerData.inventarioCartas[i]);   //define a carta que está sendo mostrada

        AtualizaLevel();           //chama a função atualiza level

    }

    //
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
