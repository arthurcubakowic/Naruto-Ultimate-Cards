/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Comentarios feitos por Matheus Carvalho:

//Classe que gerencia as ações durante o combate na torre
public class CombateTorre : MonoBehaviour
{
    TorreManager torreManager;

    public int contadorCuras;

    private void Start()
    {
        contadorCuras = 0;
        torreManager = gameObject.GetComponent<TorreManager>();
    }

    //Ataca o inimigo e calcula o dano causado, além disso ele realiza uma ação
    public void AtaquePlayer()
    {
        int dano;
        dano = torreManager.time.ninjutsuAtkT + torreManager.time.taijutsuT + torreManager.time.genjutsuT;
        torreManager.AplicaAcao(dano, AcaoInimigo());
    }

    //Cura os membros da equipe e o inimigo realiza uma ação
    public void CuraPlayer()
    {
        if (contadorCuras < 10)
        {
            contadorCuras++;
            torreManager.AplicaAcao(-torreManager.time.ninjutsuSupT, AcaoInimigo());
        }
    }

    //Define se a ação do inimigo vai ser causar dano à equipe ou se curar
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
