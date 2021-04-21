using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Bugs conhecidos: na formacao de time é possivel escolher a mesma carta 4 vezes, o ideal seria escolher apenas 1 de cada carta
public class TorreManager : MonoBehaviour
{
    [Header("Geral")]
    // Instancia referentes à torre
    public List<Torre> listaTorres;
    public Torre torre;
    public int andar;
    // Instancia referentes ao jogador
    public Time time;
    public InventarioCenaManager inventarioManager;
    // Instancia referente à musica de fundo
    public AudioClip musicaThema;

    [Header("UI")]
    // Inimigo UI
    public GameObject inimigoUI;
    public Image backgroundUI;
    public Text nomeInimigoUI;
    public Text vidaInimigoUI;

    // Player UI
    public GameObject playerUI;
    public Text vidaPlayerUI;

    // UI Geral
    public GameObject caixaMensagem;
    public Text textoMensagem;

    // Torre Select UI
    public GameObject torreSelect;
    public Image torreImage;
    public Text torreNome;

    [Header("Stats")]
    // Inimigo Stats
    public int vidaInimigo;
    public EnemyCard inimigo;

    // Player Stats
    public int vidaPlayer;
    [Space(20)]

    // Variaveis locais, algumas usadas em Torre Botoes Manager
    public bool torreEmAndamento;
    public bool torreSelectEmAndamento;
    public int i;

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().clip = musicaThema;
        gameObject.GetComponent<AudioSource>().Play();

        i = 0;

        torreEmAndamento = false;
        torreSelectEmAndamento = true;

        inimigoUI.SetActive(false);
        playerUI.SetActive(false);

