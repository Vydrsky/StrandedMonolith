using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IKeyboard, IMovement
{

    protected string characterName;
    protected float horizontalAxis, verticalAxis;
    protected RotationDirectionEnum RotDir = RotationDirectionEnum.RightDirection;
    [SerializeField] protected int maxHealth;      //do ustawieniea w inspektorze dla debugowania 
    [SerializeField] protected int healthPoints;
    [SerializeField] protected float moveSpeed;

    public void readMovementInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
    }

    public void Move()
    {
        transform.Translate(new Vector2(horizontalAxis, 0) * Time.deltaTime * moveSpeed, Space.World); //deltaTime - przetwarza wartoœæ wektorow¹ na metry/sekunde np. V(1,0) -> 1m/s w prawo 
        transform.Translate(new Vector2(0, verticalAxis) * Time.deltaTime * moveSpeed, Space.World);
    }

    public void readTurnInput()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotDir = RotationDirectionEnum.UpDirection;     //enum opisany w RotationDirectionEnum.cs
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RotDir = RotationDirectionEnum.DownDirection;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotDir = RotationDirectionEnum.LeftDirection;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotDir = RotationDirectionEnum.RightDirection;
        }
    }
    public void Rotate()
    {
        switch (RotDir)
        {
            case RotationDirectionEnum.UpDirection:
                {
                    transform.rotation = Quaternion.Euler(0f,0f,90f);
                    break;
                }
            case RotationDirectionEnum.LeftDirection:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    break;
                }
            case RotationDirectionEnum.DownDirection:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                    break;
                }
            case RotationDirectionEnum.RightDirection:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                }
        }
    }
}
