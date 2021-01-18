using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;


public class Position_Detection : MonoBehaviour
{
    public float Value = 0.1f;

    private bool move = false;
    public Transform Root_pos;

    private List<Vector3> list = new List<Vector3>();
    private List<Gesture> list_g = new List<Gesture>();

    public float nextPos_space = 0.05f;
    public GameObject filler_Space;

    public bool allow_input = true;
    public string Capture_Name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(!move && OVRInput.Get(OVRInput.RawButton.RIndexTrigger, OVRInput.Controller.RTouch))
        {
            Moving_Start();
        }
        else if(move && !OVRInput.Get(OVRInput.RawButton.RIndexTrigger, OVRInput.Controller.RTouch))
        {
            Moving_End();
        }
        else if(move && OVRInput.Get(OVRInput.RawButton.RIndexTrigger, OVRInput.Controller.RTouch))
        {
            UpdateMovement();
        }
    }

    void Moving_Start()
    {
        Debug.Log("Movement started.");
        move = true;
        //leftover list deleted
        list.Clear();

        //start this new list
        list.Add(Root_pos.position);

        //sanity check
        if(filler_Space != null)
        {
            Destroy(Instantiate(filler_Space, Root_pos.position, Quaternion.identity), 2);
        }
    }

    void Moving_End()
    {
        Debug.Log("Movement ended.");
        move = false;

        Point[] p_list = new Point[list.Count];

        for(int i=0; i< list.Count; i++)
        {
            Vector2 point_pos = Camera.main.WorldToScreenPoint(list[i]);
            p_list[i] = new Point(point_pos.x, point_pos.y, 0);
        }

        Gesture point_g = new Gesture(p_list);

        if(allow_input == true)
        {
            point_g.Name = Capture_Name;
            list_g.Add(point_g);
        }
        else
        {
          //No capturing
        }
    }

    void UpdateMovement()
    {
        Debug.Log("Movement updated...");
        Vector3 currentPos = list[list.Count - 1];

        if(Vector3.Distance(Root_pos.position, currentPos )> nextPos_space)
        {
            list.Add(Root_pos.position);

            //sanity check
            if (filler_Space != null)
            {
                Destroy(Instantiate(filler_Space, Root_pos.position, Quaternion.identity), 2);
            }
        }
       
    }
}
