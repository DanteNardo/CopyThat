using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ButtonImageSwitcher : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public Sprite pressedImage;
    private Sprite initalImage;

    private Image image; 
    private Button button;

    public void OnPointerDown(PointerEventData eventData)
    {
        switchImage();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switchImage(); 
    }

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>(); 
        button = GetComponent<Button>(); 
        initalImage = image.sprite;
       // button.OnPointerDown.AddListener(() => switchImage());
    }

    void switchImage()
    {
        if(image.sprite == pressedImage)
        {
            image.sprite = initalImage; 
        }
        else
        {
            image.sprite = pressedImage; 
        }
    }
	
	
}
