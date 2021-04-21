using UnityEngine.Audio;
using UnityEngine;
using System;

// baseado de um video do Brackeys
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string nome)
    {
        Sound s = Array.Find(sounds, sound => sound.nome == nome);
        if (s == null)
        {
            Debug.LogWarning("Som " + nome + " não encontrado");
            return;
        }

        s.source.Play();
    }

    public void Stop(string nome)
    {
        Sound s = Array.Find(sounds, sound => sound.nome == nome);
        if (s == null)
        {
            Debug.LogWarning("Som " + nome + " não encontrado");
            return;
        }

        s.source.Stop();
    }
}
