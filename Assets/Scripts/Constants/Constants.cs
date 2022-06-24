using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public enum Tags
    {  
        Player,
        Grappleable,
        GameController
    }

    public static Dictionary<Tags, string> tags = new Dictionary<Tags, string>()
    {
        {Tags.Player, "Player"},
        {Tags.GameController, "GameController"},
        {Tags.Grappleable, "Grappleable" }
    };
}