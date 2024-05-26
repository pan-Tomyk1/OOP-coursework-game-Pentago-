using UnityEngine;
using UnityEngine.UI;
//скрипт, який я накинув на всі кружечки
public class ChangeCircleColor : MonoBehaviour
{
    Image image;//змінна, яка відповідає за картинку об'єкта
    //[HideInInspector] означає, що не видно в середовищі розробки
    [HideInInspector] public Button btn;//змінна, 
    //яка відповідає за дії з кнопкою
    //кількість ходів, які зробив користувач
    [HideInInspector] public static int numberOfMoves = 0;
    private CircleNumber circleNumber;

    private Color colorP1;//змінна, яка зберігає колір кружечків
    private Color colorP2;

    void Awake(){
        string[] colorsFromPP = PlayerPrefs
        .GetString("ColorsSelected", "000000,ffffff")
        .Split(',');

        Debug.Log("Colors from saves = " + PlayerPrefs.GetString("ColorsSelected", "000000,ffffff"));

        if (ColorUtility.TryParseHtmlString($"#{colorsFromPP[0]}", out colorP1))
        {
            Debug.Log("Success set p1 color from pp");
        }

        if (ColorUtility.TryParseHtmlString($"#{colorsFromPP[1]}", out colorP2))
        {
            Debug.Log("Success set p2 color from pp");
        }
        btn = gameObject.GetComponent<Button>();
    }

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        circleNumber =  gameObject.GetComponent<CircleNumber>();
        image.color = Color.gray;
        btn.onClick.AddListener(ChangeColor);//при кліку викликає функцію,
        //яка змінює колір кружечка на колір відповідного гравця
    }

    public void ChangeColor(){
        //Не дозволяє натискати на натиснуту кнопку
        if(circleNumber.playerMark!=0){return;}
        //залежно від гравця зафарбовую кружечок та встановлюю марку,
        // щоб користувач не міг на кружечок натиснути двічі
        if(numberOfMoves % 2 == 0){
            image.color = colorP1;
            circleNumber.setPMark(1);
        }
        else{
            image.color = colorP2;
            circleNumber.setPMark(2);
        }
        numberOfMoves++;//черга переходить до іншого гравця
        
    }
    
}
