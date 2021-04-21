using UnityEngine;

[CreateAssetMenu(fileName = "Recompensa Torre ", menuName = "Torre/Recompensa")]
[System.Serializable]
public class Recompensa : ScriptableObject
{
    public int[] currency = new int[6];

    public Card carta;
}
