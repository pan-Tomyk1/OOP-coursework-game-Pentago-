using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorVariant : MonoBehaviour
{
    public Color player1Color;
    public Color player2Color;
    public int id;
    public Toggle toggle;

    public ResetSelectedColors resetSelectedColors;


    void Start()
    {
        //якщо id toogle-у співпадає зі збереженим ColorIdSelected, то цей toogle
        //активовується
        if(PlayerPrefs.GetInt("ColorIdSelected", 0) == id){
            toggle.isOn = true;
        }
    }

    public void saveColor(){
        if(!toggle.isOn)
            return;

        toggle.interactable = false;
        PlayerPrefs.SetInt("ColorIdSelected", id);
        Debug.Log($"Colors saved={player1Color.ToHexString()},{player2Color.ToHexString()}");
        PlayerPrefs.SetString("ColorsSelected", $"{player1Color.ToHexString()},{player2Color.ToHexString()}");
        resetSelectedColors.clearColorsSelected();
    }

}