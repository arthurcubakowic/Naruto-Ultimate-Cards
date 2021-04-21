using UnityEngine;

[CreateAssetMenu(fileName = "Novo Inimigo", menuName = "Inimigo")]
[System.Serializable]
public class EnemyCard : ScriptableObject
{
    public string nome;
    public string descricao;
    public int exp;

    public Sprite arte;

    public int dano;
    public int vida;

    public int cura;

    public float multNin;
    public float multGen;
    public float multTai;

    public int probabilidadeHeal;
}
