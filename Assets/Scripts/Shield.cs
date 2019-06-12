using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite woodShield;
    public Sprite ironShield;
    public Sprite goldShield;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (GameManager.instance.getShield())
        {
            //Blocking walls
            case "woodShield":
                spriteRenderer.sprite = woodShield;
                GameManager.instance.damageToPlayer = 15;
                break;
            //Blocking walls
            case "ironShield":
                spriteRenderer.sprite = ironShield;
                GameManager.instance.damageToPlayer = 10;
                break;
            //Blocking walls
            case "goldShield":
                spriteRenderer.sprite = goldShield;
                GameManager.instance.damageToPlayer = 5;
                break;
            //Blocking walls
            default :
                spriteRenderer.sprite = null;
                break;
        }
    }
}
