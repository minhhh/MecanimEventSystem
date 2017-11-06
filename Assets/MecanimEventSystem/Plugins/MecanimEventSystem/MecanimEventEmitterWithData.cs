using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UBootstrap;

[RequireComponent (typeof(Animator))]
public class MecanimEventEmitterWithData : MonoBehaviour
{
    public UnityEngine.Object animatorController;
    public Animator animator;
    public MecanimEventData data;
    protected IMecanimEventHandler mecanimEventHandler;

    private Dictionary<int, Dictionary<int, Dictionary<int, List<MecanimEvent>>>> loadedData;

    private Dictionary<int, Dictionary<int, AnimatorStateInfo>> lastStates = new Dictionary<int, Dictionary<int, AnimatorStateInfo>> ();

    void Awake ()
    {
        if (animator == null) {
            Debug.LogWarning (string.Format ("GameObject:{0} cannot find animator component.", this.transform.name));
            this.enabled = false;
            return;
        }

        if (animatorController == null) {
            Debug.LogWarning ("Please assgin animator in editor. Add emitter at runtime is not currently supported.");
            this.enabled = false;
            return;
        }

        if (data == null) {
            this.enabled = false;
            return;
        }

        mecanimEventHandler = GetComponent <IMecanimEventHandler> ();
        if (mecanimEventHandler == null) {
            Debug.LogWarning ("Please make sure you have a IMecanimEventHandler component");
            this.enabled = false;
            return;
        }

        SetData (data);
    }

    public void SetData (MecanimEventData data)
    {
        this.data = data;
        loadedData = MecanimEventManager.LoadData (data);
    }

    void Update ()
    {
        if (mecanimEventHandler == null || animatorController == null) {
            return;
        }

        MecanimEvent[] events = MecanimEventManager.GetEvents (loadedData, lastStates, animatorController.GetInstanceID (), animator);

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
