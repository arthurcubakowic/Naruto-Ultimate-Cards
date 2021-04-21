using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject inventario;

    private void Awake()
    {

        inventario = new GameObject // Um GameObject com inventario é necessario para chamar as funcoes por teclas
        {
            name = "Inventario"
        };

        inventario.AddComponent<Inventory>();
        Inventory.LoadGame();
        Inventory.playerData.inventarioCartas.Sort();

    }

    public static void AplicaRecompensa(Recompensa recompensa)
    {
        for (int i = 0; i < 6; i++)
        {
            Inventory.playerData.currency[i] += recompensa.currency[i];
        }

        Inventory.AddCarta(recompensa.carta);

        Debug.Log("Recompensa Aplicada");
    }
}
