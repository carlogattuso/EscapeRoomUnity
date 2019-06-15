using UnityEngine;
using System.Collections;

public class Tramp : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation()
    {
        animator.SetTrigger("Activate");
    }
}
