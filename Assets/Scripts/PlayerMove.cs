using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMove : MonoBehaviour
{
    public float currentSpeed;
    SpriteRenderer spriteRenderer;
    public float MaxSpeed;
    public float Jump_power;
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

        currentSpeed = rigid.velocity.x; //���� �ӵ� ���
        //Player�� �ӵ��� �ް��� ����(normalized)
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //Player Jump
        if (Input.GetButtonDown("Jump") && !animator.GetBool("IsJump"))
        {
            rigid.AddForce(Vector2.up *Jump_power, ForceMode2D.Impulse);
            animator.SetBool("IsJump", true);
        }

        //Player flip
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; //�Է��� -1�� true�� ��ȯ�ϰ�, �׷��� ������ false�� ��ȯ
        }

        //Player Move animation
        if(Mathf.Abs( rigid.velocity.x) < 0.3f)
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            animator.SetBool("isMove", true);
        }
            
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //player move
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        rigid.AddForce(vec, ForceMode2D.Impulse);

        //player Max Speed
        if (rigid.velocity.x > MaxSpeed)
        {
            rigid.velocity = new Vector2(MaxSpeed,rigid.velocity.y);//velocity �� ���� �ӷ�
        }
        if (rigid.velocity.x < MaxSpeed * (-1))
        {
            rigid.velocity = new Vector2(MaxSpeed*(-1), rigid.velocity.y);//velocity �� ���� �ӷ�
        }

        //Landing platform
        if(rigid.velocity.y < 0) {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));


            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    animator.SetBool("IsJump", false);
            }
        }
       
    }
}
