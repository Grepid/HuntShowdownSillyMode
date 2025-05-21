using System.IO;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance;

    public static string MissionsJsonPath;
    public static string FlavourTxtPath;
    public static string SaveDirectory;

    public const string SaveFolder = "SavedData";
    public const string MissionsFileName = "MissionsList.json";
    public const string FlavourFileName = "FlavourTextList.txt";

    public DefaultMissions DefaultMissions;

    private void Awake()
    {
        Instance = this;
        SaveDirectory = Path.Combine(Application.persistentDataPath, SaveFolder);
        MissionsJsonPath = Path.Combine(SaveDirectory, MissionsFileName);
        FlavourTxtPath = Path.Combine(SaveDirectory, FlavourFileName);

        SanityCheck();
    }
    public static void SanityCheck()
    {
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        if (!File.Exists(MissionsJsonPath))
        {
            MissionRandomiser.WriteToJson(Instance.DefaultMissions.Missions);
        }
    }
}
