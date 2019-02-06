using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class TopDownController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Animator anim;

    private float hAxis;
    private float vAxis;

    [SerializeField]
    private float speed;

    public enum Direction { Haut, Droite, Bas, Gauche };
    public Direction currentDirection = Direction.Bas;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        if (hAxis < 0)
        {
            currentDirection = Direction.Gauche;
            anim.SetBool("isMoving", true);
        }
        else if (hAxis > 0)
        {
            currentDirection = Direction.Droite;
            anim.SetBool("isMoving", true);
        }
        else if (vAxis < 0)
        {
            currentDirection = Direction.Bas;
            anim.SetBool("isMoving", true);
        }
        else if (vAxis > 0)
        {
            currentDirection = Direction.Haut;
            anim.SetBool("isMoving", true);
        }
        else {
            //Idle
            anim.SetBool("isMoving", false);
        }

        anim.SetInteger("Direction", (int)currentDirection);

        if (Input.GetKeyDown(KeyCode.Space)) {
            anim.SetTrigger("Action");
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(hAxis, vAxis).normalized;
        rb2d.MovePosition(((Vector2)transform.position + (direction * speed*Time.fixedDeltaTime)));
    }
}
