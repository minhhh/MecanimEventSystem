using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using UBootstrap;

[RequireComponent (typeof(Animator))]
public class MecanimEventEmitter : MonoBehaviour
{
    public UnityEngine.Object animatorController;
    public Animator animator;
    protected IMecanimEventHandler mecanimEventHandler;

    void Awake ()
    {
        if (animator == null) {
            Debug.LogWarning ("Do not find animator component.");
            this.enabled = false;
            return;
        }

        if (animatorController == null) {
            Debug.LogWarning ("Please assgin animator in editor. Add emitter at runtime is not currently supported.");
            this.enabled = false;
            return;
        }

        mecanimEventHandler = GetComponent <IMecanimEventHandler> ();
        if (mecanimEventHandler == null) {
            Debug.LogWarning ("Please make sure you have a IMecanimEventHandler component");
            this.enabled = false;
            return;
        }
    }

    void Update ()
    {
        if (mecanimEventHandler == null) {
            return;
        }

        MecanimEvent[] events = MecanimEventManager.GetEvents (animatorController.GetInstanceID (), animator);

        foreach (MecanimEvent e in events) {
            MecanimEvent.SetCurrentContext (e);
            var m = ReflectionHelper.GetMethodRecursive (mecanimEventHandler, e.functionName);
            if (e.paramType == MecanimEventParamTypes.None) {
                m.FastInvoke (mecanimEventHandler);
            } else {
                m.FastInvoke (mecanimEventHandler, e.parameter);
            }

        }
    }

}
