using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Bugs conhecidos: na formacao de time é possivel escolher a mesma carta 4 vezes, o ideal seria escolher apenas 1 de cada carta
// Comentado por: Artur von Peer Cubakowic
public class TorreManager : MonoBehaviour
{
    [Header("Geral")]
    // Instancias referentes à torre
    public List<Torre> listaTorres;
    public Torre torre;
    public int andar;
    // Instancias referentes ao jogador
    public Time time;
    public InventarioCenaManager inventarioManager;
    // Instancias referente à musica de fundo
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
    public GameObject torreSelectBotoes;
    public GameObject cartaSelectBotoes;
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
    public KeyCode enterButton = KeyCode.C;

    private void Start()
    {
        // Garante que a musica comece tocando
        gameObject.GetComponent<AudioSource>().clip = musicaThema;
        gameObject.GetComponent<AudioSource>().Play();

        // variaveis para travar as teclas
        torreEmAndamento = false;
        torreSelectEmAndamento = true;

        // Garante que a UI de jogador nao apareca na tela até o inicio da torre
        inimigoUI.SetActive(false);
        playerUI.SetActive(false);

        // Comeca a rotina de selecao de torre
        StartCoroutine(SelecionaTorre());

    }

    private void Update()
    {
        // se o jogador perdeu toda a vida e a torre ja comecou ele morreu, em seguida chama a rotina de GameOver
        if (vidaPlayer <= 0 && torreEmAndamento && !(andar == torre.listaTorre.Count))
        {
            torreEmAndamento = false;
            StartCoroutine(GameOver());
        }

        // se o inimigo perdeu toda a vida ele é derrotado, e voce avanca para o proximo andar da torre
        if (vidaInimigo <= 0 && torreEmAndamento && !(andar == torre.listaTorre.Count))
        {
            FindObjectOfType<AudioManager>().Stop("Ataque Sound"); // Para o som do ataque final para dar lugar ao som de inimigo derrotado
            FindObjectOfType<AudioManager>().Play("Inimigo Derrotado");
            ProximoAndar();
        }

    }

    // Rotina de selecao de torre
    public IEnumerator SelecionaTorre()
    {
        i = 0;  // Garante que o a lista de torres comece da primeira torre
        textoMensagem.text = "Aperte C para confirmar sua escolha"; // muda o texto visivel para o jogador

        // inicia a UI de selecao de torre
        torre = listaTorres[i];
        torreImage.sprite = listaTorres[i].imagemTorre;
        torreNome.text = listaTorres[i].nome;

        // While que prende o jogador na selecao de torre até que uma torre seja selecionada, por isso esta funcao é uma rotina
        while (torreSelectEmAndamento)
        {
            // use a seta da direita ou da esquerda para selecionar a torre que deseja jogar
            if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.GetComponent<TorreManager>().torreSelectEmAndamento)
            {
                i++;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.GetComponent<TorreManager>().torreSelectEmAndamento)
            {
                i--;
            }

            if (i < 0) i = listaTorres.Count - 1;
            if (i >= listaTorres.Count) i = 0;

            // use o Enter para selecionar a torre que deseja jogar
            if (Input.GetKeyDown(enterButton) && gameObject.GetComponent<TorreManager>().torreSelectEmAndamento)
            {
                gameObject.GetComponent<TorreManager>().torreSelectEmAndamento = false;
                FindObjectOfType<AudioManager>().Play("Select Carta");
            }


            // sempre atualiza a sprite quando o i é alterado independente da quantidade de torres que existam no jogo
            torreImage.sprite = listaTorres[i].imagemTorre;
            torreNome.text = listaTorres[i].nome;

            yield return null;
        }


        textoMensagem.text = "Torre Selecionada!"; // muda o texto visivel para o jogador


        torre = listaTorres[i];                    // atribui que a torre iniciada será a escolhida pelo jogador

        yield return new WaitForSecondsRealtime(1);// espera um segundo para o jogador poder ler que a torre foi selecionada

        torreSelect.SetActive(false);              // desativa a UI de selecao de torre
        Destroy(torreSelectBotoes);                // Destroy os botoes de troca de torre
        cartaSelectBotoes.SetActive(true);         // Ativa os botoes de troca de carta

