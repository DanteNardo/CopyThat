using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ModuleType
{                   // Width x Height
	Text_Monitor,   // 8 x 4
	Warning_Light,  // 2 x 2
	Button,         // 2 x 2
	Button_Array,   // 4 x 2
	Switch,         // 2 x 2
	Switch_Array,   // 4 x 2
	Lever,          // 2 x 4
	ValveCrank      // 4 x 4
}

public abstract class Module : MonoBehaviour
{
	public string label;
	protected int width;
	protected int height;
    public controlState currentState;

	public int Width { get; set; }
   

	public int Height { get; set; }


	public enum controlState
	{
		on,
		off,
		minimum,
		balance,
		maximum,
		set0,
		set1,
		set2,
		set3, 
		set4,
		set5,
		set6,
		set7,
		set8,
		set9,
	}

	public ModuleType moduleType;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

    protected abstract void Interact();
}
