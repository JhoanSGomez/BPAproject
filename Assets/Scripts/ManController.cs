using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManController : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float test;
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spriteMan;
   void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteMan = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rig.velocity = new Vector2(horizontal, vertical) * velocidad;
        
        anim.SetFloat("Walk", Mathf.Abs(rig.velocity.magnitude));

       
        if (horizontal > 0)
        {
            spriteMan.flipX = false;
            anim.SetBool("Down",false);
            anim.SetBool("Up",false );
        }
        else if (horizontal < 0)
        {
            spriteMan.flipX = true;
            anim.SetBool("Down", false);
            anim.SetBool("Up",false );
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            anim.SetFloat("Walk", 0);
            anim.SetBool("Up", true);
        }else{

            anim.SetBool("Up", false);
        }
        if (Input.GetKey(KeyCode.DownArrow)){
             anim.SetBool("Down", true);
             anim.SetFloat("Walk", 0);
        }else{  
            anim.SetBool("Down", false);
        }
    	if((vertical > 0 && horizontal == 0)){
            anim.SetFloat("Walk", 0);
    	    anim.SetBool("Down", false);
            anim.SetBool("Up", true);
    	}
        if((vertical < 0 && horizontal == 0)){
            anim.SetFloat("Walk", 0);
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
        }

    }
}