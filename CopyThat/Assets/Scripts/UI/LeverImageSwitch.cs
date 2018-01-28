using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class LeverImageSwitch : MonoBehaviour, IPointerDownHandler
{

    public Sprite pressedImage;
    private Sprite initalImage;
    private RectTransform rect; 

    private Image image;
    private Button button;

    EngineeringPanel parentPanel; 

    public void OnPointerDown(PointerEventData eventData)
    {
        switchImage(); 
    }

    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>(); 
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        parentPanel = GetComponentInParent<EngineeringPanel>(); 
        initalImage = image.sprite;
    }

    void switchImage()
    {
        parentPanel.LeverPress(); 
        if (image.sprite == pressedImage)
        {
            rect.SetPositionAndRotation(new Vector3(rect.position.x + 25, rect.position.y - 200, rect.position.z), rect.rotation);
            
            image.sprite = initalImage;
        }
        else
        {
            rect.SetPositionAndRotation(new Vector3(rect.position.x - 25, rect.position.y + 200, rect.position.z), rect.rotation);
            image.sprite = pressedImage;
        }
    }

    void OnDisable()
    {
        //Debug.Log("DISABLED");
        if(image != null )
        {
            if (image.sprite != pressedImage)
            {
                switchImage();
            }
        }
        
        
    }


}
