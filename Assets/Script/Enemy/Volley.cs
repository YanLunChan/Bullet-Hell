using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Volley
{
    [SerializeField] public Transform SpawnLocal;
    [SerializeField] public string tag;
    [SerializeField] public int numVolley;
    [SerializeField] public float iteration;
    [SerializeField] public float delay;
    [SerializeField] public bool target;
    [SerializeField] public int pathing;
    [SerializeField] public Pattern_Variables[] settings;
}
