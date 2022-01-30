using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 jumpHeight;
//Monster
    public float Monsterspeed = 10f;
    public Vector2 MonsterjumpHeight;
    //Human
    public float Humanspeed = 10f;
    public Vector2 HumanjumpHeight;
//Animation
    //Run
    private string Run="HRun";

    public Animator animator;
    private float horizontalInput;
    
    public bool monster;
    
    public GameObject monsterObject;
    public Sprite spriteMonster;
    public Sprite spriteHuman;

    bool jump = false;

    public float fallMultiplier = 2.5f;
    public float fallMultiplier2 = 2.5f;
    public float lowJumpMultiplier = 2f;

    private BoxCollider2D boxCollider2d;

    Rigidbody2D rb;

   [SerializeField] private LayerMask platformsLayerMask;
   [SerializeField] private LayerMask SkszyniaLayerMask;
   

    // Start is called before the first frame update
    void Start()
    {
        animator= gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

    }
    
 
    void Update()
    {
        
        Vector3 characterScale = transform.localScale;

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(!monster)
            {
                monster=true;
            }
            else
            {
                monster=false;
            }
        }
        if(monster)
            {
                rb.mass = 100;
                characterScale.x = 0.5f;
                characterScale.y = 0.5f;
                animator.SetBool("Monster",true);
                this.GetComponent<SpriteRenderer>().sprite=spriteMonster;
                speed=Monsterspeed;
                jumpHeight=MonsterjumpHeight;
                Run="MRun";
                boxCollider2d.size = new Vector2(7.3f,5.3f);
                boxCollider2d.offset = new Vector2(0, -1f);
                if (Input.GetAxis("Horizontal") < 0) {
                    characterScale.x = -0.5f;
                    animator.SetBool("MonsterL",true);
                   
                } else if (Input.GetAxis("Horizontal") > 0) {
                    characterScale.x = 0.5f;
                  
                    animator.SetBool("MonsterL",true);
                } else {
                   
                    animator.SetBool("MonsterL",false);

                    animator.SetBool("Idle",true);
                }
                transform.localScale = characterScale;
            }
            else
            {
                characterScale.x = 0.4f;
                characterScale.y = 0.4f;
                rb.mass = 1;
                animator.SetBool("Monster",false);
                this.GetComponent<SpriteRenderer>().sprite=spriteMonster;
                speed=Humanspeed;
                jumpHeight=HumanjumpHeight;
                Run="HRun";
                boxCollider2d.size = new Vector2(2f, 3.5f);
                boxCollider2d.offset = new Vector2(0, -0.8f);
                if (Input.GetAxis("Horizontal") < 0) {
                    characterScale.x = -0.4f;
                    //animator.SetBool("HumanL",true);
                   
                } else if (Input.GetAxis("Horizontal") > 0) {
                    characterScale.x = 0.4f;
                  
                    //animator.SetBool("HumanL",true);
                } else {
                   
                    //animator.SetBool("Human",false);

                   // animator.SetBool("Humanidle",true);
                }
                transform.localScale = characterScale;
            }
   

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))  //makes player jump
        {
            if(monster)
            {
               animator.SetBool("Up",true); 
            }
            
            else
            {
                animator.SetBool("KIdosJump",true);
            }
            
        GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
        
            
        }

        if (rb.velocity.y < 0)
        {
            animator.SetBool("KIdosJump",false);
            if(monster){
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier2 - 1) * Time.deltaTime;
                animator.SetBool("Up",false);
            } else {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                
            }
        } else if(rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        
            
        
        return raycastHit2d.collider != null;
       
    }
    // private bool kladka() {
    //     // GameObject[] kladka;
    //     // kladka = GameObject.FindGameObjectsWithTag("kladki");
    //     RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMaskkladki);
    //     return raycastHit2d.collider != null;
    //}
    private bool IsPush(){
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.right, 1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
      
        if(IsPush() && monster && IsGrounded())
        {
            animator.SetBool("Crate",true);
            speed = 2f;
        }
        else
        {
            animator.SetBool("Crate",false);
            speed=Humanspeed;
        }

        if (this.transform.position.y <= -20) {
            Destroy(gameObject);
            LevelManager.instance.Respawn();
        }
        // Input keys
        horizontalInput = Input.GetAxis("Horizontal");
     

        //Move horizontal, remember to lock Z in rb
        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        if (rb.velocity.y != 0 )
        {
            transform.Translate(Vector3.right * Time.deltaTime * (speed/1.5f) * horizontalInput);
        } else {
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        }

        if(Input.GetAxis("Horizontal")!=0)
        {
            animator.SetBool(Run,true);
        }
        else
        {
            animator.SetBool(Run,false);
        }
        
       
    }
    
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("F"))
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}