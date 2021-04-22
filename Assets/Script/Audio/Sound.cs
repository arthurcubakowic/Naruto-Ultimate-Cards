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
