using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private Player player;

    public void EndDeath() {
        player.OnDespawn();
    }

    public void EndAttack() {
        player.ChangeAnim(Constants.IDLE_ANIM);
    }

    public void Shoot() {
        player.Throw();
    }

    public void EndRun() {
        player.ChangeAnim(Constants.IDLE_ANIM);
    }

    // Can attack right after moving
    public void StartRun() {
        player.CanAttack();
    }
}
