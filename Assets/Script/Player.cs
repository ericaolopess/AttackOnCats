using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float velocidade = 7f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 direction = Vector2.zero;

    //JUMP
    public float jumpForce = 12f;
    public bool jump = false;

    //hud
    public int pontos = 0;
    public Text score;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool("pulando", false);
        anim.SetBool("andando", false);
        anim.SetBool("parado", true);

        score.text = "0";

    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetBool("pulando",true);
            anim.SetBool("andando", false);
            anim.SetBool("parado", false);
        }
    }

    private void FixedUpdate()
    {
        Move(Input.GetAxisRaw("Horizontal"));

        if (jump)
        {
            jump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void Move(float move)
    {

        if ((move < 0 ) || (move > 0))
        {
            anim.SetBool("pulando", false);
            anim.SetBool("andando", true);
            anim.SetBool("parado", false);
        }

        if (move > 0)
        {
            direction = Vector2.right;
        }
        if (move < 0)
        {
            direction = Vector2.left;
        }

        rb.velocity = new Vector2(direction.x * velocidade, rb.velocity.y);

        if (move == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("pulando", false);
            anim.SetBool("andando", false);
            anim.SetBool("parado", true);
        }

        transform.right = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("coletaveis"))
        {
            Destroy(collision.gameObject);
            pontos = pontos + 1;
            score.text = pontos.ToString();  
        }
    }


}
