using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{

    // Player
    public static float attackRange = 12;

    // WEAPON
    public static float WEAPON_SPEED = 10;
    public static float rotateSpeed = 720; // Degrees

    // ANIMATION TRIGGER
    public static string ATTACK_ANIM = "IsAttack";
    public static string RUN_ANIM = "IsRun";
    public static string IDLE_ANIM = "IsIdle";
    public static string DEATH_ANIM = "IsDeath";
    public static string SKIN_DANCE_ANIM = "IsDanceSkin";

    // TAG
    public static string PLAYER_TAG = "Player";

    // MATERIAL
    public static Material Diffuse = new Material(Shader.Find("Diffuse"));

}
