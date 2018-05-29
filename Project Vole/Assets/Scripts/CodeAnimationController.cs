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
    }

    public override void Update() {
        if(paused)
            return;
        t += (Time.time - startTime) / journeyTime;
        if(type == VectorType.ROTATE) {
            float v = Mathf.LerpAngle(originalVector3.z, newVector3.z, t);
            currentVector = new Vector3(0f, 0f, v);
        } else {
            currentVector = Vector3.Slerp(originalVector3, newVector3, t);
        }
        
        if(t >= 1f) {
            done = true;
        }

        HandleUpdate();
    }

    public Vector3 GetProgress() {
        return currentVector;
    }

    public override void Reset(CodeAnimation c) {
        VectorSlerp vs = (VectorSlerp) c;
        if(newVector3 == null || vs.newVector3 == newVector3) {
            return;
        }
        paused = false;
        done = false;
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

public class FloatLerp: CodeAnimation {
    public float original { get; private set; }
    public float newNumber { get; private set; }
    public float journeyTime { get; private set; }
    private float startTime;
    private float currentNumber;
    private float t;

    public FloatLerp(float from, float to, float journeyTime, GameObject g) : base(g) {
        done = false;
        original = from;
        newNumber = to;
        this.journeyTime = journeyTime;
        this.startTime = Time.time;
        t = 0;
        currentNumber = original;
    }

    public override void Update() {
        if(paused)
            return;
        t += (Time.time - startTime) / journeyTime;
        currentNumber = Mathf.Lerp(original, newNumber, t);
        
        if(t >= 1f) {
            done = true;
        }
    }

    public float GetProgress() {
        return currentNumber;
    }

    public override void Reset(CodeAnimation c) {
        FloatLerp vs = (FloatLerp) c;
        if(!done) {
            return;
        }
        paused = false;
        done = false;
        original = vs.original;
        newNumber = vs.newNumber;
        journeyTime = vs.journeyTime;
        startTime = Time.time;
        t = 0;
        currentNumber = original;
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
       bool found = false;

        //If someone tries to add a new animation of a gameobject that already has an animation, 
        //call it's reset method which will evaluate if it should reset instead of always adding 
        //or removing objects from the list
       foreach(CodeAnimation a in animations) {
           if(a.g == anim.g) {
               a.Reset(anim);
               found = true;
               break;
           }
       }

        if(!found)
            animations.Add(anim);
    }

    public CodeAnimation GetAnimation(GameObject g) {
        bool found = false;
        foreach(CodeAnimation a in animations) {
           if(a.g == g) {
               return a;
           }
       }

       return null;

    }

    public void Update() {
        foreach(CodeAnimation anim in animations) {
            anim.Update();
            if(anim.done) {
                anim.paused = true;
            }
        }
    }
}