using UnityEngine;
using System.Collections;

public class MouseRotation : MonoBehaviour
{
    public Transform Target;                                          //鼠标控制相机旋转 
    public float xSpeed = 200, ySpeed = 200, mSpeed = 5;              //鼠标拖拽时相机旋转的速度  X方向 Y方向 滚轮缩放速度
    public float yMinLimit = -50, yMaxLimit = 50;                     //鼠标拖拽时 Y轴视角极限值 最小 最大
    public float distance = 4, minDistance = 2, maxDistance = 5;      //滚轮控制相机远近时，相机离模型距离 当前、最小、最大
    public bool needDamping = false;                                  //鼠标拖拽时，是否开启相机平滑
    public float damping = 5.0f;                                      //开启相机平滑时  平滑完成的时间 值越小越快
    public float x = 0.0f, y = 0.0f;                                  //拖拽对象的欧拉角
	// Use this for initialization
	void Start () {
	    if (Target)
	    {
	        Target = GameObject.Find("GameObject").transform;
	    }
	    Vector3 angles = transform.eulerAngles;
	    x = angles.y;
	    y = angles.x;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Target)
	    {
	        //使用鼠标左键点击拖拽控制相机旋转
	        if (Input.GetMouseButton(0))
	        {
	            x += Input.GetAxis("Mouse X")*xSpeed*0.02f;
	            y -= Input.GetAxis("Mouse Y")*ySpeed*0.02f;
	            y = ClampAngle(y, yMinLimit, yMaxLimit);
	        }
            distance -= Input.GetAxis("Mouse ScrollWheel") * mSpeed;
	        distance = Mathf.Clamp(distance, minDistance, maxDistance);
            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
            Vector3 disVector =new Vector3(0.0f, 0.0f, -distance);
	        Vector3 position = rotation * disVector + Target.position;

	        if (needDamping)
	        {
	            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime*damping);
	            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime*damping);
	        }
	        else
	        {
	            transform.rotation = rotation;
	            transform.position = position;
	        }
	    }
	}

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    //void CarmOn()
    //{
    //    bool needDamping = false;
    //}
}
