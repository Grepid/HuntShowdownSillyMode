using UnityEngine;
using System.Collections;
using Unity.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;

[System.Serializable]
public class Mission
{
    public string Guid;
    public string Name;
    public string Description;
    public string Verbose;

    public Mission()
    {
        
    }
    public Mission(bool isUnique)
    {
        if(isUnique)Guid = System.Guid.NewGuid().ToString();
    }
}



[System.Serializable]
public class MissionArrayWrapper
{
    public Mission[] MissionsArray;
}

public class MissionRandomiser : MonoBehaviour
{
    public Mission[] MissionsArray;
    public const string missionsFileName = "MissionList.json";
    public const string flavourFileName = "FlavourTextList.txt";

    private string persistentMissionsFilePath;
    private string persistentFlavourFilePath;
    public bool MissionsFound { get; private set; }
    private string[] missions;
    private string[] flavour;

    [SerializeField]
    TextMeshProUGUI resultText,flavourText;


    private void Awake()
    {
        persistentMissionsFilePath = (Application.persistentDataPath + "/" + missionsFileName);
        persistentFlavourFilePath = (Application.persistentDataPath + "/" + flavourFileName);
    }
    private void LoadFlavourText()
    {
        if (!File.Exists(persistentFlavourFilePath))
        {
            Debug.LogError("File not found in Persistent data path: " + persistentFlavourFilePath);
            return;
        }

        string text = File.ReadAllText(persistentFlavourFilePath);
        flavour = text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
    }
    private void LoadMissions()
    {
        if (!File.Exists(persistentMissionsFilePath))
        {
            Debug.LogError("File not found in Persistent data path: " + persistentMissionsFilePath);
            MissionsFound = false;
            return;
        }

        string text = File.ReadAllText(persistentMissionsFilePath);
        missions = text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        MissionsFound = true;
    }
    public void DisplayMissions()
    {
        LoadMissions();
        LoadFlavourText();
        if (MissionsFound)
        {
            string flavourMessage = flavour[Random.Range(0, flavour.Length)] ?? "Install the flavour texts you tasteless bastard" ;
            flavourText.text = flavourMessage;

            string resultMessage = missions[Random.Range(0, missions.Length)] ?? "This literally should not be possible to see lol";
            resultText.text = resultMessage;
        }
        else
        {
            flavourText.text = "TRIED TO SPIN WHEN NO FILES ARE FOUND";
            resultText.text = "TRIED TO SPIN WHEN NO FILES ARE FOUND";
        }
    }



    [ContextMenu("Try put to Json")]
    public void TryPutToJson()
    {
        MissionArrayWrapper maw = new MissionArrayWrapper();
        maw.MissionsArray = MissionsArray;
        
        string jsonString = JsonUtility.ToJson(maw,true);
        string path = persistentMissionsFilePath;
        File.WriteAllText(path, jsonString);
    }

    [ContextMenu("Try read Json")]
    public void TryReadJson()
    {

        if (File.Exists(persistentMissionsFilePath))
        {
            string json = File.ReadAllText(persistentMissionsFilePath);
            MissionArrayWrapper wrapper = JsonUtility.FromJson<MissionArrayWrapper>(json);
            foreach(Mission m in wrapper.MissionsArray)
            {
                print(m.Name);
            }
        }
    }
}
