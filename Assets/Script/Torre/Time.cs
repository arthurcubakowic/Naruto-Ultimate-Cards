/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;

//Comentarios feitos por: Nicolas Borges

public class Time : MonoBehaviour
{
    //cria as variáveis dos membros da equipe
    public TimeMember capitao;
    public TimeMember membro1;
    public TimeMember membro2;
    public TimeMember membro3;

    public int i;

    //cria as variáveis do poder da equipe
    public int poderT;
    public int taijutsuT;
    public int genjutsuT;
    public int ninjutsuAtkT;
    public int ninjutsuSupT;

    private void Start()
    {
        i = 1;
    }

    //Cria a equipe 
    public void MontaTime()
    {
        //calcula o poder de cada membro da equipe
        CalculaAtributosMembro(capitao);
        CalculaAtributosMembro(membro1);
        CalculaAtributosMembro(membro2);
        CalculaAtributosMembro(membro3);

        /*EquipaMembro(capitao);
        EquipaMembro(membro1);
        EquipaMembro(membro2);
        EquipaMembro(membro3);*/

        //calcula o poder total da equipe, somando o poder de cada membro
        poderT       = (int) (capitao.poderRelativo + membro1.poderRelativo + membro2.poderRelativo + membro3.poderRelativo);
        taijutsuT    = (int) (capitao.taijutsuRelativo + membro1.taijutsuRelativo + membro2.taijutsuRelativo + membro3.taijutsuRelativo);
        genjutsuT    = (int) (capitao.genjutsuRelativo + membro1.genjutsuRelativo + membro2.genjutsuRelativo + membro3.genjutsuRelativo);
        ninjutsuAtkT = (int) (capitao.ninjutsuAtkRelativo + membro1.ninjutsuAtkRelativo + membro2.ninjutsuAtkRelativo + membro3.ninjutsuAtkRelativo);
        ninjutsuSupT = (int) (capitao.ninjutsuSupRelativo + membro1.ninjutsuSupRelativo + membro2.ninjutsuSupRelativo + membro3.ninjutsuSupRelativo);
    }

    //calcula o poder de cada membro, nas diferentes categorias 
    public void CalculaAtributosMembro(TimeMember membro)
    {
        membro.ninjutsuSupRelativo = membro.dataCarta.carta.cura + membro.dataCarta.carta.cura * membro.dataCarta.lvl / 25;
        membro.ninjutsuAtkRelativo = membro.dataCarta.carta.ninjutsu + membro.dataCarta.carta.ninjutsu * membro.dataCarta.lvl / 25;
        membro.genjutsuRelativo    = membro.dataCarta.carta.genjutsu    + membro.dataCarta.carta.genjutsu    * membro.dataCarta.lvl / 25;
        membro.taijutsuRelativo    = membro.dataCarta.carta.taijutsu    + membro.dataCarta.carta.taijutsu    * membro.dataCarta.lvl / 25;

        membro.poderRelativo = membro.ninjutsuSupRelativo + membro.ninjutsuAtkRelativo + membro.genjutsuRelativo + membro.taijutsuRelativo;

    }

    //atualiza do membro da equipe de acordo com o equipamento
    public void EquipaMembro(TimeMember membro)
    {

        membro.ninjutsuSupRelativo += membro.equip.upgradeNinSup;
        membro.ninjutsuSupRelativo *= membro.equip.multiplicadorNinSup;

        membro.ninjutsuAtkRelativo += membro.equip.upgradeNinAtk;
        membro.ninjutsuAtkRelativo *= membro.equip.multiplicadorNinAtk;

        membro.genjutsuRelativo    += membro.equip.upgradeGen;
        membro.genjutsuRelativo    *= membro.equip.multiplicadorGen;

        membro.taijutsuRelativo    += membro.equip.upgradeTai;
        membro.taijutsuRelativo    *= membro.equip.multiplicadorTai;

        membro.poderRelativo       += membro.equip.upgradePoder;
        membro.poderRelativo       *= membro.equip.multiplicadorPoder;

    }

    //define o capitão
    public void MontaCapitao(InventoryDataCarta dataCarta, Equipamento equip)
    {
        capitao.dataCarta = dataCarta;
        capitao.equip = equip;
    }


    //define os membros da equipe
    public void MontaMembro(InventoryDataCarta dataCarta, Equipamento equip)
    {
        switch (i)
        {
            case 1:
                membro1.dataCarta = dataCarta;
                membro1.equip = equip;
                break;
            case 2:
                membro2.dataCarta = dataCarta;
                membro2.equip = equip;
                break;
            case 3:
                membro3.dataCarta = dataCarta;
                membro3.equip = equip;
                break;
        }

        i++;

    }



}

[System.Serializable]

//instancia os atributos do membro do time
public class TimeMember
{
    public InventoryDataCarta dataCarta;

    public Equipamento equip;

    public float poderRelativo;
    public float taijutsuRelativo;
    public float genjutsuRelativo;
    public float ninjutsuAtkRelativo;
    public float ninjutsuSupRelativo;

}
