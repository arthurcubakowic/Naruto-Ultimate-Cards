using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


// Comentado por: Arthur von Peer Cubakowic
public class Inventory : MonoBehaviour
{
    public static Inventory instancia;                 // transforma em singleton

    public static InventoryData playerData;            // TRANSFORMAR ESSA VARIAVEL EM GLOBAL


    // Variaveis para metodos da Classe Inventory
    public static int retornoPorCartaRepetida = 50;

    // Variaveis de teste, DELETAR DEPOIS
    public int quantidadeCartas;
    public InventoryData playerDataDELETAR;

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

    private void Update()
    {
        // Salva o jogo com F5
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        // Carrega o jogo com F9
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }

        // Reseta o jogo inteiro com F1
        if (Input.GetKeyDown(KeyCode.F1))
        {
            playerData.inventarioCartas.Clear();

            for (int i = 0; i < 6; i++)
            {
                playerData.currency[i] = 0;
            }


            AddCarta(Resources.Load<Card>("Cartas/110"));
            AddCarta(Resources.Load<Card>("Cartas/210"));
            AddCarta(Resources.Load<Card>("Cartas/310"));
            AddCarta(Resources.Load<Card>("Cartas/2301"));

            SaveGame();

        }

    }


    public static void AddCarta(Card carta)
    {
        // Debugado para ajudar na criacao do jogo e garantir sua funcionalidade plena
        Debug.Log(playerData);
        Debug.Log(playerData.inventarioCartas[0]);
        Debug.Log(playerData.inventarioCartas.Count);

        // para cada carta repetida adiciona Currency ao jogador (nas primeiras versoes do jogo a currency chamava Ryo, agora é so Currency)
        foreach (InventoryDataCarta obj in playerData.inventarioCartas)
        {
            if (obj.carta == carta)
            {
                Inventory.ConverteCartaEmRyo(carta);
                return;
            }
        }

        // Instancia uma nova DataCarta, ou seja uma carta com o sistema de leveling
        InventoryDataCarta data = new InventoryDataCarta
        {
            carta = carta,
            lvl = 0,
            experiencia = 0
        };

        // Adiciona a carta ao inventario do jogador
        playerData.inventarioCartas.Add(data);
    }

    // Adiciona currency ao jogador para a carta repetida, vale citar que a currency recebida depende do Rank da carta repetida
    public static void ConverteCartaEmRyo (Card carta)
    {

        switch (carta.rank)
        {
            case "D":
                playerData.currency[1] += retornoPorCartaRepetida;
                break;
            case "C":
                playerData.currency[2] += retornoPorCartaRepetida;
                break;
            case "B":
                playerData.currency[3] += retornoPorCartaRepetida;
                break;
            case "A":
                playerData.currency[4] += retornoPorCartaRepetida;
                break;
            case "S":
                playerData.currency[5] += retornoPorCartaRepetida;
                break;
            case "SS":
                playerData.currency[4] += (300 / retornoPorCartaRepetida) * 300;
                break;
        }
    }

    // funcao global que chama o salva o jogo
    public static void SaveGame()
    {
        SaveSystem.SalvarInventario();

        Debug.Log("Jogo Salvo");
    }


    // funcao global que chama o carrega o jogo
    public static void LoadGame()
    {
        playerData = SaveSystem.CarregarInventario();

        Debug.Log("Jogo Carregado");
    }

    // Classe feita para o sistema de save
    public static class SaveSystem
    {
        // Funcao que transforma o inventario em Json e salva ele no arquivo SaveFile
        public static void SalvarInventario()
        {
            string json = JsonUtility.ToJson(playerData);

            File.WriteAllText(Application.dataPath + "/saveFile.json", json);
        }

        // funcao que le o Json com o save do jogador e transfere ele para o Data do jogo rodando
        // Se o sabe nao existir ele cria um save zerado, ou seja, com 4 cartas e sem currency 
        public static InventoryData CarregarInventario()
        {
            // carrega jogo
            if (File.Exists(Application.dataPath + "/saveFile.json"))
            {
                string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
                InventoryData data = JsonUtility.FromJson<InventoryData>(json);

                return data;
            }

            // cria um novo
            else
            {
                File.Create(Application.dataPath + "/saveFile.json");

                playerData = new InventoryData
                {
                    inventarioCartas = new List<InventoryDataCarta>()
                };

                for (int i = 0; i < 6; i++)
                {
                    playerData.currency[i] = 0;
                }

                InventoryDataCarta data = new InventoryDataCarta
                {
                    carta = Resources.Load<Card>("Cartas/110"),
                    lvl = 0,
                    experiencia = 0
                };

                playerData.inventarioCartas.Add(data);

                AddCarta(Resources.Load<Card>("Cartas/210"));
                AddCarta(Resources.Load<Card>("Cartas/310"));
                AddCarta(Resources.Load<Card>("Cartas/2301"));

                return playerData;
            }


        }
    }
}

// Classe que salva a currency do jogador, Ryo é inutil por enquanto, pois o sistema de equipamentos ainda nao foi implementado
[System.Serializable]
public class InventoryData
{
    public List<InventoryDataCarta> inventarioCartas;

    public int[] currency = new int [6]; // Para cartas D, usar [0] -> cartas C, usar [1] -> cartas B, usar [2] -> cartas A, usar [3] -> cartas S, usar [4] -> cartas SS, usar [5]
    public int ryo;
}

// Classe que salva o progresso de cada carta, referente ao leveling
[System.Serializable]
public class InventoryDataCarta : IComparable<InventoryDataCarta>
{
    public Card carta;
    public int lvl;
    public int experiencia;

    // essa parte serve para que essa classe possa fazer uma ordenacao por ID
    public int CompareTo(InventoryDataCarta other)
    {
        if (carta.idCard > other.carta.idCard)
            return 1;
        if (carta.idCard < other.carta.idCard)
            return -1;
        else
            return 0;
    }

    // Adiciona XP para a carta e evolui ela
    public void AdicionaXP(int exp)
    {
        experiencia += exp;

        // A experiancia é zerada ao evoluir pois houve um erro nas tentativas de fazer o XP excedente ser mantido
        if (experiencia >= 100 * Math.Pow(1.08f, lvl))
        {
            //experiencia -= (int) (100 * Math.Pow(1.08f, lvl));
            experiencia = 0;
            lvl++;
        }
    }
}