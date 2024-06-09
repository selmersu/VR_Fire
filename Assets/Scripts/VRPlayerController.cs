using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.PXR.Input;
using Unity.XR.PXR;




public class VRPlayerController : MonoBehaviour
{
    public CharacterController cc;  //��ɫ������
    public ParticleSystem[] particleSystems;

    public float MoveSpeed;         //����ƶ��ٶ�
    public Vector2 MoveDire;        //����ƶ���ֵ
    public Vector2 moveDirection;   //����ƶ�����
    
    public float RotationSpeed;     //�����ת�ٶ�
    public Vector2 rotateDirection; //�����ת����

    public bool isVR = false;       //�ж��Ƿ���VR�豸������
    public ItemEvent itemEvent;
    


    // Start is called before the first frame update
    void Start()
    {
        
        cc = GetComponent<CharacterController>();  //��ȡ��ɫ������
        if (PXR_Input.IsControllerConnected(PXR_Input.Controller.LeftController))
        {
            isVR = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVR)  //��VR�豸����
        {
            MoveDire.x = Input.GetAxis("Horizontal");   //��ȡ������������
            MoveDire.y = Input.GetAxis("Vertical");     //��ȡ������������

            // ʹ������������������ת
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
        else        //VR�豸����
        {
            MoveDire = GetLeftJoystickValue();          //���ֱ�ҡ���ƶ�

            rotateDirection = GetRightJoystickValue();  //���ֱ�ҡ����ת
   
        }

        transform.eulerAngles += new Vector3(0, rotateDirection.x * RotationSpeed * Time.deltaTime, 0);     // ��ת�����

    }

    private void FixedUpdate()
    {
        cc.Move(transform.forward * MoveSpeed * MoveDire.y + transform.right * MoveSpeed * MoveDire.x); //����ƶ�����
    }

    private Vector2 GetLeftJoystickValue()  //��ȡ�����ֱ�ҡ���ƶ���ֵ
    {
        Vector2 LeftJoystickValue = Vector2.zero;

        // ����Ƿ������ӵ��豸
        if (InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out LeftJoystickValue))
        {
            return LeftJoystickValue;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private Vector2 GetRightJoystickValue() // ��ȡ�����ֱ�ҡ���ƶ���ֵ
    {
        Vector2 rightJoystickValue = Vector2.zero;

        // ����Ƿ������ӵ��豸
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
