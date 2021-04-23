/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;

//Comentarios feitos por: Matheus Carvalho

[CreateAssetMenu(fileName = "Nova Carta", menuName = "Carta")]
[System.Serializable]

//scriptable object que guarda os atributos de cada carta
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
