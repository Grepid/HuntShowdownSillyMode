using System.IO;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance;

    public static string MissionsJsonPath;
    public static string FlavourTxtPath;

    public const string MissionsFileName = "MissionsList.json";
    public const string FlavourFileName = "FlavourTextList.txt";

    public DefaultMissions DefaultMissions;

    private void Awake()
    {
        Instance = this;
        MissionsJsonPath = Path.Combine(Application.persistentDataPath, MissionsFileName);
        FlavourTxtPath = Path.Combine(Application.persistentDataPath, FlavourFileName);

        SanityCheck();
    }
    public static void SanityCheck()
    {
        if (!File.Exists(MissionsJsonPath))
        {
            //create a way to hold and load Defaults
            MissionRandomiser.WriteToJson(Instance.DefaultMissions.Missions);
        }
    }
}