        StartCoroutine(FormarTime());              // inicia a rotina de formacao de time
    }

    // Rotina de formacao de time
    public IEnumerator FormarTime()
    {
        int i = 1;                  // variavel para contar quantos membros do time foram escolgidos
        bool recebeuCarta = false;  // variavel para travar o jogador na selecao de carta

        // instancia a UI de cartas, para que as cartas fiquem visiveis para o jogador escolhe-las
        gameObject.GetComponent<TorreManager>().inventarioManager.InstanciaCarta(Inventory.playerData.inventarioCartas[gameObject.GetComponent<TorreManager>().inventarioManager.i]);
        gameObject.GetComponent<TorreManager>().inventarioManager.mostrandoInventario = true;


        // Enquanto o jogador nao escolher uma carta ele fica preso na selecao de capitao
        while (!recebeuCarta)
        {
            textoMensagem.text = "Escolha o capitao da sua equipe"; // muda o texto visivel para o jogador

            // quando o jogador apertar Enter a carta que instanciada é selecionada para ser o capitao
            if (Input.GetKeyDown(enterButton))
            {
                time.MontaCapitao(Inventory.playerData.inventarioCartas[inventarioManager.i], ScriptableObject.CreateInstance<Equipamento>());
                recebeuCarta = true; // ao colocar que recebeu carta como true o jogador pode sair do while

                FindObjectOfType<AudioManager>().Play("Select Carta");
            }
            yield return null;
        }
        recebeuCarta = false; // agora é necessario colocar recebeu carta como falso para o jogador ficar novamente preso na selecao

        // Enquanto o jogador nao escolher uma carta ele fica preso na selecao de membros
        while (!recebeuCarta)
        {
            textoMensagem.text = "Escolha o membro " + i + " da sua equipe"; // muda o texto visivel para o jogador

            // Sempre que apertar Enter manda uma carta para o formador de membro, até que sejam mandados os ultimos 3 membros da equipe
            if (Input.GetKeyDown(enterButton))
            {
                if (Inventory.playerData.inventarioCartas[inventarioManager.i] == time.capitao.dataCarta || Inventory.playerData.inventarioCartas[inventarioManager.i] == time.membro1.dataCarta || Inventory.playerData.inventarioCartas[inventarioManager.i] == time.membro2.dataCarta)
                {
                    textoMensagem.text = "Esta carta já foi escolhida";
                    yield return new WaitForSecondsRealtime(0.5f);
                }
                else
                {
                    time.MontaMembro(Inventory.playerData.inventarioCartas[inventarioManager.i], ScriptableObject.CreateInstance<Equipamento>());
                    i++;

                    FindObjectOfType<AudioManager>().Play("Select Carta");
                }
            }

            // todos os membros do time instanciados entao pode sair do While
            if (i >= 4)
            {
                recebeuCarta = true;
            }
            yield return null;
        }

        textoMensagem.text = "Time Formado!"; // muda o texto visivel para o jogador
        yield return new WaitForSecondsRealtime(1); // espera um segundo para o jogador poder ler a mensagem de time formado
        caixaMensagem.SetActive(false); // caixa de mensagem nao sera mais necessaria por isso ela é desativada

        // Com essa atribuicao de false, as setas para direita e esquerda nao instanciam mais novas cartas
        gameObject.GetComponent<TorreManager>().inventarioManager.mostrandoInventario = false;

        // Destroy a ultima carta instanciada
        Destroy(GameObject.Find("Carta"));

        // Chama a funcao que efetivamente formará o time
        time.MontaTime();

        // Inicia de fato a torre
        IniciaTorre();

        Destroy(cartaSelectBotoes);

        yield break;
    }

    // Comeca a torre de fato
    public void IniciaTorre()
    {

        torreEmAndamento = true;    // define que o jogador já está jogando a torre
        andar = 0;                  // define que o jogador está no primeiro andar da torre
        inimigoUI.SetActive(true);  // ativa a UI do inimigo
        playerUI.SetActive(true);   // ativa a UI do jogador

        // inicia um som referente ao inicio da torre
        FindObjectOfType<AudioManager>().Play("Comeco Torre");

        // inicia a musica tema da torre (Salvo no Scriptable Object da torre e não em um Game Object)
        gameObject.GetComponent<AudioSource>().clip = torre.musicaTema;
        gameObject.GetComponent<AudioSource>().Play();

        // PLAYER
        // Prepara Stats Player
        vidaPlayer = time.poderT; // A vida do player é equivalente ao poder total do time

        // INIMIGO
        // Carrega os Stats do Inimigo
        CarregaInimigo();

    }

    // Termina a torre onde o jogador perdeu
    public IEnumerator GameOver()
    {
        Debug.Log("Voce Perdeu!"); // Debugado para ajudar na criacao do codigo

        // para a musica tema e comeca a musica de derrota
        gameObject.GetComponent<AudioSource>().Stop();
        FindObjectOfType<AudioManager>().Play("Lose Theme");

        // ativa a caixa de mensagem novamente e coloca uma mensagem de derrota, tambem coloca a caixa no centro da tela
        caixaMensagem.SetActive(true);
        caixaMensagem.transform.position = GameObject.Find("Canvas").transform.position;
        textoMensagem.text = "Voce Perdeu! Volte Tudo";
        
        // define que a torre parou
        torreEmAndamento = false;

        // Pausa o jogo até que o jogador clique alguma tecla, assim ele pode ouvir Sadness and Sorrow
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

        // volta o jogador para o menu
        MenuManager.LoadMenuScene();
    }
   
    // Termina a torre e atribui recompensas ao jogador
    public IEnumerator Vitoria()
    {
        Debug.Log("Parabens Voce Completou a Torre"); // Debugado para ajudar na criacao do codigo

        // para a musica tema e comeca a musica de vitoria
        gameObject.GetComponent<AudioSource>().Stop();
        FindObjectOfType<AudioManager>().Play("Vitoria Theme");

        // ativa a caixa de mensagem novamente e coloca uma mensagem de derrota, tambem coloca a caixa no centro da tela
        caixaMensagem.SetActive(true);
        caixaMensagem.transform.position = GameObject.Find("Canvas").transform.position;
        textoMensagem.text = "Parabens, Voce Completou a Torre!";

        // define que a torre parou
        torreEmAndamento = false;

        // Recompensa o jogador por ter terminado a torre
        RecompensaJogador();

        // Pausa o jogo até que o jogador clique alguma tecla, assim ele pode ouvir Victory Fanfare
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

        // volta o jogador para o menu
        MenuManager.LoadMenuScene();
    }

    // Recompensa o jogador por ter terminado a torre, essa funcao é unutil na forma atual do codigo, porem caso o sistema de recompensa mude 
    // ou algo do genero, essa funcao será o intermediario
    public void RecompensaJogador()
    {
        GameManager.AplicaRecompensa(torre.recompensa);
    }

    // Passa o jogador para o proximo andar da torre
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

    // Carrega os Stats do inimigo
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

    // Atualiza do jogador e a do Inimigo ao final do turno
    public void AtualizaVidaUI() // Essa funcao apenas atualiza a vida dos personagens da torre, ela deve ser chamada toda vez que um turno passar
    {
        vidaInimigoUI.text = "Vida: " + vidaInimigo.ToString() + "/" + inimigo.vida;
        vidaPlayerUI.text = "Vida: " + vidaPlayer.ToString() + "/" + time.poderT;
    }

    // Aplica uma acao, essa funcao aplica tanto ataque quanto cura
    public void AplicaAcao(int APlayer, int Ainimigo) // Aplica as acoes feitas pelas dois entidades.
    {
        if (APlayer > 0)   // Player Efetuou um ataque
        {
            // Toca o som de ataque, se estiver rolando um som de ataque ou cura ele para, o de ataque para sem o Stop, pois é o mesmo som que sera novamente chamado
            FindObjectOfType<AudioManager>().Stop("Cura Sound");
            FindObjectOfType<AudioManager>().Play("Ataque Sound");

            // Efetua o ataque
            vidaInimigo -= APlayer;
        }
        else               // Player Efetuou uma cura, como o valor sera negativo voce subtrai ele da vida do player, assim faz uma soma 
        {
            // Toca o som de cura, se estiver rolando um som de ataque ou cura ele para, o de cura para sem o Stop, pois é o mesmo som que sera novamente chamado
            FindObjectOfType<AudioManager>().Stop("Ataque Sound");
            FindObjectOfType<AudioManager>().Play("Cura Sound");

            // Efetua a cura
            vidaPlayer -= APlayer;
        }

        if (Ainimigo > 0)  // Inimigo Efetuou um ataque
            vidaPlayer -= Ainimigo;
        else               // Inimigo Efetuou uma cura
            vidaInimigo -= Ainimigo;

        // impede que a cura seja maior que a vida maxima do jogador
        if (vidaPlayer > time.poderT)
            vidaPlayer = time.poderT;

        // Atualiza a vida do jogador e do inimigo
        AtualizaVidaUI();
    }

}
