using UnityEngine;
using System.Collections;
using Unity.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Linq;

[System.Serializable]
public enum MissionType {Loadout,Gameplay,Kill}


[System.Serializable]
public class Mission
{
    public string GUID;
    public MissionType Type;
    public string Name;
    public string Description;
    public string Verbose;

    public Mission()
    {
        
    }
    
    public Mission(bool isUnique)
    {
        if(isUnique) GUID = System.Guid.NewGuid().ToString();
    }
}




[System.Serializable]
public class MissionsWrapper
{
    public Mission[] Array;
}

public class MissionRandomiser : MonoBehaviour
{
    public bool readFileOnLoad;
    public Mission[] MissionsArray;
    public bool MissionsFound { get; private set; }
    private string[] flavour;

    [SerializeField]
    TextMeshProUGUI resultText,flavourText;


    private void Awake()
    {
        if (readFileOnLoad)
        {
            MissionsArray = RetrieveAllMissions();
        }
    }
    private void LoadFlavourText()
    {
        if (!File.Exists(AppManager.FlavourTxtPath))
        {
            return;
        }

        string text = File.ReadAllText(AppManager.FlavourTxtPath);
        flavour = text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
    }
    private void LoadMissions()
    {
        if (!File.Exists(AppManager.MissionsJsonPath))
        {
            MissionsFound = false;
            return;
        }

        string text = File.ReadAllText(AppManager.MissionsJsonPath);
        //missions = text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        MissionsArray = RetrieveAllMissions();
        MissionsFound = true;
    }
    public void DisplayMissions()
    {
        LoadMissions();
        LoadFlavourText();
        if (MissionsFound)
        {
            string flavourMessage;
            if (flavour == null)flavourMessage = "Install the flavour texts you tasteless bastard";
            else
            {
                flavourMessage = flavour[UnityEngine.Random.Range(0, flavour.Length)];
            }
            flavourText.text = flavourMessage;

            string resultMessage = MissionsArray[UnityEngine.Random.Range(0, MissionsArray.Length)].Description ?? "This literally should not be possible to see lol";
            resultText.text = resultMessage;
        }
        else
        {
            flavourText.text = "TRIED TO SPIN WHEN NO FILES ARE FOUND";
            resultText.text = "TRIED TO SPIN WHEN NO FILES ARE FOUND";
        }
    }

    [ContextMenu("WriteOutTypes")]
    public void WriteOutTypes()
    {
        foreach(Mission m in RetrieveMissionsOfType(MissionType.Loadout))
        {
            print(m.GUID);
        }

        foreach (Mission m in RetrieveMissionsOfType(MissionType.Gameplay))
        {
            print(m.GUID);
        }

        foreach (Mission m in RetrieveMissionsOfType(MissionType.Kill))
        {
            print(m.GUID);
        }
    }

    [ContextMenu("Write to Json")]
    public void ToJson()
    {
        WriteToJson(MissionsArray);
    }
    [ContextMenu("Apply Json")]
    public void ApplyJson()
    {
        LoadMissions();
    }

    public static void WriteToJson(Mission[] missions)
    {
        MissionsWrapper maw = new MissionsWrapper();
        foreach(Mission m in missions)
        {
            if(m.GUID == null || m.GUID == "")
            {
                m.GUID = Guid.NewGuid().ToString();
            }
        }
        maw.Array = missions;
        
        string jsonString = JsonUtility.ToJson(maw,true);

        File.WriteAllText(AppManager.MissionsJsonPath, jsonString);
    }

    public static void AddToJson(Mission m)
    {
        var missionArray = RetrieveAllMissions();
        var missions = missionArray.ToList();
        missions.Add(m);

        WriteToJson(missions.ToArray());
    }


    public static Mission[] RetrieveMissionsOfType(MissionType type)
    {
        if (File.Exists(AppManager.MissionsJsonPath))
        {
            string json = File.ReadAllText(AppManager.MissionsJsonPath);
            MissionsWrapper wrapper = JsonUtility.FromJson<MissionsWrapper>(json);
            var res = Array.FindAll(wrapper.Array, m => m.Type == type);
            return res;
        }
        else
        {
            Debug.LogError($"The file at {AppManager.MissionsJsonPath} does not exist");
        }
        return new Mission[]{};
    }
    public static Mission[] RetrieveMissionsOfTypes(MissionType[] types)
    {
        List<Mission> result = new List<Mission>();
        foreach(MissionType t in types)
        {
            result.AddRange(RetrieveMissionsOfType(t));
        }
        return result.ToArray();
    }
    public static Mission[] RetrieveAllMissions()
    {
        List<Mission> result = new List<Mission>();
        foreach (MissionType t in Enum.GetValues(typeof(MissionType)))
        {
            result.AddRange(RetrieveMissionsOfType(t));
        }
        return result.ToArray();
    }
}
