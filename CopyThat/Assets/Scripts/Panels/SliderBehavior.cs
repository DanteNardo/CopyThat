using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBehavior : Module
{
    public controlState currentState;
    private float MinMin;
    private float MinMed;
    private float MedMax;
    private float MaxMax;
    public GameObject AbsMinMark;
    public GameObject MinMedMark;
    public GameObject MedMaxMark;
    public GameObject AbsMaxMark;
    public GameObject arrow;
    float arPos;
    Vector3 screenPoint;
    Vector3 offset;

    // Use this for initialization
    void Start()
    {
        MinMin = AbsMinMark.transform.position.y;
        MinMed = MinMedMark.transform.position.y;
        MedMax = MedMaxMark.transform.position.y;
        MaxMax = AbsMaxMark.transform.position.y;

        transform.position = new Vector3(gameObject.transform.position.x, Random.Range(MinMin, MaxMax), gameObject.transform.position.z);
        
    }


    // Update is called once per frame
    void Update()
    {
        arPos = arrow.transform.position.y;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                Interact();
            }
        }

        // Checks where arrow is and sets state
        if (arPos >= MinMin && arPos < MinMed)
        {
            currentState = controlState.minimum;
        }
        else if (arPos >= MinMed && arPos < MedMax)
        {
            currentState = controlState.balance;
        }
        else if (arPos >= MedMax && arPos < MaxMax)
        {
            currentState = controlState.maximum;
        }

        // Stops transformation if too far
        if (arPos <= MinMin)
        {
            transform.position = new Vector3(gameObject.transform.position.x, MinMin, gameObject.transform.position.z);
        }
        else if (arPos >= MaxMax)
        {
            transform.position = new Vector3(gameObject.transform.position.x, MaxMax, gameObject.transform.position.z);
        }

    }

    // Moves the slider
    protected override void Interact()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        Debug.Log("Inteact Hit");
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint - offset);
        transform.position = new Vector3(gameObject.transform.position.x, curPosition.y, gameObject.transform.position.z);
    }


}
