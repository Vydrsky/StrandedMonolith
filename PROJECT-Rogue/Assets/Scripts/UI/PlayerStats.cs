using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private List<Text> StatList  = new List<Text>();
    private Dictionary<string, Text> StatDictionary = new Dictionary<string, Text>();

    private void Start()
    {
        StatDictionary.Add("MS",StatList[0]);
        StatDictionary.Add("DAMAGE",StatList[1]);
        StatDictionary.Add("SHOTSPEED",StatList[2]);
        StatDictionary.Add("AS",StatList[3]);
        StatDictionary.Add("RANGE",StatList[4]);
    }

    public void SetStat(string key, float value)
    {
        if (StatDictionary.ContainsKey(key))
        {
            StatDictionary[key].text = ""+value;
        }
    }
}
