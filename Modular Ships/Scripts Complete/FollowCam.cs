using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowCam : MonoBehaviour
{
	public Vector3 TP_Position;
	Transform pivot;
	public float vertRot;


	[SerializeField]
	Transform target;
	float scale = 1f;
	float orbitRate = 5f;

	[SerializeField]
	float sensitivity = 5f;
	Camera mainCamera;

	static FollowCam instance;


	void Awake()
	{
		mainCamera = GetComponentInChildren<Camera>();
		pivot = mainCamera.transform.parent;
		//disconnect camera
		mainCamera.transform.parent = null;

	}
	public static FollowCam Instance
	{
		get
		{
			if (!instance)
			{
				instance = FindObjectOfType<FollowCam>();
			}
			return instance;
		}
	}
	public static Camera Current{
		get
		{
			return Instance.mainCamera;
		}
	}
	public Vector3 Forward{
		get
		{
			return mainCamera.transform.forward;
		}
	}
	public Vector3 ProjForward{
		get
		{
			return Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
		}
	}
	public Vector3 ProjRight{
		get
		{
			return Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up).normalized;
		}
	}
	
	void LateUpdate()
	{
		if (target)
		{
			
			PositionCam();
			
		}
	}


	void PositionCam()
	{
		Vector3 localPosition = Vector3.zero;
		transform.position = target.position;
		localPosition = TP_Position;
		if (Physics.Linecast(target.position, pivot.TransformPoint(localPosition), out RaycastHit hit, ~0,QueryTriggerInteraction.Ignore))
		{
			localPosition = TP_Position * (hit.distance / TP_Position.magnitude);

			//Vector3.Lerp(transform.position,target.position, 0.3f);
			mainCamera.transform.position = pivot.TransformPoint(localPosition);//Vector3.Lerp(mainCamera.transform.localPosition,localPosition, 0.3f);
		}
		else
		{
			mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,pivot.TransformPoint(localPosition), 0.8f);

		}
	}

	public void OrbitCam(Vector3 input)
	{
		transform.Rotate(input.x*Vector3.up*orbitRate);
		vertRot += -input.y*orbitRate * sensitivity;
		vertRot = Mathf.Clamp(vertRot, -90, 90);
		
		pivot.transform.localEulerAngles = Vector3.right*vertRot;
		mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation,pivot.rotation, 0.5f);
	}
	
	/*public void SetTarget(Transform _target)
	{
		CameraTarget camTarget = _target.GetComponentInChildren<CameraTarget>();
		if (camTarget)
		{
			target = camTarget.transform;
			scale = camTarget.scale;
		}
		else
		{
			target = _target;
			scale = 1f;
		}
		if (cameraMode == CameraMode.Vehicle)
		{
			autoCam.SetTarget(target);
		}
		else
		{
			transform.rotation = Quaternion.identity;
		}
		pivot.transform.localRotation = Quaternion.identity;
	}*/

}
