using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory instancia; // transforma em singleton

    public static InventoryData playerData;            // TRANSFORMAR ESSA VARIAVEL EM GLOBAL


    // Variaveis para metodos da Classe Inventory
    public static int retornoPorCartaRepetida = 50;

    // Variaveis de teste, DELETAR DEPOIS
    public int quantidadeCartas;
    public InventoryData playerDataDELETAR;

    private void Awake()
    {
        instancia = this; // transforma em singleton

    }

    private void Update()
    {
        // DELETAR
        //quantidadeCartas = playerData.inventarioCartas.Count;
        //playerDataDELETAR = playerData;

        // DELETAR

        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }

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
        Debug.Log(playerData);
        Debug.Log(playerData.inventarioCartas[0]);
        Debug.Log(playerData.inventarioCartas.Count);
        foreach (InventoryDataCarta obj in playerData.inventarioCartas)
        {
            if (obj.carta == carta)
            {
                Inventory.ConverteCartaEmRyo(carta);
                return;
            }
        }

        InventoryDataCarta data = new InventoryDataCarta
        {
            carta = carta,
            lvl = 0,
            experiencia = 0
        };

        playerData.inventarioCartas.Add(data);
    }

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

    public static void SaveGame()
    {
        SaveSystem.SalvarInventario();

        Debug.Log("Jogo Salvo");
    }

    public static void LoadGame()
    {
        playerData = SaveSystem.CarregarInventario();

        Debug.Log("Jogo Carregado");
    }

    public static class SaveSystem
    {

        public static void SalvarInventario()
        {

            string json = JsonUtility.ToJson(playerData);

            File.WriteAllText(Application.dataPath + "/saveFile.json", json);
        }

        public static InventoryData CarregarInventario()
        {

            if (File.Exists(Application.dataPath + "/saveFile.json"))
            {
                string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
                InventoryData data = JsonUtility.FromJson<InventoryData>(json);

                return data;
            }
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
                
                //Debug.LogError("Save Criado");

                return playerData;
            }


        }
    }
}

[System.Serializable]
public class InventoryData
{
    public List<InventoryDataCarta> inventarioCartas;

    public int[] currency = new int [6]; // Para cartas D, usar [0] -> cartas C, usar [1] -> cartas B, usar [2] -> cartas A, usar [3] -> cartas S, usar [4] -> cartas SS, usar [5]
    public int ryo;
}

[System.Serializable]
public class InventoryDataCarta : IComparable<InventoryDataCarta>
{
    public Card carta;
    public int lvl;
    public int experiencia;

    public int CompareTo(InventoryDataCarta other)
    {
        if (carta.idCard > other.carta.idCard)
            return 1;
        if (carta.idCard < other.carta.idCard)
            return -1;
        else
            return 0;
    }

    public void AdicionaXP(int exp)
    {
        experiencia += exp;

        if (experiencia >= 100 * Math.Pow(1.08f, lvl))
        {
            //experiencia -= (int) (100 * Math.Pow(1.08f, lvl));
            experiencia = 0;
            lvl++;
        }
    }
}