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
    TextMeshProUGUI resultText,flavourText,verboseText,nameText;


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

        MissionsArray = RetrieveAllMissions();
        MissionsFound = true;
    }
    Mission currentMission;
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

            currentMission = MissionsArray[UnityEngine.Random.Range(0, MissionsArray.Length)];
            /*string resultMessage = currentMission.Description;
            resultText.text = resultMessage;
            nameText.text = currentMission.Name;
            verboseText.text = currentMission.Verbose;*/
        }
        else
        {
            /*flavourText.text = "TRIED TO SPIN WHEN NO FILES ARE FOUND";
            resultText.text = "TRIED TO SPIN WHEN NO FILES ARE FOUND";*/
            currentMission = AppManager.Instance.DefaultMissions.Missions[UnityEngine.Random.Range(0, AppManager.Instance.DefaultMissions.Missions.Length)];
        }
        string resultMessage = currentMission.Description;
        resultText.text = resultMessage;
        nameText.text = currentMission.Name;
        verboseText.text = currentMission.Verbose;
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
    [ContextMenu("Apply Json to Inspector Array")]
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
        AppManager.SanityCheck();
        if (File.Exists(AppManager.MissionsJsonPath))
        {
            string json = File.ReadAllText(AppManager.MissionsJsonPath);

            MissionsWrapper wrapper = JsonUtility.FromJson<MissionsWrapper>(json);
            
            var res = Array.FindAll(wrapper.Array, m => m.Type == type);
            return res;
        }
        else
        {
            Debug.LogError($"The file at {AppManager.MissionsJsonPath} did not exist. Creating default copy");
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
            var ms = RetrieveMissionsOfType(t);
            result.AddRange(ms);
        }
        return result.ToArray();
    }
    [ContextMenu("Save Current To Defaults")]
    public void SaveCurrentToDefaults()
    {
        AppManager.Instance.DefaultMissions.WriteMissions(MissionsArray);
    }
    [ContextMenu("Load Defaults")]
    public void LoadDefaults()
    {
        MissionsArray = (Mission[])AppManager.Instance.DefaultMissions.Missions.Clone();
    }
}
