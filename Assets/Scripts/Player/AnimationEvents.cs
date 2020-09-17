using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator animator;
    private string walkAnimation = "Player Walk";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void PlayerWalk()
    {
        animator.Play(walkAnimation);
        if(PlayerController.instance.playerJumped)
        {
            PlayerController.instance.playerJumped = false;
        }
    }

    void AnimationEnded()
    {
        gameObject.SetActive(false);
    }

    void PausePanelClosed()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
