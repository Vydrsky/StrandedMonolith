using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character,IMovement
{
    [SerializeField] private Player player;


    public void Move()
    {
        transform.Translate(new Vector2(horizontalAxis, 0) * Time.deltaTime * moveSpeed, Space.World); //deltaTime - przetwarza warto�� wektorow� na metry/sekunde np. V(1,0) -> 1m/s w prawo 
        transform.Translate(new Vector2(0, verticalAxis) * Time.deltaTime * moveSpeed, Space.World);
    }

    public void Rotate()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
