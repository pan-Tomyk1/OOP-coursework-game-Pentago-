using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform upperLeftField;
    public Transform upperRightField;
    public Transform bottomLeftField;
    public Transform bottomRightField;

    public TMP_Text nowPlayer;

    public EndGameP endPanel;
    //масив, в який я закидую кружечки з юніті
    public CircleNumber[] circleNumbers;
    public Button[] rotateButtons;
    private const int arrayDemension=6; 
    private const int smoothingAngle=3;
    private const int rotationAngle=90;
    private int freeSpaces=36;
    private const float delay=0.01f;
    //мій двовимірний масив
    private CircleNumber[,] gameField = new CircleNumber[arrayDemension,arrayDemension];

    public GameSaver gameSaver;
    private void Awake() {
        CircleNumber.OnUserPlaceMark += Print2DArray;
        CircleNumber.OnUserPlaceMark += turnOnRotateButtons;

        int index = 0;
        for(int i = 0; i < arrayDemension; i++){
            for(int j = 0; j < arrayDemension; j++){
                Debug.Log(index);
                gameField[i,j] = circleNumbers[index];
                index++;
            }
        }
    }

    private void Start() {
        turnOffRotateButtons();
    }

private void turnOnRotateButtons(){
    foreach(Button btn in rotateButtons){
        btn.interactable = true;
    }

    foreach(var circle in circleNumbers){
        circle.deActivateCircleNumber();
    }
}

