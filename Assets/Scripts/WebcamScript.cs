using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamScript : MonoBehaviour {

	public GameObject WebCamPlane;
	public Button FireButton;
	// Use this for initialization
	void Start () {

		if(Application.isMobilePlatform){
			GameObject cameraParent = new GameObject ("camParent");
			cameraParent.transform.position = this.transform.position;
			this.transform.parent = cameraParent.transform;
			cameraParent.transform.Rotate (Vector3.right,90);
		}

		Input.gyro.enabled = true;

		FireButton.onClick.AddListener (OnButtonDown);

		WebCamTexture webCamTexture = new WebCamTexture();
		WebCamPlane.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;
		webCamTexture.Play ();
	}

	void OnButtonDown(){
		GameObject bullet = Instantiate (Resources.Load ("bullet",typeof(GameObject))) as GameObject;
		Rigidbody rb = bullet.GetComponent<Rigidbody> ();
		bullet.transform.rotation = Camera.main.transform.rotation;
		bullet.transform.position = Camera.main.transform.position;
		rb.AddForce (Camera.main.transform.forward * 500f);
		Destroy (bullet,3);

	}
	
	// Update is called once per frame
	void Update () {
		Quaternion camRotation = new Quaternion (Input.gyro.attitude.x,Input.gyro.attitude.y,-Input.gyro.attitude.z,-Input.gyro.attitude.w);
		this.transform.localRotation = camRotation;
	}
}
