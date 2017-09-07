using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEventHandler : MonoBehaviour, IMecanimEventHandler
{
    static ImageEventHandler () 
    {
        DelegateSupport.RegisterActionType<ImageEventHandler>();
        DelegateSupport.RegisterActionType<ImageEventHandler, object>();
    }

    void OnAnimationEnd ()
    {
        Debug.Log ("OnAnimationEnd");
    }

    void OnAnimationUpdate (object parameter)
    {
        Debug.Log ("OnAnimationUpdate " + parameter);
    }
}
