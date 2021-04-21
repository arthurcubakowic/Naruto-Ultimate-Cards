using System.IO;
using UnityEngine;

class GerarTXT : MonoBehaviour
{

    // Gera o TXT Com o ID de todas as cartas
    private void Start()
    {

        DirectoryInfo di = new DirectoryInfo(Application.dataPath + @"/Resources/Cartas");

        StreamWriter x;

        string CaminhoNome = Application.dataPath + "/Resources/Lista de Cartas.txt";

        x = File.CreateText(CaminhoNome);

        int i = 0;
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
