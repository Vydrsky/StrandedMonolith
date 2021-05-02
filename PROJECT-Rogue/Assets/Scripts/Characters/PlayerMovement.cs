using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : IKeyboard,IMovement
{
    private Vector2 movement;
    public Rigidbody2D _rigidbody;
    private RotationDirectionEnum RotDir = RotationDirectionEnum.RightDirection;
    private bool playerRotated = false;

    public void readMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }
    public void move(Player player)
    {
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(movement.x * player.MoveSpeed,0), ForceMode2D.Impulse);
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, movement.y * player.MoveSpeed), ForceMode2D.Impulse);
    }

    public void readTurnInput()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotDir = RotationDirectionEnum.UpDirection;     //enum opisany w RotationDirectionEnum.cs
            playerRotated = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            RotDir = RotationDirectionEnum.DownDirection;
            playerRotated = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotDir = RotationDirectionEnum.LeftDirection;
            playerRotated = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotDir = RotationDirectionEnum.RightDirection;
            playerRotated = true;
        }
    }

    public void rotate(Player player)
    {
        if (playerRotated)
        {
            switch (RotDir)
            {
                case RotationDirectionEnum.UpDirection:
                    {
                        player.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                        break;
                    }
                case RotationDirectionEnum.LeftDirection:
                    {
                        player.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                        break;
                    }
                case RotationDirectionEnum.DownDirection:
                    {
                        player.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                        break;
                    }
                case RotationDirectionEnum.RightDirection:
                    {
                        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        break;
                    }
            }
            player.WeaponItem.weapon.CheckAttack();
            playerRotated = false;
        }
    }
}
