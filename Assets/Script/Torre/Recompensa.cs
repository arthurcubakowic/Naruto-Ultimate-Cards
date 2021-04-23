/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;

//Comentarios feitos por: Nicolas Borges

[CreateAssetMenu(fileName = "Recompensa Torre ", menuName = "Torre/Recompensa")]
[System.Serializable]

//scriptable object que guarda os atributos de cada recompensa 
public class Recompensa : ScriptableObject
{
    public int[] currency = new int[6];

    public Card carta;
}
