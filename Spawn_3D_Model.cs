using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_3D_Model : MonoBehaviour
{
    //list of gestures from Position_Detection

    public List<GameObject> instance;

    public void Pop_Model(string _model_Name)
    {
        foreach (var model in instance)
        {
            //Trigger the object with a uniqe mesh and texture in to spawn
            model.SetActive(_model_Name == model.name);
        }
    }

}
