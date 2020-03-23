using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandEvtSystem : MonoBehaviour
{
    public List<MonoBehaviour> evtList;
    public GameObject m_warSystem;

    public int randLevel;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer + 1)
        {
            if(Random.Range(0, randLevel) == 0)
            {
                evtList[Random.Range(0, evtList.Count)].GetComponent<IWarPlayable>().PlayEvent(m_warSystem);
            }
            timer = Time.time;
        }
    }
}
