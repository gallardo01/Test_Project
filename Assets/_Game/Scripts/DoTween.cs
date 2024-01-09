using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoTween : MonoBehaviour
{
    [SerializeField] Transform weaponButton, skinButton, playButton;
    [SerializeField] Transform weaponShiftTransform, skinShiftTransform, playShiftTransform;
    [SerializeField] float shiftTime;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) ChangeScene();
    }

    public void ChangeScene() {
        weaponButton.DOMove(weaponShiftTransform.position, shiftTime);
        skinButton.DOMove(skinShiftTransform.position, shiftTime);
        playButton.DOMove(playShiftTransform.position, shiftTime);
    }
}