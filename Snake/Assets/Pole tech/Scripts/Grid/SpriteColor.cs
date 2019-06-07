using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteColor
{
    public Sprite sprite;
    public Color col;
}

[System.Serializable]
public class AnimName
{
    public string animName;
}

[System.Serializable]
public class Direction
{
    public enum Condition { Up, Down, Left, Right }
    public Condition conditions;
}