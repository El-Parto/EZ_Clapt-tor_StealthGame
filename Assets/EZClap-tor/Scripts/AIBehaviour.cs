using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class AIBehaviour : MonoBehaviour
{
    //public Transform objectCol; // from where the aI will raycast from
    [SerializeField] private Collider2D aiCollider;
    
    [SerializeField] private float speed;

    public bool active =true;
    
    private LayerMask playerMask;

    //[SerializeField] private GameObject test;

    private float warningDist = 15;
    private float catchDist = 10;
    //private DetectStatus detectStatus;

    public GUIManager gui;
    

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Looking());
        playerMask = LayerMask.GetMask("PlayerMsk");
        aiCollider = GetComponent<Collider2D>();
       // gui = FindObjectOfType<GUIManager>();
    }

    
    // Update is called once per frame
    void Update()
    {
        if(DetectionWarning())
        {
           
            gui.detectionText.color = new Color(1, 0.5f, 0);
            gui.detectionText.text = "Warning";
        }
        else
        {
            gui.detectionText.color = Color.green;
            gui.detectionText.text = "Clear";
        }

        if(DetectionCaught())
        {
            gui.detectionText.color = new Color(1, 0, 0);
            gui.detectionText.text = "You have been Caught. GAME OVER";
        }

            
        
    }

    public bool DetectionWarning()
    {
        
        
        
        RaycastHit2D warning;
        warning = Physics2D.Raycast(aiCollider.bounds.center, Vector2.right, warningDist, playerMask);
        Debug.DrawRay(aiCollider.bounds.center, Vector3.right * warningDist, Color.cyan);
        return warning.collider != null;
        
    }

    public bool DetectionCaught()
    {

        //float _catchDist = 10.5f;
        RaycastHit2D caught = Physics2D.Raycast(aiCollider.bounds.center, Vector2.right, catchDist, playerMask);
        Debug.DrawRay(aiCollider.bounds.center, Vector3.right * catchDist, Color.yellow);
        return caught.collider != null;
        
    }
    
    public IEnumerator Looking()
    {
        int secondsRange = Random.Range(3,6);
        while(active)
        {
            gameObject.transform.Rotate( new Vector3(0,-180,0));
            warningDist = warningDist * -1; // so the raycast is inverted to match the direction of the object
            catchDist = catchDist * -1;
            
            yield return new WaitForSeconds(secondsRange);    
        }
        
        
        
        //yield return new WaitForSeconds(3);
        
        


    }

}
