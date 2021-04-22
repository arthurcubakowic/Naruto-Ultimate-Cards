using UnityEngine.Audio;
using UnityEngine;

// baseado de um video do Brackeys
// Essa Classe serve para que uma lista de soms sejam guardados em Audio Manager
[System.Serializable]
public class Sound
{
    public string nome;

    public AudioClip clip;

    public float volume;
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
