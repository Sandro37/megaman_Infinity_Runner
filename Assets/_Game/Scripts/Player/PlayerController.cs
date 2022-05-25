using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float forceJump;
    [SerializeField] private float radiusCheckGround;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private Transform groundCheck;
    [Header("Slide")]
    [SerializeField] private float slideTemp;
    private float timeTemp;

    [Header("Collider")]
    [SerializeField] private Transform collider2DTransform;
    [SerializeField] private Vector2 positionColliderRun;
    [SerializeField] private Vector2 positionColliderSlide;

    [Header("Audio")]
    [SerializeField] private AudioClip slideFx;
    [SerializeField] private AudioClip jumpFx;

    [Header("UI")]
    public UnityEngine.UI.Text pointTxt;
    public static int Point { get; set; }

    new AudioSource audio;
    private Animator anim;
    private Rigidbody2D rig;

    private bool canJump;
    private bool canSlide;

    private bool isGround;
    private bool isSlide;
    void Start()
    {
        Point = 0;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        pointTxt.text = Point.ToString();
        JumpInput();
        SlideInput();
        GroundCheck();
    }
    private void FixedUpdate()
    {
        Jump();
        Slide();
    }

    private void JumpInput()
    {
        if (Input.GetMouseButtonDown(0) && isGround)
        {
            canJump = true;
        }
    }

    private void SlideInput()
    {
        if (Input.GetMouseButtonDown(1) && isGround && !isSlide)
        {
            canSlide = true;
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            rig.velocity = Vector2.zero;
            rig.AddForce(forceJump * Vector2.up, ForceMode2D.Impulse);
            audio.PlayOneShot(jumpFx);
            canJump = false;
            isSlide = false;
        }

        anim.SetBool("jump", !isGround);
    }
    private void Slide()
    {
        if (canSlide)
        {
            canSlide = false;

            isSlide = true;
            audio.PlayOneShot(slideFx);
            timeTemp = 0;
            collider2DTransform.transform.localPosition = positionColliderSlide; //new Vector2(0.064f, -0.419f);
        }

        if (isSlide)
        {
            timeTemp += Time.deltaTime;

            if(timeTemp >= slideTemp)
            {
                isSlide = false;
                collider2DTransform.transform.localPosition = positionColliderRun; //new Vector2(0.064f, -0.08f);
            }
        }

        anim.SetBool("slide", isSlide);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            PlayerPrefs.SetInt("Point", Point);
            if (Point > PlayerPrefs.GetInt("Record"))
            {
                PlayerPrefs.SetInt("Record", Point);
            }
            SceneManager.LoadScene("GameOver");
        }
    }
    #region Ground_Check
    private void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, radiusCheckGround, layerGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radiusCheckGround);
    }
    
    #endregion
}
