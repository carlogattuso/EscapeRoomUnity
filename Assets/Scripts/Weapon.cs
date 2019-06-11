using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite firstSwordSprite;
    public Sprite secondSwordSprite;
    public Sprite thirdSwordSprite;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        switch (GameManager.instance.getWeapon())
        {
            //Blocking walls
            case "sword":
                spriteRenderer.sprite = firstSwordSprite;
                break;
        }
    }
}
