using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{

    public static GameObject GetTopParent(GameObject startObject)
    {
        GameObject returnObject = startObject;
        bool checkExist = true;
        while (checkExist)
        {
            var trans = returnObject.transform.parent;
            if (trans)
                returnObject = trans.parent.gameObject;
            else
                checkExist = false;
        }
        return returnObject;
    }

}
