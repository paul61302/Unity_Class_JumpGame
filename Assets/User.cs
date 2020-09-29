using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User 
{
    public string UserID;
    public string Password;
    public int Score;
    public User()
    {
        UserID = DataUse.UserID;
        Password = DataUse.Password;
        Score = DataUse.Score;
    }
}
