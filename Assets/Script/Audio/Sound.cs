using UnityEngine.Audio;
using UnityEngine;

// baseado de um video do Brackeys
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
