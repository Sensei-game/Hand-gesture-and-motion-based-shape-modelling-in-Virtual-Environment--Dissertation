using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gestures
{
    public string name;
    public List<Vector3> Data_line;
    public UnityEvent is_Recognized;
}

public class Hand_Detection : MonoBehaviour
{

   // public float value = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gestures> Gestures;

    public bool allow_input = true;

    private List<OVRBone> point_Bones;
    private Gestures lastGesture;

    // Start is called before the first frame update
    void Start()
    {
        point_Bones = new List<OVRBone>(skeleton.Bones);
        lastGesture = new Gestures();
    }

    // Update is called once per frame
    void Update()
    {
        //sanity check, and allow capture via SpaceBar
        if(allow_input && Input.GetKeyDown(KeyCode.Space))
        {
            Save_Gesture();
        }

       // Gesture thisGesture = Recognize_Gesture();
        //bool confirmation = !thisGesture.Equals(new Gesture());
    }

    //Capture Gestures Method
   void Save_Gesture()
    {
        Gestures gesture = new Gestures();
        gesture.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();

        foreach(var point in point_Bones)
        {
            //semi-accurate position, relative to the position of hand
            data.Add(skeleton.transform.InverseTransformPoint(point.Transform.position));
        }
        gesture.Data_line = data;
        Gestures.Add(gesture);
    }
}