        StartCoroutine(SelecionaTorre());

    }

    private void Update()
    {
        if (vidaPlayer <= 0 && torreEmAndamento && !(andar == torre.listaTorre.Count))
        {
            torreEmAndamento = false;
            StartCoroutine(GameOver());
        }

        if (vidaInimigo <= 0 && torreEmAndamento && !(andar == torre.listaTorre.Count))
        {
            FindObjectOfType<AudioManager>().Stop("Ataque Sound");
            FindObjectOfType<AudioManager>().Play("Inimigo Derrotado");
            ProximoAndar();
        }

    }

    //FALTA IMPLEMENTAR TUDO

    public IEnumerator SelecionaTorre()
    {
        int i = 0;
        textoMensagem.text = "Aperte Enter para selecionar a torre";

        torre = listaTorres[i];
        torreImage.sprite = listaTorres[i].imagemTorre;
        torreNome.text = listaTorres[i].nome;

        while (torreSelectEmAndamento)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.GetComponent<TorreManager>().torreSelectEmAndamento)
            {
                i++;
                if (i >= listaTorres.Count) i = 0;
                Debug.Log(i);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.GetComponent<TorreManager>().torreSelectEmAndamento)
            {
                i--;
                if (i < 0) i = listaTorres.Count - 1;
                Debug.Log(i);
            }

            if (Input.GetKeyDown(KeyCode.Return) && gameObject.GetComponent<TorreManager>().torreSelectEmAndamento)
            {
                gameObject.GetComponent<TorreManager>().torreSelectEmAndamento = false;
                FindObjectOfType<AudioManager>().Play("Select Carta");
            }

            Debug.Log("TA PRESO");

            torreImage.sprite = listaTorres[i].imagemTorre;
            torreNome.text = listaTorres[i].nome;

            yield return null;
        }

        textoMensagem.text = "Torre Selecionada!";

        torre = listaTorres[i];

        yield return new WaitForSecondsRealtime(1);
        torreSelect.SetActive(false);
        StartCoroutine(FormarTime());
    }
    public IEnumerator GameOver()
    {
        Debug.Log("Voce Perdeu!");

        gameObject.GetComponent<AudioSource>().Stop();
        FindObjectOfType<AudioManager>().Play("Lose Theme");

        caixaMensagem.SetActive(true);
        caixaMensagem.transform.position = GameObject.Find("Canvas").transform.position;
        textoMensagem.text = "Voce Perdeu! Volte Tudo";
        
        torreEmAndamento = false;

        bool continueGame = false;
        while (!continueGame)
        {
            if (Input.anyKeyDown)
            {
                continueGame = true;
                FindObjectOfType<AudioManager>().Stop("Lose Theme");
            }

            yield return null;
        }
        MenuManager.LoadMenuScene();
    }

    public IEnumerator Vitoria()
    {
        Debug.Log("Parabens Voce Completou a Torre");

        gameObject.GetComponent<AudioSource>().Stop();
        FindObjectOfType<AudioManager>().Play("Vitoria Theme");

        caixaMensagem.SetActive(true);
        caixaMensagem.transform.position = GameObject.Find("Canvas").transform.position;
        textoMensagem.text = "Parabens, Voce Completou a Torre!";

        torreEmAndamento = false;
        RecompensaJogador();

        bool continueGame = false;
        while (!continueGame)
        {
            if (Input.anyKeyDown)
            {
                continueGame = true;
                FindObjectOfType<AudioManager>().Stop("Vitoria Theme");
            }

            yield return null;
        }
        MenuManager.LoadMenuScene();
    }

    public void RecompensaJogador()
    {
        GameManager.AplicaRecompensa(torre.recompensa);
    }

    public IEnumerator FormarTime()
    {
        int i = 1;
        bool recebeuCarta = false;

        gameObject.GetComponent<TorreManager>().inventarioManager.InstanciaCarta(Inventory.playerData.inventarioCartas[gameObject.GetComponent<TorreManager>().inventarioManager.i]);
        gameObject.GetComponent<TorreManager>().inventarioManager.mostrandoInventario = true;


        while (!recebeuCarta)
        {
            textoMensagem.text = "Escolha o capitao da sua equipe";
            // quando o jogador apertar Espaco a carta que instanciada é selecionada para ser o capitao
            if (Input.GetKeyDown(KeyCode.Return))
            {
                time.MontaCapitao(Inventory.playerData.inventarioCartas[inventarioManager.i], ScriptableObject.CreateInstance<Equipamento>());
                recebeuCarta = true;

                FindObjectOfType<AudioManager>().Play("Select Carta");
            }
            yield return null;
        }
        recebeuCarta = false;


        while (!recebeuCarta)
        {
            textoMensagem.text = "Escolha o membro " + i + " da sua equipe";
            // Sempre que apertar Space manda uma carta para o formador de membro, até que sejam mandados os ultimos 3 membros da equipe
            if (Input.GetKeyDown(KeyCode.Return))
            {
                time.MontaMembro(Inventory.playerData.inventarioCartas[inventarioManager.i], ScriptableObject.CreateInstance<Equipamento>());
                i++;

                FindObjectOfType<AudioManager>().Play("Select Carta");
            }

            // todos os membros do time instanciados entao pode sair do While
            if (i >= 4)
            {
                recebeuCarta = true;
            }
            yield return null;
        }
        textoMensagem.text = "Time Formado!";
        yield return new WaitForSecondsRealtime(1);
        caixaMensagem.SetActive(false);

        // Com essa atribuicao de false, as setas para direita e esquerda nao instanciam mais novas cartas
        gameObject.GetComponent<TorreManager>().inventarioManager.mostrandoInventario = false;

        // Destroy a ultima carta instanciada
        Destroy(GameObject.Find("Carta"));

        time.MontaTime();

        //SelecionaTorre();

        IniciaTorre(); // Transferir essa chamada pra SelecionaTorre()

        yield break;
    }

    public void IniciaTorre()
    {
        torreEmAndamento = true;
        andar = 0;
        inimigoUI.SetActive(true);
        playerUI.SetActive(true);

        FindObjectOfType<AudioManager>().Play("Comeco Torre");

        gameObject.GetComponent<AudioSource>().clip = torre.musicaTema;
        gameObject.GetComponent<AudioSource>().Play();

        //PLAYER
        // Prepara Stats Player
        vidaPlayer = time.poderT; // A vida do player é equivalente ao poder total do time

        //INIMIGO
        CarregaInimigo();

    }
    public void SelecionaTorre(Torre torre)
    {
        this.torre = torre;
    }

    public void ProximoAndar()
    {
        // Da experiencia para as cartas que participaram da batalha
        time.capitao.dataCarta.AdicionaXP(torre.listaTorre[andar].exp);
        time.membro1.dataCarta.AdicionaXP(torre.listaTorre[andar].exp);
        time.membro2.dataCarta.AdicionaXP(torre.listaTorre[andar].exp);
        time.membro3.dataCarta.AdicionaXP(torre.listaTorre[andar].exp);

        // Sobe um andar na torre
        andar++;

        // Boss fight
        if (andar == torre.listaTorre.Count - 1)
        {
            gameObject.GetComponent<AudioSource>().clip = torre.musicaBoss;
            gameObject.GetComponent<AudioSource>().Play();
        }

        // Completou todos os andares da torre
        if (andar >= torre.listaTorre.Count)
        {
            StartCoroutine(Vitoria());
            return;
        }

        // Carrega o novo inimigo
        CarregaInimigo();
    }

    public void CarregaInimigo()
    {
        // Prepara Stats do inimigo
        inimigo = torre.listaTorre[andar];
        vidaInimigo = inimigo.vida;

        // Prepara a UI do inimigo
        backgroundUI.sprite = inimigo.arte;
        nomeInimigoUI.text = inimigo.nome;

        AtualizaVidaUI(); // Atualiza a vida do Player e do inimigo
    }

    public void AtualizaVidaUI() // Essa funcao apenas atualiza a vida dos personagens da torre, ela deve ser chamada toda vez que um turno passar
    {
        vidaInimigoUI.text = "Vida: " + vidaInimigo.ToString() + "/" + inimigo.vida;
        vidaPlayerUI.text = "Vida: " + vidaPlayer.ToString() + "/" + time.poderT;
    }

    public void AplicaAcao(int APlayer, int Ainimigo) // Aplica as acoes feitas pelas dois entidades.
    {
        if (APlayer > 0)   // Player Efetuou um ataque
        {
            FindObjectOfType<AudioManager>().Stop("Cura Sound");
            FindObjectOfType<AudioManager>().Play("Ataque Sound");
            vidaInimigo -= APlayer;
        }
        else               // Player Efetuou uma cura, como o valor sera negativo voce subtrai ele da vida do player, assim faz uma soma 
        {
            FindObjectOfType<AudioManager>().Stop("Ataque Sound");
            FindObjectOfType<AudioManager>().Play("Cura Sound");
            vidaPlayer -= APlayer;
        }

        if (Ainimigo > 0)  // Inimigo Efetuou um ataque
            vidaPlayer -= Ainimigo;
        else               // Inimigo Efetuou uma cura
            vidaInimigo -= Ainimigo;

        if (vidaPlayer > time.poderT)
            vidaPlayer = time.poderT;

        AtualizaVidaUI();
    }

}
