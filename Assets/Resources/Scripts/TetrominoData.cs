using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TetrominoData : MonoBehaviour
{   
    private List<Transform> blocks = new List<Transform>();
    public List<Transform> Blocks { get => blocks; }
   
    // Start is called before the first frame update
    void Start()
    {
        blocks = GetComponentsInChildren<Transform>().ToList();
    }
}