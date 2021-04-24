/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;

//Comentarios feitos por: Gustavo da Silva Oliveira

[CreateAssetMenu(fileName = "Novo Inimigo", menuName = "Inimigo")]
[System.Serializable]

//instancia os atributos do objeto da carta do inimigo
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
