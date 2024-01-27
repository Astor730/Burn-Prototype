using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public string waterTag = "Wet";
    private Tilemap tilemap;
    private Rigidbody2D rb;
    private bool isMoving = false;
    public int health = 3;
    private bool movementEnabled = true;

    private void Start()
    {
        tilemap = FindObjectOfType<Tilemap>(); // Change this if you have multiple Tilemaps
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        Movement();
    }

    private void CheckHealth()
    {
        if(health <=0)
        {
            movementEnabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(waterTag))
        {
            health -=1;
        }
    }

    private void Movement()
    {
        if(movementEnabled)
        {
            if(!isMoving)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;

                if(movement.magnitude > 0.1f)
                {
                    Vector3 targetPosition = transform.position + movement;
                    Vector3Int cellPosition = tilemap.WorldToCell(targetPosition);

                    // Move the player to the new cell's center
                    Vector3 snappedPosition = tilemap.GetCellCenterWorld(cellPosition);
                    StartCoroutine(MoveToPosition(snappedPosition));
                
                }
            }
        }
        
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 targetPosition) 
    {
        isMoving = true;
        float sqrRemainingDistance = (transform.position - targetPosition).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - targetPosition).sqrMagnitude;
            yield return null;
        }

        isMoving = false;
    }

    
}
