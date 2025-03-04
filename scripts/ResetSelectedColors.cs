using UnityEngine;

public class ResetSelectedColors : MonoBehaviour
{
    public ColorVariant[] allColors;

    public void clearColorsSelected(){
        int currentSelected = PlayerPrefs.GetInt("ColorIdSelected", 0);

        foreach(var color in allColors){
            if(color.id != currentSelected){
                color.toggle.isOn = false;
                color.toggle.interactable = true;
            }    
        }
    }
}
