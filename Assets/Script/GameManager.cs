/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;
//Comentários feitos por: Matheus Carvalho
public class GameManager : MonoBehaviour
{
    public static GameObject inventario;        //referencia o objeto invntário

    private void Awake()
    {
        // Um GameObject com inventario é necessario para chamar as funcoes por teclas
        inventario = new GameObject
        {
            name = "Inventario"
        };

        inventario.AddComponent<Inventory>();           //adiciona o componente inventário ao gameobject
        Inventory.LoadGame();                           //carrega o jogo
        Inventory.playerData.inventarioCartas.Sort();   //ordena a lista de cartas do jogador

    }

    public static void AplicaRecompensa(Recompensa recompensa)          //adiciona currency e uma carta ao inventário do jogador
    {
        for (int i = 0; i < 6; i++)
        {
            Inventory.playerData.currency[i] += recompensa.currency[i];
        }

        Inventory.AddCarta(recompensa.carta);

        Debug.Log("Recompensa Aplicada");
    }
}
