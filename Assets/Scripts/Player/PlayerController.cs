using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Animator animator;

    private string jumpAnimation = "Player Jump", changeLaneAnimation = "Change Lane";

    public GameObject player, shadow;
    public Vector3 playerFirstPos, playerSecondPos;

    public GameObject explosion;

    private GameObject[] starEffect;

    private SpriteRenderer playerRenderer;

    public Sprite trexSprite, playerSprite;

    private bool trexTrigger;

    [HideInInspector]
    public bool isDead;

    [HideInInspector]
    public bool playerJumped;

    void Awake()
    {
        MakeInstance();
        animator = player.GetComponent<Animator>();
        playerRenderer = player.GetComponent<SpriteRenderer>();

        starEffect = GameObject.FindGameObjectsWithTag(Tags._starEffect);
    }

    void Start()
    {
        string path = "Sprites/Player/hero" + GameManager.instance.selectedIndex + "_big";
        playerSprite = Resources.Load<Sprite>(path);
        playerRenderer.sprite = playerSprite;
    }

    void Update()
    {
        ChangeLane();
        Jump();
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    void ChangeLane()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            animator.Play(changeLaneAnimation);
            transform.localPosition = playerSecondPos;
            SoundManager.instance.PlayMoveAudio();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            animator.Play(changeLaneAnimation);
            transform.localPosition = playerFirstPos;
            SoundManager.instance.PlayMoveAudio();
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!playerJumped)
            {
                animator.Play(jumpAnimation);
                playerJumped = true;
                SoundManager.instance.PlayJumpAudio();
            }
        }
    }

    void Die()
    {
        isDead = true;
        player.SetActive(false);
        shadow.SetActive(false);

        GameplayController.instance.speed = 0f;
        GameplayController.instance.GameOver();

        SoundManager.instance.PlayDieAudio();
        SoundManager.instance.PlayGameOverAudio();
    }
    
    void DieWithObstacle(Collider2D target)
    {
        Die();

        explosion.transform.position = target.transform.position;
        explosion.SetActive(true);
        target.gameObject.SetActive(false);

        SoundManager.instance.PlayDieAudio();
    }

    void DestroyObstacle(Collider2D target)
    {
        explosion.transform.position = target.transform.position;
        explosion.SetActive(false); // Deactivating explosion if it's already active
        explosion.SetActive(true);

        target.gameObject.SetActive(false);

        SoundManager.instance.PlayDieAudio();
    }

    IEnumerator TrexDuration()
    {
        yield return new WaitForSeconds(7f);

        if(trexTrigger)
        {
            trexTrigger = false;
            playerRenderer.sprite = playerSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == Tags._obstacle)
        {
            if(!trexTrigger)
            {
                DieWithObstacle(target);
            }
            else
            {
                DestroyObstacle(target);
            }
        }

        if(target.tag == Tags._tRex)
        {
            trexTrigger = true;
            playerRenderer.sprite = trexSprite;
            target.gameObject.SetActive(false);

            SoundManager.instance.PlayPoweupAudio();

            StartCoroutine(TrexDuration());
        }

        if(target.tag == Tags._star)
        {
            for (int i = 0; i < starEffect.Length; i++)
            {
                if(!starEffect[i].activeInHierarchy)
                {
                    starEffect[i].transform.position = target.transform.position;
                    starEffect[i].SetActive(true);
                    break;
                }
            }
            target.gameObject.SetActive(false);
            SoundManager.instance.PlayCoinAudio();
            GameplayController.instance.UpdateStarCount();
        }
    }
}
