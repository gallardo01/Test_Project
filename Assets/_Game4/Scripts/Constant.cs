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

public static class Status
{
    public const bool win = true;
    public const bool lose = false;
}

public static class PopUpText
{
    public const string win = "You lucky bitch";
    public const string lose = "Nice try";

}

public static class GameTimer
{
    public const int pause = 0;
    public const int resume = 1;
}