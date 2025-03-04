using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOpener : MonoBehaviour
{
    public void openScene(int index){
        SceneManager.LoadScene(index);
    }
    public void returnToGreetingScene(){
        ChangeCircleColor.numberOfMoves=0;
        SceneManager.LoadScene(0);
    }
}
