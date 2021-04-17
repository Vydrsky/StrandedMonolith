using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character,IMovement
{
    [SerializeField] private Player player;
    private float delay;

    public void Move()
    {
        delay = Time.time;
        
        horizontalAxis = delay;
        transform.Translate(new Vector2(horizontalAxis, 0) * Time.deltaTime * moveSpeed, Space.World); //deltaTime - przetwarza wartoœæ wektorow¹ na metry/sekunde np. V(1,0) -> 1m/s w prawo 
        transform.Translate(new Vector2(0, verticalAxis) * Time.deltaTime * moveSpeed, Space.World);
    }

    public void Rotate()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
