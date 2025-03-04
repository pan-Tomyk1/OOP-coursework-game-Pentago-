using UnityEngine;

public class OpenSettingsPanel : MonoBehaviour
{

    public GameObject settingsPanel;
    // Start is called before the first frame update
    public void openPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void closePanel()
    {
        settingsPanel.SetActive(false);
    }
}
