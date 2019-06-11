using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite woodShield;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        switch (GameManager.instance.getWeapon())
        {
            //Blocking walls
            case "wood":
                spriteRenderer.sprite = woodShield;
                break;
        }
    }
}
