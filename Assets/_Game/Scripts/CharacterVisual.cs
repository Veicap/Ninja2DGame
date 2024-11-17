using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentAnim = "idle";
    public void ChangeAnim(string nameAnim)
    {
        if (currentAnim != nameAnim)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = nameAnim;
            animator.SetTrigger(currentAnim);
        }
    }
    public void RunAim()
    {
        ChangeAnim("Run");
    }
    public void DeadAnim()
    {
        ChangeAnim("Dead");
    }
    public void IdleAim()
    {

        ChangeAnim("Idle");
    }
}
