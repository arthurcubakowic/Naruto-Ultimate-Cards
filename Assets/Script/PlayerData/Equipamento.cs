using UnityEngine;

[CreateAssetMenu(fileName = "Novo Equipamento", menuName = "Equipamento")]
[System.Serializable]
public class Equipamento : ScriptableObject
{
    public string nome;
    public int idEquip;

    public Sprite arte;

    public int multiplicadorPoder = 1;
    public int upgradePoder = 0;

    public int multiplicadorTai = 1;
    public int upgradeTai = 0;

    public int multiplicadorGen = 1;
    public int upgradeGen = 0;

    public int multiplicadorNinAtk = 1;
    public int upgradeNinAtk = 0;

    public int multiplicadorNinSup = 1;
    public int upgradeNinSup = 0;

    public Equipamento()
    {
        nome = "Default";
        idEquip = 0;
        multiplicadorPoder = 1;
        upgradePoder = 0;
        multiplicadorTai = 1;
        upgradeTai = 0;
        multiplicadorGen = 1;
        upgradeGen = 0;
        multiplicadorNinAtk = 1;
        upgradeNinAtk = 0;
        multiplicadorNinSup = 1;
        upgradeNinSup = 0;
    }

}
