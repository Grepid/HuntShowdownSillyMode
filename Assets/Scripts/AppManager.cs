using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static string MissionsJsonPath;
    public static string FlavourTxtPath;

    public const string MissionsFileName = "MissionsList.json";
    public const string FlavourFileName = "FlavourTextList.txt";
    private void Awake()
    {
        MissionsJsonPath = (Application.persistentDataPath + "/" + MissionsFileName);
        FlavourTxtPath = (Application.persistentDataPath + "/" + FlavourFileName);
    }
}
