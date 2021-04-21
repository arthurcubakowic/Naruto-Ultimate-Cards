using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateTorre : MonoBehaviour
{
    TorreManager torreManager;

    private void Start()
    {
        torreManager = gameObject.GetComponent<TorreManager>();
    }

    public void AtaquePlayer()
    {
        int dano;
        dano = torreManager.time.ninjutsuAtkT + torreManager.time.taijutsuT + torreManager.time.genjutsuT;
        torreManager.AplicaAcao(dano, AcaoInimigo());
    }

    public void CuraPlayer()
    {
        torreManager.AplicaAcao(-torreManager.time.ninjutsuSupT, AcaoInimigo());
    }

    public int AcaoInimigo()
    {
        int Ainimigo;
        if (Random.Range(0, 100) <= torreManager.inimigo.probabilidadeHeal)
        {
            Ainimigo = torreManager.inimigo.cura;
        }
        else
        {
            Ainimigo = torreManager.inimigo.dano;
        }
        return Ainimigo;
    }
}
