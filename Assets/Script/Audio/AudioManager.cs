using UnityEngine.Audio;
using UnityEngine;
using System;

// Comentado por Nicolas Borges
public class AudioManager : MonoBehaviour
{
    // baseado de um video do Brackeys
    public static AudioManager instance;

    public Sound[] sounds;

    void Awake()
    {
        if (instance == null) // singleton
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // faz com que o AudioManager seja passado para todas as Cenas
        DontDestroyOnLoad(gameObject);

        // Adiciona o Source no gameObject para cada som guardado no gameObject
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Toca qualquer som com o nome indicado
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

    // Unica coisa de nossa autoria, pausa qualquer som que esteja tocando
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
