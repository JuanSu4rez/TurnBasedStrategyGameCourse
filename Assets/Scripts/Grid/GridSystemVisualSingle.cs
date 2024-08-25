using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    
    [SerializeField] private MeshRenderer m_Renderer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideVisual () { 
        m_Renderer.enabled = false;
    }

    public void ShowVisual () {
        m_Renderer.enabled = true;
    }
}
