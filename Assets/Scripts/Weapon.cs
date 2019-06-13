using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite woodSword;
    public Sprite ironSword;
    public Sprite goldSword;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (GameManager.instance.getWeapon())
        {
            //Blocking walls
            case "woodSword":
                spriteRenderer.sprite = woodSword;
                GameManager.instance.damageToEnemy = 15;
                break;
            //Blocking walls
            case "ironSword":
                spriteRenderer.sprite = ironSword;
                GameManager.instance.damageToEnemy = 20;
                break;
            //Blocking walls
            case "goldSword":
                spriteRenderer.sprite = goldSword;
                GameManager.instance.damageToEnemy = 25;
                break;
            default:
                spriteRenderer.sprite = null;
                break;
        }
    }
}
