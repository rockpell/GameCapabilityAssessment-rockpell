using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualPadContorller : MonoBehaviour, IDeselectHandler, IPointerUpHandler, IPointerDownHandler, IDragHandler
{

    private Image backgroundImg;
    private Image joystrickImg;
    private Vector3 inputVector;

	// Use this for initialization
	void Start () {
        backgroundImg = GetComponent<Image>();
        joystrickImg = transform.GetChild(0).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystrickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / backgroundImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / backgroundImg.rectTransform.sizeDelta.y);
            inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystrickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (backgroundImg.rectTransform.sizeDelta.x / 2), inputVector.z * (backgroundImg.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Verical");
    }
}
