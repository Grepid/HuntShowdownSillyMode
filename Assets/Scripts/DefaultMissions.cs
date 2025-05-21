using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultMissions", menuName = "Scriptable Objects/DefaultMissions")]
public class DefaultMissions : ScriptableObject
{
    public Mission[] Missions;

    public void WriteMissions(Mission[] missions)
    {
        Missions = (Mission[])missions.Clone();
    }
}
