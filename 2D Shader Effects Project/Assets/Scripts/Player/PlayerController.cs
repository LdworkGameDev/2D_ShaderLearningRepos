using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rigid;

    [ColorUsage(true, true)]
    public Color color1;
    [ColorUsage(true, true)]
    public Color color2;
    [ColorUsage(true, true)]
    public Color color3;
    
    private Material material;

    [Space]
    public float moveSpeed = 0f;
    public float jumpForce = 0f;

    private float xMove = 0;
    private bool isFacingRight = true;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void FixedUpdate()
    {
        if ((xMove < 0 && isFacingRight) || (xMove > 0 && !isFacingRight))
        {
            Flip();
        }
        rigid.velocity = new Vector2(xMove *moveSpeed, rigid.velocity.y);
    }

    private void Update()
    {
        anim.SetBool("_Punch", false);

        xMove = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("_Speed", Mathf.Abs(xMove));
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            material.SetColor("_GlowingColor", color1);
            anim.SetBool("_Punch", true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            material.SetColor("_GlowingColor", color2);
            anim.SetBool("_Punch", true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            material.SetColor("_GlowingColor", color3);
            anim.SetBool("_Punch", true);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        float scale = transform.localScale.x;
        scale *= -1;
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }
}
