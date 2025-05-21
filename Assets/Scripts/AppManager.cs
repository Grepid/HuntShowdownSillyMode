using System.IO;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static string MissionsJsonPath;
    public static string FlavourTxtPath;

    public const string MissionsFileName = "MissionsList.json";
    public const string FlavourFileName = "FlavourTextList.txt";
    private void Awake()
    {
        MissionsJsonPath = Path.Combine(Application.persistentDataPath, MissionsFileName);
        FlavourTxtPath = Path.Combine(Application.persistentDataPath, FlavourFileName);
    }
}
