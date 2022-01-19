using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using VRKeys;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.Events;

public class VRKeyboardManager : MonoBehaviour
{
	public Keyboard keyboard;

	public GameObject playerCamera;
	public Vector3 relativePosition = new Vector3(0,1,2);

	public TMP_InputField playerName;

	public GameObject leftMarret;
	public GameObject rightMarret;

	public GameObject leftBaseController; 
	public GameObject rightBaseController;

	List<InputDevice> devices;
	public XRNode controllerNode;

	bool isEnabled = false;

	private void Awake()
	{
		devices = new List<InputDevice>();
	}

	void GetDevice()
	{
		InputDevices.GetDevicesAtXRNode(controllerNode, devices);

	}

	void Start()
	{
		GetDevice();
	}

	public void EnableVRKeyboard()
	{
		isEnabled = true;
		keyboard.Enable();
		keyboard.SetPlaceholderMessage("Care este numele dumneavoastra?");

		keyboard.OnUpdate.AddListener(HandleUpdate);
		keyboard.OnSubmit.AddListener(HandleSubmit);
		keyboard.OnCancel.AddListener(HandleCancel);

		keyboard.gameObject.transform.position = playerCamera.transform.position + relativePosition;
		AttachMarrets();

		leftBaseController.GetComponent<XRRayInteractor>().enabled = false;
		rightBaseController.GetComponent<XRRayInteractor>().enabled = false;

	}

	void AttachMarrets()
	{
		leftMarret.transform.SetParent(leftBaseController.transform);
		leftMarret.transform.localPosition = Vector3.zero;
		leftMarret.transform.localRotation = Quaternion.Euler(new Vector3(90f,0f,0f));
		leftMarret.SetActive(true);

		rightMarret.transform.SetParent(rightBaseController.transform);
		rightMarret.transform.localPosition = Vector3.zero;
		rightMarret.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
		rightMarret.SetActive(true);
	}

	void DetachMarrets()
	{
		leftMarret.transform.SetParent(null);
		leftMarret.SetActive(false);

		rightMarret.transform.SetParent(null);
		rightMarret.SetActive(false);
	}

	public void DisableVRKeyboard() 
	{
		isEnabled = false;
		keyboard.OnUpdate.RemoveListener(HandleUpdate);
		keyboard.OnSubmit.RemoveListener(HandleSubmit);
		keyboard.OnCancel.RemoveListener(HandleCancel);

		keyboard.Disable();

		DetachMarrets();

		leftBaseController.GetComponent<XRRayInteractor>().enabled = true;
		rightBaseController.GetComponent<XRRayInteractor>().enabled = true;

	}

	private void Update()
	{
        //GetDevice();
        //foreach (var device in devices)
        //{
        //    Debug.Log(device.name + " " + device.characteristics);

        //    if (device.isValid)
        //    {
        //        bool inputValue;

        //        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out inputValue) && inputValue) //butonul a
        //        {
        //            if (!isEnabled)
        //            {
        //                EnableVRKeyboard();
        //            }
        //            else
        //            {
        //                DisableVRKeyboard();
        //            }
        //        }
        //        if (keyboard.disabled)
        //        {
        //            return;
        //        }
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isEnabled)
            {
                EnableVRKeyboard();
            }
            else
            {
                DisableVRKeyboard();
            }
        }

        if (keyboard.disabled)
        {
            return;
        }
    }

	public void HandleUpdate(string text)
	{
		keyboard.HideValidationMessage();
		playerName.text = text;
		playerName.caretPosition = playerName.text.Length;

	}

	public void HandleSubmit(string text)
	{
		DisableVRKeyboard();
		var eventSystem = EventSystem.current;
		if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
	}

	public void HandleCancel()
	{
		Debug.Log("Cancelled keyboard input!");
		DisableVRKeyboard();

		var eventSystem = EventSystem.current;
		if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
	}

	public void OnSelectEnter()
	{
		Debug.Log("Entered");
	}

	public void OnSelectExit()
	{
		Debug.Log("Exited");

	}
}
