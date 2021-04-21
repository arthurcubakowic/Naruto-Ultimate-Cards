using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Torre ", menuName = "Torre/Torre")]
[System.Serializable]
public class Torre : ScriptableObject
{
    public string nome;
    public Sprite imagemTorre;

    public AudioClip musicaTema;
    public AudioClip musicaBoss;

    public List<EnemyCard> listaTorre;
    
    public Recompensa recompensa;


}