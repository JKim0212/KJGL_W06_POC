using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class RoomSystem : MonoBehaviour
{
    List<CrewController> crewList = new List<CrewController>();

    private void Start()
    {
        foreach (CrewController crew in GetComponentsInChildren<CrewController>())
        {
            crewList.Add(crew);
        }
    }

    public void AddCrew(CrewController crewToAdd)
    {
        crewList.Add(crewToAdd);
    }

    public void RemoveCrew(CrewController crewToRemove)
    {
        crewList.Remove(crewToRemove);
    }
}
