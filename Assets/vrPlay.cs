using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Avatar;
using Oculus.Platform;
using Oculus.Platform.Models;

public class vrPlay : MonoBehaviour
{
    public OvrAvatar myAvatar;

    void Awake()
    {
        Oculus.Platform.Core.Initialize();
        Oculus.Platform.Users.GetLoggedInUser().OnComplete(GetLoggedInUserCallback);
        Oculus.Platform.Request.RunCallbacks();  //avoids race condition with OvrAvatar.cs Start().
    }

    private void GetLoggedInUserCallback(Message<User> message)
    {
        if (!message.IsError)
        {
            myAvatar.oculusUserID = message.Data.ID.ToString();
        }
    }
}
