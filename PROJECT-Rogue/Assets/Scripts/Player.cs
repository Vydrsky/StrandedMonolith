using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character,IKeyboard,IMovement
{
    private Rigidbody2D _rigidbody;
    private float horizontalAxis, verticalAxis;

    public void getMovementInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
    }

    public void Move()
    {
        transform.Translate(new Vector2(horizontalAxis, 0) * Time.deltaTime * moveSpeed); //deltaTime - przetwarza wartoœæ wektorow¹ na metry/sekunde np. V(1,0) -> 1m/s w prawo 
        transform.Translate(new Vector2(0, verticalAxis) * Time.deltaTime * moveSpeed);
    }


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        characterName = "Hero";
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        getMovementInput();
        Move();
    }
}
