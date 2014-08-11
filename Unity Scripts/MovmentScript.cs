using UnityEngine;
using System.Collections;

public class MovmentScript : MonoBehaviour
{	
	void Update ()
	{		
		float x = Input.GetAxis("Mouse X");
		float y = Input.GetAxis("Mouse Y");
		float z = Input.GetAxis("Mouse ScrollWheel");
		
		if ( Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))
		{
			if(x>y)
				transform.RotateAround(transform.up, -x*.5f);
			if(y>x)
				transform.RotateAround(transform.right, y*.5f);
		}
		else if( Input.GetMouseButton(1) )
		{
			transform.RotateAround(transform.up, -x*.5f);
			transform.RotateAround(transform.right, y*.5f);
			transform.position = new Vector3(0, 1, z*.5f);
		}
	}
}
