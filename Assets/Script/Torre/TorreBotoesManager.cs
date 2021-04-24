/// <summary>
/// Projeto Final Programa��o Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulh�o, Nicolas Borges
/// Data: 22/04/2021
/// Vers�o do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;


//coment�rios feitos por: Gustavo da Silva Oliveira
public class TorreBotoesManager : MonoBehaviour
{
    private void Update()
    {
        // Visualiza entre as cartas obtidas, podendo selecionar as cartas � direita e � esquerda, a fim de selecionar o time
        if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.GetComponent<TorreManager>().inventarioManager.mostrandoInventario)
        {
            gameObject.GetComponent<TorreManager>().inventarioManager.ProximaCartaGeral();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.GetComponent<TorreManager>().inventarioManager.mostrandoInventario)
        {
            gameObject.GetComponent<TorreManager>().inventarioManager.CartaAnteriorGeral();
        }

        // Pode escolher entre atacar o advers�rio ou curar a equipe durante a torre
        if (Input.GetKeyDown(KeyCode.A) && gameObject.GetComponent<TorreManager>().torreEmAndamento)
        {
            gameObject.GetComponent<CombateTorre>().AtaquePlayer();
        }

        if (Input.GetKeyDown(KeyCode.C) && gameObject.GetComponent<TorreManager>().torreEmAndamento)
        {
            gameObject.GetComponent<CombateTorre>().CuraPlayer();

        }

        // Troca para a cena do menu caso clique no backspace
        if (Input.GetKeyDown(KeyCode.Backspace) && !gameObject.GetComponent<TorreManager>().torreEmAndamento)
        {
            MenuManager.LoadMenuScene();
        }
    }

    public void ProximaCartaTorre()
    {
        gameObject.GetComponent<TorreManager>().inventarioManager.ProximaCartaGeral();
    }

    public void CartaAnterioraTorre()
    {
        gameObject.GetComponent<TorreManager>().inventarioManager.CartaAnteriorGeral();
    }

    public void ProxTorre()
    {
        gameObject.GetComponent<TorreManager>().i++;

    }

    public void TorreAnteriora()
    {
        gameObject.GetComponent<TorreManager>().i--;

    }
}
