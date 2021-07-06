using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    public TextMesh flag;
    private void Start()
    {
        flag.text = 35454 + "m"; // récup le nombre de prefab instancié
    }
}
