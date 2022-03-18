using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_UI : MonoBehaviour
{
    public void SetHealth(int amount) 
    {
        GetComponent<Text>().text = $"Lives: {amount}";
    }
}
