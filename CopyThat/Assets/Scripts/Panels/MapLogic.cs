using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MapLogic : Module {

    public Vector2 min;
    public Vector2 max;
    public GameObject MapBackground;
    private RectTransform mapSize;
    private RectTransform thisPos;

    public GameObject LeftDivide;
    public GameObject MiddleDivide;
    public GameObject RightDivide;
    public GameObject TopDivide;
    public GameObject BottomDivide;

    private AudioSource moveSound;

    private RectTransform LeftDiv;
    private RectTransform MiddleDiv;
    private RectTransform RightDiv;
    private RectTransform TopDiv;
    private RectTransform BottomDiv;

    private Vector2 cursorPos;

	// Use this for initialization
	void Start ()
    {
        currentState = controlState.off;
        LeftDiv = LeftDivide.GetComponent<RectTransform>();
        MiddleDiv = MiddleDivide.GetComponent<RectTransform>();
        RightDiv = RightDivide.GetComponent<RectTransform>();
        TopDiv = TopDivide.GetComponent<RectTransform>();
        BottomDiv = BottomDivide.GetComponent<RectTransform>();

        mapSize = MapBackground.GetComponent<RectTransform>();
        thisPos = gameObject.GetComponent<RectTransform>();
        Vector3[] mapCorners = new Vector3[4];
        mapSize.GetLocalCorners(mapCorners);
        min = new Vector2(mapCorners[0].x, mapCorners[0].y);
        max = new Vector2(mapCorners[3].x, mapCorners[0].y);

        moveSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        cursorPos = new Vector2(thisPos.localPosition.x, thisPos.localPosition.y);
        if (Input.GetMouseButton(0) && RectTransformUtility.RectangleContainsScreenPoint(mapSize, Input.mousePosition))
        {
            Interact();
        }

        SetState();
		
	}

    protected override void Interact()
    {
        Vector2 GlobalMin = MapBackground.transform.position - MapBackground.transform.lossyScale;
        Vector2 GlobalMax = MapBackground.transform.position + MapBackground.transform.lossyScale;
        thisPos.localPosition = (new Vector2(
            ((Input.mousePosition.x - GlobalMin.x)
            /(GlobalMax.x - GlobalMin.x))* ( 2 * mapSize.localScale.x) + mapSize.localPosition.x,

              ((Input.mousePosition.y - GlobalMin.y)
            / (GlobalMax.y - GlobalMin.y)) * (2 * mapSize.localScale.y) + mapSize.localPosition.y));

        moveSound.Play();
    }
    
    private void SetState()
    {
        // Correct Quad
        if (cursorPos.x > RightDiv.localPosition.x &&
            cursorPos.y > BottomDiv.localPosition.y &&
            cursorPos.y < TopDiv.localPosition.y)
        {
            currentState = controlState.on;
        }
        else
        {
            currentState = controlState.off;
        }

        // The other Quads
    }
}
