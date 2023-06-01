using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photowall : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] bool useGrid = false;
    [Tooltip("Creates a Grid of GameObjects if true and sets them as targets. You can just place your own Objects as childs and deactivate useGrid.")]
    public int cols = 5;
    public int rows = 5;
    public float distance = 2;
    int gridSize;
    [HideInInspector] public bool[] assignedPhoto;
    public static GameObject photos;

    // Start is called before the first frame update
    void Start()
    {
        if(useGrid) CreateGrid();
        else LoadPositions();
        photos = new GameObject();
        photos.name = "photos";
    }

    void CreateGrid(){
        gridSize = cols + rows;
        assignedPhoto = new bool[gridSize];
        int cnt = 0;
        for(int y = 1; y <= cols; y++){
            for(int x = 1; x <= rows; x++){
                cnt++;
                GameObject photoPos = new GameObject();
                photoPos.name = "photoPos " + cnt;
                photoPos.transform.parent = transform;
                photoPos.transform.position = new Vector3(x*distance-1, y*distance-1, transform.position.z);
            }
        }
    }

    void LoadPositions(){
        gridSize = transform.childCount;
        assignedPhoto = new bool[gridSize];
    }
}
