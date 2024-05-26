using System;
using System.IO;
using System.Text;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public void saveGame(CircleNumber[,] gameField, string whoWon){
        StringBuilder sb = new();
        sb.Append("Date and time=").Append(DateTime.Now.ToString("dd.MM.yyyy hh:mm")).Append("\n");
        sb.Append(whoWon).Append("\n GameField=\n");
        for(int i=0; i<6;i++){
            for(int j=0; j<6;j++){
                sb.Append(gameField[i,j].ToString()).Append(" ");
            }
            sb.Append("\n");
        }
        if(!Directory.Exists($"{Application.persistentDataPath}\\GameSaves"))
            Directory.CreateDirectory($"{Application.persistentDataPath}\\GameSaves");

        File.WriteAllText($"{Application.persistentDataPath}\\GameSaves\\GameSave{DateTime.Now.ToString("dd.MM.yyyy hh.mm")}.txt", sb.ToString());
    }
}
