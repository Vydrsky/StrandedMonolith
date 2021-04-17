using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] public GameObject myPrefab;
    private List<Room> pokoje;
    // Start is called before the first frame update
    void Start()
    {
        Room pokoj = new Room(myPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
