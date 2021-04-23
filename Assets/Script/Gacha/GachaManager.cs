/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// Comentado por: Arthur von Peer Cubakowic
public class GachaManager : MonoBehaviour
{
    public static GachaManager instancia; // transforma em singleton

    [Header("Referencia para Instanciamento")]
    // Componentes para instanciamento de cartas
    public GameObject cartaTemplate;
    public GameObject paiCartas;


    [Header("UI")]
    // Componentes da UI
    public GameObject mensagemPrefab;
    public Image fundoTela;
    public Text currencyText;
    public Text rankDaCurrency;
    public Text rankBotaoCompra;

    [Header("Variaveis Locais")]
    // Variaveis locais
    public float distanciaEntreCartas = 200;


    [Header("Variaveis do Reedem")]
    // Variaveis para manegamento das paginas de Gacha
    public string rank;
    public int n; 
    [Range(0,5)]
    public int pagina;

    [Header("Fundos de tela")]
    public Sprite fundoD;
    public Sprite fundoC;
    public Sprite fundoB;
    public Sprite fundoA;
    public Sprite fundoS;
    public Sprite fundoSS;



    private void Awake()
    {
        if (instancia == null) // transforma em singleton
            instancia = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    void Update()
    {
        // Atualiza a quantidade de Currency mostrada para o jogador, assim como qual currency esta sendo mostrada
        currencyText.text = Inventory.playerData.currency[pagina].ToString();
        rankDaCurrency.text = rank;

        // Esc sai volta a pagina
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenuScene();
        }
        
        // Essa parte serve para Debugar, mas atrapalha na jogabilidade do jogo
        /*if (Input.GetKeyDown(KeyCode.F3))
        {
            Inventory.playerData.currency[0] += 10000;
        }*/
    }

    // Cria a carta e adiciona ela ao inventario do jogador
    public void LoadNCartas(int n, string rank)
    {
        // Instancia N GameObject's das Cartas que serao sorteadas
        for (int i = 0; i < n; i++)
        {
            // Cria uma carta
            GameObject cartaGacha = CriaCarta(n, i, rank);

            // Carrega os atributos do ScriptableObject carta no GameObject
            cartaGacha.GetComponent<CardDisplay>().LoadCartaGacha();

            // Adiciona essa carta ao inventario do jogador
            Inventory.AddCarta(cartaGacha.GetComponent<CardDisplay>().carta);
        }
    }

    // Instancia a carta (Visual)
    public GameObject CriaCarta(int n, int i, string rank)
    {
        // Instancia o GameObject da Carta que será sorteada
        GameObject novaCarta = Instantiate(cartaTemplate, GameObject.Find("Canvas").transform.position + new Vector3((-distanciaEntreCartas / 2) * (n - 1) + distanciaEntreCartas * i, 0, 0), Quaternion.identity);
        novaCarta.transform.parent = paiCartas.transform;

        // Instancia a carta do componente CardDisplay de cartaGacha, com uma carta sorteada
        novaCarta.GetComponent<CardDisplay>().carta = (Resources.Load<Card>("Cartas/" + PegaUmaCartaRank(rank)));

        return novaCarta;
    }

    // Sorteia uma carta de um rank especifico
    public string PegaUmaCartaRank(string rank)
    {
        // Codigo baseado na aula do jogo de Cartas, porem separa por ';'
        TextAsset t1 = (TextAsset)Resources.Load("Lista de Cartas", typeof(TextAsset));
        string s = t1.text;
        string[] palavras = s.Split(';');

        // transforma o Rank passado (Char) para um numero (Char)
        string localRank = RankToIntString(rank);

        // Cria uma lista com todas as cartas do Rank passado
        List<string> cartasD = new List<string> { };
        foreach (string carta in palavras)
        {
            if (carta.EndsWith(localRank))
                cartasD.Add(carta);
        }

        // sorteia um numero dentre todas as cartas de rank passado
        int palavraAleatoria = Random.Range(0, cartasD.Count);

        // retorna a carta sorteado
        return cartasD[palavraAleatoria];
    }

