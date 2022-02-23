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

    public bool IsDead => isDead;
    public bool IsJumping => isJumping;
    public bool JumpStart => jumpStart;
    public bool IsIdle => isIdle;


    [SerializeField] private ParticleSystem particle = null;
    [SerializeField] private GameObject chick = null;

    private Renderer renderer = null;
    private bool isVisible = false;

    private void Start()
    {
        renderer = chick.GetComponent<Renderer>();
    }

    private void Update()
    {

        //Todo can play

        if(isDead)    return;

        CanIdle();
        CanMove();

        IsVisible();
    }

    private void CanIdle()
    {
        if (isIdle)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                CheckIfCanMove();
            }
        }
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

        // Move player and after its done call MoveComplete methode
        LeanTween.move(gameObject, position, moveTime).setOnComplete(MoveComplete);
    }

    private void MoveComplete()
    {
        isJumping = false;
        isIdle = true;
    }

    private void SetMoveForwardState()
    {

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

    private void GotHit()
    {
        isDead = true;

        var emission = particle.emission;
        emission.enabled = true;
    }


}
