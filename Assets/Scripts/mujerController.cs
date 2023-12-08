using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mujerController : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float test;
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spriteMujer;

    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteMujer = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rig.velocity = new Vector2(horizontal, vertical) * velocidad;
        
        anim.SetFloat("Camina", Mathf.Abs(rig.velocity.magnitude));

       
        if (horizontal > 0)
        {
            spriteMujer.flipX = false;
            anim.SetBool("Baja",false);
            anim.SetBool("Sube",false );
        }
        else if (horizontal < 0)
        {
            spriteMujer.flipX = true;
            anim.SetBool("Baja", false);
            anim.SetBool("Sube",false );
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            anim.SetFloat("Camina", 0);
            anim.SetBool("Sube", true);
        }else{

            anim.SetBool("Sube", false);
        }
        if (Input.GetKey(KeyCode.DownArrow)){
             anim.SetBool("Baja", true);
             anim.SetFloat("Camina", 0);
        }else{  
            anim.SetBool("Baja", false);
        }
    	if((vertical > 0 && horizontal == 0)){
            anim.SetFloat("Camina", 0);
    	    anim.SetBool("Baja", false);
            anim.SetBool("Sube", true);
    	}
        if((vertical < 0 && horizontal == 0)){
            anim.SetFloat("Camina", 0);
            anim.SetBool("Sube", false);
            anim.SetBool("Baja", true);
        }

    }
}
