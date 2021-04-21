using UnityEngine;

[CreateAssetMenu(fileName = "Nova Carta", menuName = "Carta")]
[System.Serializable]
public class Card : ScriptableObject
{
    public int idCard;

    public string rank;

    public string nomeCarta;
    public string descricao;
    public string tipo;

    public Sprite arte;

    public int poder;
    public int taijutsu;
    public int genjutsu;
    public int ninjutsu;
    public int cura;


}
