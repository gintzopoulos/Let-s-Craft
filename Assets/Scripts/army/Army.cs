using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army
{
    public string name { get; set; }
    public uint power { get; set; }

    public Army(string theName, uint basePower = 0)
    {
        name = theName;
        power = basePower;
    }
}
