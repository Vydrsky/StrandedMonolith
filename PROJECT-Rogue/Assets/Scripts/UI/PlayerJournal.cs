using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJournal : MonoBehaviour
{
    [SerializeField] private Text text;

    public void SetText(string txt)
    {
        text.text = txt;
    }
    public void SetColor(Color _color)
    {
            this.text.GetComponent<Text>().color = _color;
    }
}