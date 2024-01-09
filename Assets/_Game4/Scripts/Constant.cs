using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
   
}

public static class Anim // Toi muon doi tat ca moi noi thanh Anim + Muon them Attack() cho bot
{
    public const string idleAnim = "idle";
    public const string runAnim = "run";
    public const string attackAnim = "attack";
    public const string deadAnim = "dead";

}

public static class Tag
{
    public const string botTag = "Bot";
    public const string bulletTag = "Bullet";
    public const string playerTag = "Player";
    public const string characterTag = "Character";
}

public static class State
{
    public const int half = 50;
    public const int all = 100;

}