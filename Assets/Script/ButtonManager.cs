using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    public void ButtonClick()
    {
        Debug.Log("Chamou a funcao ButtonClick");
        FindObjectOfType<AudioManager>().Play("Button Sound");
    }
}
