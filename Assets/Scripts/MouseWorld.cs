using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    [SerializeField]
    private LayerMask mousePlaneLayerMask;

    public static MouseWorld instance;

    private void Awake() {
        instance = this;   
    }

    public MouseWorld GetInstance() {
        if (instance == null) 
            instance = new MouseWorld();        
        return instance;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = MouseWorld.GetMousePosition();
    }

    public static Vector3 GetMousePosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
