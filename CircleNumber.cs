using UnityEngine;

public class CircleNumber : MonoBehaviour
{
    public int number;
    public int playerMark = 0;
    public delegate void onUserClicked();
    public static event onUserClicked OnUserPlaceMark;

    private ChangeCircleColor changeCircleColor;

    private void Awake() {
        changeCircleColor = gameObject.GetComponent<ChangeCircleColor>();
    }

    public void setPMark(int mark){
        playerMark = mark;
        OnUserPlaceMark?.Invoke();
    }
    public override string ToString()
    {
        return playerMark.ToString();
    }

    public void activateCircleNumber(){
        changeCircleColor.btn.interactable = true;
    }

    public void deActivateCircleNumber(){
        changeCircleColor.btn.interactable = false;
    }
}