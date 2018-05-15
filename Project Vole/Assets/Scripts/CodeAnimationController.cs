using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeAnimation {
    protected int hashCode = 0;
    public bool done { get; protected set; }
    public GameObject g { get; protected set; }

    public CodeAnimation(GameObject g) {
        done = false;
        this.g = g;
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

    
    private VectorType type;

    public VectorSlerp(Vector3 from, Vector3 to, float journeyTime, Transform transform, VectorType type) : base(transform.gameObject) {
        originalVector3 = from;
        newVector3 = to;
        this.journeyTime = journeyTime;
        this.startTime = Time.time;
        this.transform = transform;
        this.type = type;
        this.hashCode = 1;
        //this.hashCode = 1;
        currentVector = originalVector3;
        Debug.Log("KJKJK");
    }

    public override void Update() {
        
        float fracComplete = (Time.time - startTime) / journeyTime;
        Debug.Log("ROTATING " + fracComplete);
        currentVector = Vector3.Slerp(currentVector, newVector3, fracComplete);
        if(fracComplete >= 1) {
            done = true;
        }
    }

    public void HandleUpdate() {
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

       foreach(CodeAnimation a in animations) {
           if(a.g == anim.g) {
               return;
           }
       }

        animations.Add(anim);
       
       /* if(!wentOk) {
            Debug.LogWarning("ALREADY EXISTS IN CODE-ANIMATION");
        }*/
    }

    public void Update() {
        Debug.Log(animations.Count);
        foreach(CodeAnimation anim in animations.ToList()) {
            anim.Update();
            if(anim.done) {
                animations.Remove(anim);
            }
        }
    }
}