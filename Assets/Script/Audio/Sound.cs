/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine.Audio;
using UnityEngine;

// Comentado por Gustavo da Silva Oliveira
// Essa Classe serve para que uma lista de sons sejam guardados em Audio Manager
[System.Serializable]
public class Sound
{
    // baseado de um video do Brackeys
    public string nome;

    public AudioClip clip;

    public float volume;
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
