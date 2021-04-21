using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card carta;
    public InventoryDataCarta dataCarta;

    public int idCarta;

    public Image arte;

    public Text nome;
    public Text rank;
    public Text tipo;
    public Text discricao;
    public Text poder;
    public Text taijutsu;
    public Text genjutsu;
    public Text ninjutsu;
    public Text cura;

    public void LoadDataCarta()
    {
        nome.text = dataCarta.carta.nomeCarta;
        tipo.text = dataCarta.carta.tipo;
        discricao.text = dataCarta.carta.descricao;
        rank.text = dataCarta.carta.rank;
        idCarta = dataCarta.carta.idCard;

        arte.sprite = dataCarta.carta.arte;

        taijutsu.text    = (dataCarta.carta.taijutsu + dataCarta.carta.taijutsu * dataCarta.lvl / 25).ToString();
        genjutsu.text    = (dataCarta.carta.genjutsu + dataCarta.carta.genjutsu * dataCarta.lvl / 25).ToString();
        ninjutsu.text    = (dataCarta.carta.ninjutsu + dataCarta.carta.ninjutsu * dataCarta.lvl / 25).ToString();
        cura.text        = (dataCarta.carta.cura     + dataCarta.carta.cura     * dataCarta.lvl / 25).ToString();

        int poderDisplay = dataCarta.carta.taijutsu + dataCarta.carta.genjutsu + dataCarta.carta.ninjutsu + dataCarta.carta.cura;
        poderDisplay += poderDisplay * dataCarta.lvl / 25;
        poder.text = poderDisplay.ToString();


        carta.poder = carta.taijutsu + carta.genjutsu + carta.ninjutsu + carta.cura;


    }

    public void LoadCartaGacha()
    {
        nome.text = carta.nomeCarta;
        tipo.text = carta.tipo;
        discricao.text = carta.descricao;
        rank.text = carta.rank;
        idCarta = carta.idCard;

        carta.poder = carta.taijutsu + carta.genjutsu + carta.ninjutsu + carta.cura;
        poder.text = carta.poder.ToString();

        arte.sprite = carta.arte;

    }

}
