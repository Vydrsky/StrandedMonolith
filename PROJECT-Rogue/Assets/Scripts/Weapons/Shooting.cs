using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static Shooting instance;

    [SerializeField] public List<GameObject> bulletPrefabs = new List<GameObject>();
    [SerializeField] public List<LineRenderer> raycastPrefabs = new List<LineRenderer>();

    private void Start() { instance = this; }
}
