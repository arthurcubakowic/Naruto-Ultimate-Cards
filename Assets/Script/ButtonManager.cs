/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>
 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//Comentários feitos por: Gustavo da Silva Oliveira
public class ButtonManager : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);        //sempre que o gameobject com essa classe for clicado, ele soltará um som 
    }

    public void ButtonClick()
    {
        Debug.Log("Chamou a funcao ButtonClick");
        FindObjectOfType<AudioManager>().Play("Button Sound");      //toca o som
    }
}
