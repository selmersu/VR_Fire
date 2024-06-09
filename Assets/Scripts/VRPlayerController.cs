using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.PXR.Input;
using Unity.XR.PXR;




public class VRPlayerController : MonoBehaviour
{
    public CharacterController cc;  //角色控制器
    public ParticleSystem[] particleSystems;

    public float MoveSpeed;         //玩家移动速度
    public Vector2 MoveDire;        //玩家移动数值
    public Vector2 moveDirection;   //玩家移动方向
    
    public float RotationSpeed;     //玩家旋转速度
    public Vector2 rotateDirection; //玩家旋转方向

    public bool isVR = false;       //判断是否在VR设备中运行
    public ItemEvent itemEvent;
    


    // Start is called before the first frame update
    void Start()
    {
        
        cc = GetComponent<CharacterController>();  //获取角色控制器
        if (PXR_Input.IsControllerConnected(PXR_Input.Controller.LeftController))
        {
            isVR = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVR)  //非VR设备运行
        {
            MoveDire.x = Input.GetAxis("Horizontal");   //获取键盘左右输入
            MoveDire.y = Input.GetAxis("Vertical");     //获取键盘上下输入

            // 使用鼠标或键盘输入进行旋转
            rotateDirection.x = Input.GetAxis("Mouse X");
            rotateDirection.y = Input.GetAxis("Mouse Y");

            //test
            if (Input.GetKeyDown(KeyCode.V))
            {
                itemEvent.ShowFE();
                
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                itemEvent.HideFE();
                
            }

        }
        else        //VR设备运行
        {
            MoveDire = GetLeftJoystickValue();          //左手柄摇杆移动

            rotateDirection = GetRightJoystickValue();  //右手柄摇杆旋转
   
        }

        transform.eulerAngles += new Vector3(0, rotateDirection.x * RotationSpeed * Time.deltaTime, 0);     // 旋转摄像机

    }

    private void FixedUpdate()
    {
        cc.Move(transform.forward * MoveSpeed * MoveDire.y + transform.right * MoveSpeed * MoveDire.x); //玩家移动方法
    }

    private Vector2 GetLeftJoystickValue()  //获取左手手柄摇杆移动的值
    {
        Vector2 LeftJoystickValue = Vector2.zero;

        // 检查是否有连接的设备
        if (InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out LeftJoystickValue))
        {
            return LeftJoystickValue;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private Vector2 GetRightJoystickValue() // 获取右手手柄摇杆移动的值
    {
        Vector2 rightJoystickValue = Vector2.zero;

        // 检查是否有连接的设备
        if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out rightJoystickValue))
        {
            return rightJoystickValue;
        }
        else
        {
            return Vector2.zero;
        }
    }
}
