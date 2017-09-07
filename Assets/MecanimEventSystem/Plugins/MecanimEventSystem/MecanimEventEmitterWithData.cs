using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        if (mecanimEventHandler == null) {
            Debug.LogWarning ("Please assign Mecanim Event Handler in editor");
            this.enabled = false;
            return;
        }
			
        loadedData = MecanimEventManager.LoadData (data);
    }

    void Update ()
    {
        if (mecanimEventHandler == null) {
            return;
        }

        MecanimEvent[] events = MecanimEventManager.GetEvents (loadedData, lastStates, animatorController.GetInstanceID (), animator);

        foreach (MecanimEvent e in events) {
			
            MecanimEvent.SetCurrentContext (e);
			
            if (e.paramType != MecanimEventParamTypes.None)
                mecanimEventHandler.GetType ().GetMethod (e.functionName).FastInvoke (mecanimEventHandler);
            else
                mecanimEventHandler.GetType ().GetMethod (e.functionName).FastInvoke (mecanimEventHandler, e.parameter);
        }
    }
}
