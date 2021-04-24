/// <summary>
/// Projeto Final Programação Baseada em Componentes para Jogos
/// Prof Dr. Mario Minami
/// Alunos: Arthur Cubakowic, Gustavo da Silva, Matheus Mergulhão, Nicolas Borges
/// Data: 22/04/2021
/// Versão do Unity: 2020.3.0f1
/// </summary>

using UnityEngine;
using UnityEngine.UI;

//comentários feitos por: Nicolas Borges

public class CardDisplay : MonoBehaviour
{
    //instancia os atributos do display da carta
    public Card carta;
    public InventoryDataCarta dataCarta;

    public int idCarta;

    public Image arte;

    public Text nome;
    public Text rank;
    public Text tipo;
    public Text descricao;
    public Text poder;
    public Text taijutsu;
    public Text genjutsu;
    public Text ninjutsu;
    public Text cura;

    //serve para atualizar a UI da datacarta na cena do inventário e da torre, assim os objetos instanciados recebem os atributos do scriptable object
    public void LoadDataCarta()
    {
        nome.text = dataCarta.carta.nomeCarta;
        tipo.text = dataCarta.carta.tipo;
        descricao.text = dataCarta.carta.descricao;
        rank.text = dataCarta.carta.rank;
        idCarta = dataCarta.carta.idCard;

        arte.sprite = dataCarta.carta.arte;

        //gera os atributos da datacarta e passa o valor para a UI
        taijutsu.text    = (dataCarta.carta.taijutsu + dataCarta.carta.taijutsu * dataCarta.lvl / 25).ToString();
        genjutsu.text    = (dataCarta.carta.genjutsu + dataCarta.carta.genjutsu * dataCarta.lvl / 25).ToString();
        ninjutsu.text    = (dataCarta.carta.ninjutsu + dataCarta.carta.ninjutsu * dataCarta.lvl / 25).ToString();
        cura.text        = (dataCarta.carta.cura     + dataCarta.carta.cura     * dataCarta.lvl / 25).ToString();

        //gera o poder total da datacarta e passa o valor para a UI
        int poderDisplay = dataCarta.carta.taijutsu + dataCarta.carta.genjutsu + dataCarta.carta.ninjutsu + dataCarta.carta.cura;
        poderDisplay += poderDisplay * dataCarta.lvl / 25;
        poder.text = poderDisplay.ToString();


        carta.poder = carta.taijutsu + carta.genjutsu + carta.ninjutsu + carta.cura;        //gera o poder da carta


    }

    //serve para atualizar a UI da carta na cena do Gacha, assim os objetos instanciados recebem os atributos do scriptable object
    public void LoadCartaGacha()
    {
        nome.text = carta.nomeCarta;
        tipo.text = carta.tipo;
        descricao.text = carta.descricao;
        rank.text = carta.rank;
        idCarta = carta.idCard;

        carta.poder = carta.taijutsu + carta.genjutsu + carta.ninjutsu + carta.cura;
        poder.text = carta.poder.ToString();

        arte.sprite = carta.arte;

    }

}
