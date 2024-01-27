using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite grassSprite;
    public Collider2D playerCollider;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myCollider.IsTouching(playerCollider))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchToGrassSprite();
            }
        }
    }

    private void SwitchToGrassSprite()
    {
        Vector2 colliderSize = myCollider.size;
        Vector2 colliderOffset = myCollider.offset;

        spriteRenderer.sprite = grassSprite;

        myCollider.size = colliderSize;
        myCollider.offset = colliderOffset;
    } 
}
