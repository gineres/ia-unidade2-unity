using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType{
    HEAL,
    BOMB
}

public class Collectible : MonoBehaviour
{
    public CollectibleType collectibleType;
    private SpriteRenderer spriteRenderer;
    public Sprite healSprite;
    public Sprite bombSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateSprite();
    }

    void UpdateSprite()
    {
        switch (collectibleType)
        {
            case CollectibleType.HEAL:
                spriteRenderer.sprite = healSprite;
                break;

            case CollectibleType.BOMB:
                spriteRenderer.sprite = bombSprite;
                break;
        }
    }
}
