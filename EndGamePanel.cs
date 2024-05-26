using TMPro;
using UnityEngine;

public class EndGameP : MonoBehaviour
{
    public TMP_Text winnerText;

    public void turnOnPanel(string text){
        winnerText.text = text;
        gameObject.SetActive(true);
    }

    public void turnOffPanel(){
        gameObject.SetActive(false);
    }
}
