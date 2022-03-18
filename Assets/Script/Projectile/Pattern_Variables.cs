using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Pattern_Variables
{
    [SerializeField] public float innerAngle;
    [SerializeField] public int innerNum;
    [SerializeField] public int totalAngle;
    [SerializeField] public int totalNum;
    [SerializeField] public float rotation;
    [SerializeField] public float angle;
    [SerializeField] public int pointNum;
    [SerializeField] public string type;
    [SerializeField] public string key;

    //create copy
    public Pattern_Variables(Pattern_Variables p) 
    {
        this.innerAngle = p.innerAngle;
        this.innerNum = p.innerNum;
        this.totalAngle = p.totalAngle;
        this.totalNum = p.totalNum;
        this.rotation = p.rotation;
        this.angle = p.angle;
        this.pointNum = p.pointNum;
        this.type = p.type;
        this.key = p.key;
    }
}
