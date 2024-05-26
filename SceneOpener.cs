using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOpener : MonoBehaviour
{
    public void openScene(int index){
        SceneManager.LoadScene(index);
    }
}