private void turnOffRotateButtons(){
    foreach(Button btn in rotateButtons){
        btn.interactable = false;
    }

    foreach(var circle in circleNumbers){
        circle.activateCircleNumber();
    }
}

    private void Print2DArray(){
        StringBuilder sb = new();

        for(int i=0; i<arrayDemension;i++){
            for(int j=0; j<arrayDemension;j++){
                sb.Append(gameField[i,j].ToString()).Append("\t");
            }
            Debug.Log(sb);
            sb.Clear();
        }
    }

       private void RotateSection(int startX, int startY, int size, bool clockwise)
    {
        CircleNumber[,] temp = new CircleNumber[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                temp[i, j] = gameField[startX + i, startY + j];
            }
        }

        if (clockwise)
        {
            for (int i = 0; i < size / 2; i++)
            {
                for (int j = i; j < size - i - 1; j++)
                {
                    CircleNumber tempValue = temp[i, j];
                    temp[i, j] = temp[size - 1 - j, i];
                    temp[size - 1 - j, i] = temp[size - 1 - i, size - 1 - j];
                    temp[size - 1 - i, size - 1 - j] = temp[j, size - 1 - i];
                    temp[j, size - 1 - i] = tempValue;
                }
            }
        }
        else
        {
            for (int i = 0; i < size / 2; i++)
            {
                for (int j = i; j < size - i - 1; j++)
                {
                    CircleNumber tempValue = temp[i, j];
                    temp[i, j] = temp[j, size - 1 - i];
                    temp[j, size - 1 - i] = temp[size - 1 - i, size - 1 - j];
                    temp[size - 1 - i, size - 1 - j] = temp[size - 1 - j, i];
                    temp[size - 1 - j, i] = tempValue;
                }
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                gameField[startX + i, startY + j] = temp[i, j];
            }
        }
        freeSpaces--;
        Debug.Log(freeSpaces);
    }

    public void RotateUpperRight(bool clockwise)
    {
        RotateSection(0, 3, 3, clockwise);
        rotate(upperRightField, clockwise);
        DisplayWinner();
        turnOffRotateButtons();
        setPlayerNow();
    }

    public void RotateUpperLeft(bool clockwise)
    {
        RotateSection(0, 0, 3, clockwise);
        rotate(upperLeftField, clockwise);
        DisplayWinner();
        turnOffRotateButtons();
        setPlayerNow();
    }

    public void RotateLowerRight(bool clockwise)
    {
        RotateSection(3, 3, 3, clockwise);
        rotate(bottomRightField, clockwise);
        DisplayWinner();
        turnOffRotateButtons();
        setPlayerNow();
    }

    public void RotateLowerLeft(bool clockwise)
    {
        RotateSection(3, 0, 3, clockwise);
        rotate(bottomLeftField, clockwise);
        DisplayWinner();
        turnOffRotateButtons();
        setPlayerNow();
    }
    
    private void setPlayerNow(){
        if(ChangeCircleColor.numberOfMoves % 2 == 0)
            nowPlayer.text = "Player 1";
        else
            nowPlayer.text = "Player 2";
    }
    
    private void DisplayWinner(){
        if(freeSpaces<=0)endPanel.turnOnPanel("Game played in a draw");
        switch(CheckWinning(gameField)){
            case 0:
            Debug.Log("Game continues"); 
            break;
            case 1:
            endPanel.turnOnPanel("First player won");
            gameSaver.saveGame(gameField,"First player won");
            break;
            case 2:
            endPanel.turnOnPanel("Second player won");
            gameSaver.saveGame(gameField,"Second player won");
            break;
            case 3 :
            endPanel.turnOnPanel("The game is played in a draw");
            gameSaver.saveGame(gameField,"The game is played in a draw");
            break;
        }

    }
    private int CheckWinning(CircleNumber[,] array){
       
            CircleNumber[] tempArray = new CircleNumber[arrayDemension];
            for (int i = 0; i <arrayDemension; i++)
            {//перевірка горизонтальних рядків
                for(int l=0; l<arrayDemension;l++){
                    tempArray[l] = gameField[i,l];
                }
                switch (CheckLine(tempArray))
                {
                    case 1: return 1;
                    case 2: return 2;
                }
                
                for (int j = 0; j <arrayDemension; j++)
                {
                    tempArray[j] = gameField[j,i];
                }
                //перевірка вертикальних рядків
                switch (CheckLine(tempArray))
                {
                    case 1: return 1;
                    case 2: return 2;
                }

                MakeDiagonalArray(tempArray, i);
                //перевірка діагональних рядків
                switch (CheckLine(tempArray))
                {
                    case 1: return 1;
                    case 2: return 2;
                }
            }
            if (CheckDraw())
            {
                return 3;
            }
            return 0;                                                                                                                               
    }
    private bool CheckDraw(){
        CircleNumber[] tempArray = new CircleNumber[arrayDemension];
        for (int i = 0; i < arrayDemension; i++)
            {
                for(int l=0; l<arrayDemension;l++){
                    tempArray[l] = gameField[i,l];
                }
                if (Array.Exists(tempArray, element => element.playerMark == 0))
                {
                    return false;
                }
            }
            return true;
    }
    private  void MakeDiagonalArray(CircleNumber[] line, int index)
        {//метод робить лінійні масиви з всіх потрібних діагоналей 2Д масиву
            switch (index)
            {
                case 0:
                    for (var i = 0; i < arrayDemension; i++)
                    {
                        line[i] =gameField[i,i];
                    }

                    break;
                case 1:
                    for (var i = 0; i < arrayDemension- 1; i++)
                    {
                        line[i] = gameField[i,i + 1];
                    }

                    break;
                case 2:
                    for (var i = 0; i < arrayDemension - 1; i++)
                    {
                        line[i] = gameField[i + 1,i];
                    }

                    break;
                case 3:
                    for (var i = arrayDemension - 1; i > 0; i--)
                    {
                        line[arrayDemension - i - 1] = gameField[arrayDemension - i - 1,i];
                    }

                    break;
                case 4:
                    for (var i = arrayDemension- 2; i >= 0; i--)
                    {
                        line[arrayDemension - i - 2] = gameField[arrayDemension - i - 2,i];
                    }

                    break;
                case 5:
                    for (var i = arrayDemension - 1; i > 0; i--)
                    {
                        line[arrayDemension - i - 1] = gameField[arrayDemension - i,i];
                    }
                    break;
                default: break;
            }
        }
     private int CheckLine(CircleNumber[] array)
        {
            int count1 = 0, count2 = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].playerMark == 1)
                {
                    count1++;
                    count2 = 0; // Reset count2 when 1 is found
                }
                else if (array[i].playerMark == 2)
                {
                    count2++;
                    count1 = 0; // Reset count1 when 2 is found
                }
                else
                {
                    count1 = 0;
                    count2 = 0;
                }

                if (count1 == 5)
                {
                    return 1;
                }
                else if (count2 == 5)
                {
                    return 2;
                }
            }
            return 0;
        }

public void rotate(Transform fieldPart, bool clockwise)
{
    StartCoroutine(rotateObject(fieldPart, rotationAngle, clockwise));
}

private IEnumerator rotateObject(Transform fieldPart, int angle, bool clockwise)
{
    WaitForSeconds waiter = new WaitForSeconds(delay);

    while (angle > 0)
    {
        yield return waiter;
        var eulerAngles = fieldPart.eulerAngles;

        if(clockwise)
            fieldPart.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z - smoothingAngle);
        else
            fieldPart.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z + smoothingAngle);

        angle-=smoothingAngle;
    }

    yield break;
}   

private void OnDestroy() {
    CircleNumber.OnUserPlaceMark -= Print2DArray;
    CircleNumber.OnUserPlaceMark -= turnOnRotateButtons;
}
}
