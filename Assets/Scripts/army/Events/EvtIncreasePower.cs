using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EvtIncreasePower : MonoBehaviour, IWarPlayable
{
    uint armyNo;
    public void PlayEvent(GameObject warSystem)
    {
        armyNo = (uint)Random.Range(1, 3);
        switch (armyNo)
        {
            case 1:
                warSystem.GetComponent<WarSystem>().army1.power += (uint)Random.Range(3, 12);
                break;
            case 2:
                warSystem.GetComponent<WarSystem>().army2.power += (uint)Random.Range(3, 12);
                break;
            default:
                break;
        }
    }
}
