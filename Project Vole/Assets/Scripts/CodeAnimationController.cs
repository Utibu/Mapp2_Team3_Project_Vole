using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeAnimation {
    protected int hashCode = 0;
    public bool done { get; protected set; }
    public bool paused;
    public GameObject g { get; protected set; }

    public CodeAnimation(GameObject g) {
        done = false;
        this.g = g;
    }

    public virtual void Reset(CodeAnimation c) {

    }

    public virtual void Update() {

    }
}

public enum VectorType { ROTATE, MOVE, SCALE };

public class VectorSlerp: CodeAnimation {
    public Vector3 originalVector3 { get; private set; }
    public Vector3 newVector3 { get; private set; }
    public float journeyTime { get; private set; }
    private float startTime;
    private Vector3 currentVector;
    private Transform transform;
    private float t;

    
    private VectorType type;

    public VectorSlerp(Vector3 from, Vector3 to, float journeyTime, Transform transform, VectorType type) : base(transform.gameObject) {
        done = false;
        originalVector3 = from;
        newVector3 = to;
        this.journeyTime = journeyTime;
        this.startTime = Time.time;
        this.transform = transform;
        this.type = type;
        t = 0;
        currentVector = originalVector3;
        //Debug.Log("KJKJK");
    }

    public override void Update() {
        if(paused)
            return;
       // float fracComplete = (Time.time - startTime) / journeyTime;
        t += (Time.time - startTime) / journeyTime;
        //Debug.Log("ROTATING " + t);
        if(type == VectorType.ROTATE) {
            float v = Mathf.LerpAngle(originalVector3.z, newVector3.z, t);
            currentVector = new Vector3(0f, 0f, v);
        } else {
            currentVector = Vector3.Slerp(originalVector3, newVector3, t);
        }
        
        if(t >= 1f) {
           // Debug.Log("DONE");
            done = true;
        }

        HandleUpdate();
    }

    public Vector3 GetProgress() {
        return currentVector;
    }

    public override void Reset(CodeAnimation c) {
        VectorSlerp vs = (VectorSlerp) c;
        //Debug.LogWarning("WANTS TO RESET!!! " + vs.originalVector3 + " TO: " + vs.newVector3 + " AND CHECKING IF NOT SIMILAR TO " + newVector3);
        if(newVector3 == null || vs.newVector3 == newVector3) {
            return;
        }
        //Debug.LogWarning(vs.originalVector3);
        paused = false;
        done = false;
       // Debug.Log("RESET: " + vs.newVector3 + " FROM: " + vs.originalVector3);
        //originalVector3 = new Vector3(0f, 0f, transform.rotation.z);
        originalVector3 = vs.originalVector3;
        newVector3 = vs.newVector3;
        journeyTime = vs.journeyTime;
        startTime = Time.time;
        transform = vs.transform;
        type = vs.type;
        t = 0;
        currentVector = originalVector3;
    }

    public void HandleUpdate() {
        if(transform == null) {
            return; 
        }

        switch(type) {
            case VectorType.ROTATE:
                transform.parent.rotation = Quaternion.Euler (currentVector);
                break;
            case VectorType.MOVE:
                transform.position = currentVector;
                break;
            case VectorType.SCALE:
                transform.localScale = currentVector;
                break;
            default:
                break;
        }
    }

}
public class CodeAnimationController: MonoBehaviour {
    public static CodeAnimationController instance = null;
    private List<CodeAnimation> animations;

    void Awake() {
		//Singleton-pattern to allow other scripts to access this game manager by name
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
	}

    void Start() {
        animations = new List<CodeAnimation>();
    }

    public void Add(CodeAnimation anim) {
        //if(!animations.Contains((c => c == ))) {
            
       // }

       bool found = false;

       foreach(CodeAnimation a in animations) {
           if(a.g == anim.g) {
               //Debug.Log("dfgffgfg");
               //Debug.LogError(((VectorSlerp)anim).newVector3);
               a.Reset(anim);
               found = true;
               break;
           }
       }

        if(!found)
            animations.Add(anim);
       
       /* if(!wentOk) {
            Debug.LogWarning("ALREADY EXISTS IN CODE-ANIMATION");
        }*/
    }

    public CodeAnimation GetAnimation(GameObject g) {
        bool found = false;
        foreach(CodeAnimation a in animations) {
           if(a.g == g) {
               //Debug.Log("dfgffgfg");
               //Debug.LogError(((VectorSlerp)anim).newVector3);
               return a;
           }
       }

       return null;

    }

    public void Update() {
        //Debug.Log(animations.Count);
        foreach(CodeAnimation anim in animations) {
            anim.Update();
            if(anim.done) {
                anim.paused = true;
            }
        }
    }
}