    // recebe um Rank em letras e retorna em caracter
    public string RankToIntString (string rank)
    {
        string intRank;

        switch (rank)
        {
            case "D":
                intRank = "0";
                break;
            case "C":
                intRank = "1";
                break;
            case "B":
                intRank = "2";
                break;
            case "A":
                intRank = "3";
                break;
            case "S":
                intRank = "4";
                break;
            case "SS":
                intRank = "5";
                break;
            default:
                intRank = "";
                break;
        }

        return intRank;
    }

    // Destroi o Game Object da carta mostrada na tela
    public void UnloadAllCards()
    {
        for (int i = paiCartas.transform.childCount; i > 0; i--)
            Destroy(paiCartas.transform.GetChild(i - 1).gameObject);
    }

    // Salva o jogo e retorna pro menu
    public void LoadMenuScene()
    {
        Inventory.SaveGame();
        SceneManager.LoadScene("Menu");
    }
    
    // Funcao para chamar a rotina de sortear carta (Ela existe para ser colocada no botao do jogo)
    public void ReedemCarta()
    {
        StartCoroutine(RedeemCarta());
    }

    // Sorteia uma carta e adiciona ao inventario do jogador
    public IEnumerator RedeemCarta()
    {

        // Deleta todas as cartas existentes
        UnloadAllCards();

        // Le a pagina que o jogador está para saber qual é o rank da carta que ele deve sortear, se o jogador tiver a currency necessaria para sortear uma
        // carta ela sera sorteada
        if (Inventory.playerData.currency[pagina] >= n * 300)
        {
            Inventory.playerData.currency[pagina] -= n * 300;
            LoadNCartas(n, rank); // essa funcao que ira adicionar a carta ao inventario do jogador, assim como ela que irá mostrar a carta na tela
        }
        // Caso ele nao tenha currency suficiente, uma mensagem dizendo que ele nao tem dinheiro será mostrada na tela
        else
        {
            Debug.Log("Ryo Insuficiente");
            GameObject mensagemDinheiroInsuficiente = GameObject.Instantiate(mensagemPrefab, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            yield return new WaitForSecondsRealtime(1);
            Destroy(mensagemDinheiroInsuficiente);
        }

        yield return null;
    }

    // Passa para a pagina de sorteio de um rank a cima
    public void ProximaPagina()
    {
        // Deleta todas as cartas existentes
        UnloadAllCards();

        pagina++;
        if (pagina > 5)
        {
            pagina = 0;
        }
        AtualizaUI();
    }

    // Volta para a pagina de sorteio de um rank a baixo
    public void PaginaAnterior()
    {
        // Deleta todas as cartas existentes
        UnloadAllCards();

        pagina--;
        if (pagina < 0)
        {
            pagina = 5;
        }
        AtualizaUI();
    }

    // Atualiza a UI da Cena sempre que troca de pagina
    public void AtualizaUI()
    {
        switch (pagina)
        {
            case 0:
                fundoTela.sprite = fundoD;
                rank = "D";
                rankBotaoCompra.text = "Compra Carta D";
                break;
            case 1:
                fundoTela.sprite = fundoC;
                rank = "C";
                rankBotaoCompra.text = "Compra Carta C";
                break;
            case 2:
                fundoTela.sprite = fundoB;
                rank = "B";
                rankBotaoCompra.text = "Compra Carta B";
                break;
            case 3:
                fundoTela.sprite = fundoA;
                rank = "A";
                rankBotaoCompra.text = "Compra Carta A";
                break;
            case 4:
                fundoTela.sprite = fundoS;
                rank = "S";
                rankBotaoCompra.text = "Compra Carta S";
                break;
            case 5:
                fundoTela.sprite = fundoSS;
                rank = "SS";
                rankBotaoCompra.text = "Compra Carta SS";
                break;
            default:
                fundoTela.sprite = fundoD;
                rank = "D";
                rankBotaoCompra.text = "Compra Carta D";
                break;
        }
    }

}
