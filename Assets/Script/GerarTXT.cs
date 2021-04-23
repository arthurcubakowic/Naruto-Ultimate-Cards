/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using System.IO;
using UnityEngine;

// Comentado por: Matheus Carvalho
class GerarTXT : MonoBehaviour
{

    // Gera o TXT Com o ID de todas as cartas
    private void Start()
    {

        DirectoryInfo di = new DirectoryInfo(Application.dataPath + @"/Resources/Cartas"); //caminho onde estão localizadas as cartas

        StreamWriter x;

        string CaminhoNome = Application.dataPath + "/Resources/Lista de Cartas.txt";   //caminho onde o arquivo será criado

        x = File.CreateText(CaminhoNome);

        int i = 0;

        //para cada carta presente no diretório das cartas, escreve no arquivo Lista de Cartas.txt o ID das cartas
        foreach (var fi in di.GetFiles())
        {
            if (i % 2 == 0)
            {
                string[] split = fi.Name.ToString().Split('.');
                x.Write(split[0]);

                if (i < di.GetFiles().Length - 3)
                {
                    x.Write(";");
                }
            }
            i++;
        }

        x.Close();

    }
}
