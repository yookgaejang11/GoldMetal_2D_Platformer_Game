using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMove : MonoBehaviour
{
    public float currentSpeed;
    SpriteRenderer spriteRenderer;
    public float MaxSpeed;
    public float Speed;
    Rigidbody2D rigid;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        currentSpeed = rigid.velocity.x;
        if (Input.GetButtonUp("Horizontal"))
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; //입력이 -1면 true를 반환하고, 그렇지 않으면 false를 반환
            }
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

            if(Mathf.Abs( rigid.velocity.x) < 0.3)
            {
                animator.SetBool("isMove", false);
            }
            else
            {
                animator.SetBool("isMove", true);
            }
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        rigid.AddForce(vec, ForceMode2D.Impulse);
        if (rigid.velocity.x > MaxSpeed)
        {
            rigid.velocity = new Vector2(MaxSpeed,rigid.velocity.y);//velocity 는 현재 속력
        }
        if (rigid.velocity.x < MaxSpeed * (-1))
        {
            rigid.velocity = new Vector2(MaxSpeed*(-1), rigid.velocity.y);//velocity 는 현재 속력
        }
    }
}
