using UnityEngine;

public class UIUtils : MonoBehaviour
{
    public void CloseApp()
    {
        Application.Quit();
    }
    public void OpenLocalLow()
    {
        Application.OpenURL(AppManager.SaveDirectory);
    }
}
