/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Comentarios feitos por: Matheus Carvalho

[CreateAssetMenu(fileName = "Torre ", menuName = "Torre/Torre")]
[System.Serializable]

//scriptable object que guarda os atributos de cada torre
public class Torre : ScriptableObject
{
    public string nome;
    public Sprite imagemTorre;

    public AudioClip musicaTema;
    public AudioClip musicaBoss;

    public List<EnemyCard> listaTorre;
    
    public Recompensa recompensa;


}