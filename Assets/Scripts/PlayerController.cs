using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveDistance = 1f;
    [SerializeField] private float moveTime = 0.4f;
    [SerializeField] private float colliderDistance = 1;

    [SerializeField] private bool isIdle = true;
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool jumpStart = false;
    [SerializeField] private bool parentedToObject = false;

    [Header("Audio")]
    [SerializeField] private AudioClip[] idleAudioClips;
    [SerializeField] private AudioClip hopAudioClip;
    [SerializeField] private AudioClip hitAudioClip;
    [SerializeField] private AudioClip splashAudioClip;


    public bool IsDead => isDead;
    public bool IsJumping => isJumping;
    public bool JumpStart => jumpStart;
    public bool IsIdle => isIdle;

    public bool ParentedToObject
    {
        get => parentedToObject;
        set => parentedToObject = value;
    }


    [SerializeField] private ParticleSystem deathParticle = null;
    [SerializeField] private ParticleSystem splashParticle = null;
    [SerializeField] private GameObject chick = null;

    private Renderer renderer = null;
    private bool isVisible = false;
    private AudioSource audioSource;

    private void Start()
    {
        renderer = chick.GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!GameManager.Instance.CanPlay)    return;

        if(isDead)    return;

        CanIdle();
        CanMove();

        IsVisible();
    }

    private void CanIdle()
    {
        if (isIdle)
        {
            SetRotation();
        }
    }
    private void SetRotation()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))        CheckIfIdle(270,0,0);
        if(Input.GetKeyDown(KeyCode.DownArrow))      CheckIfIdle(270,180,0);
        if(Input.GetKeyDown(KeyCode.RightArrow))     CheckIfIdle(270,90,0);
        if(Input.GetKeyDown(KeyCode.LeftArrow))      CheckIfIdle(270,-90,0);
    }

    private void CheckIfIdle(float x, float y, float z)
    {
        chick.transform.rotation=Quaternion.Euler(x,y,z);

        CheckIfCanMove();

        PlayAudioClip(idleAudioClips[Random.Range(0,idleAudioClips.Length)]);
    }

    private void CheckIfCanMove()
    {
        Physics.Raycast(transform.position, -chick.transform.up, out RaycastHit hit, colliderDistance);

        Debug.DrawRay(transform.position,-chick.transform.up*colliderDistance,Color.red,2);

        if (hit.collider == null)
        {
            SetMove();
        } else
        {
            if (hit.collider.CompareTag("Collider"))
            {
                print("Collider is in front so you can't move in this direction");
            } else
            {
                SetMove();
            }
        }
    }

    private void SetMove()
    {
        isIdle = false;
        isMoving = true;
        jumpStart = true;
    }

    private void CanMove()
    {
        if (isMoving)
        {
            // When player Release the key jump animation starts
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));
                SetMoveForwardState();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance));
            else if (Input.GetKeyUp(KeyCode.RightArrow))
                Moving(new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z));
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
                Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z));
        }
    }

    private void Moving(Vector3 position)
    {
        isIdle = false;
        isMoving = false;
        isJumping = true;
        jumpStart = false;

        // Play jump audio clip
        PlayAudioClip(hopAudioClip);

        // Move player and after its done call MoveComplete methode
        LeanTween.move(gameObject, position, moveTime).setOnComplete(MoveComplete);
    }

    private void MoveComplete()
    {
        isJumping = false;
        isIdle = true;

        PlayAudioClip(idleAudioClips[Random.Range(0,idleAudioClips.Length)]);
    }

    private void SetMoveForwardState()
    {
        GameManager.Instance.UpdateDistanceCount();
    }

    private void IsVisible()
    {
        if (renderer.isVisible)
        {
            isVisible = true;
        }

        if (!renderer.isVisible && isVisible)
        {
            print("Player off screen");

            GotHit();
        }
    }

    public void GotHit()
    {
        isDead = true;

        GameManager.Instance.GameOver();

        PlayAudioClip(hitAudioClip);

        var emission = deathParticle.emission;
        emission.enabled = true;
    }

    public void GotSoaked()
    {
        isDead = true;

        GameManager.Instance.GameOver();

        PlayAudioClip(splashAudioClip);

        chick.SetActive(false);

        var emission = splashParticle.emission;
        emission.enabled = true;
    }

    private void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }



}
