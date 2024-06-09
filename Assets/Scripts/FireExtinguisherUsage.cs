using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FireExtinguisherUsage : MonoBehaviour
{
    public InputDevice rightController;
    public InputDevice leftController;
    // ���û�����������
    public FireEffectController fireEffectController;
    public XRRayInteractor rayInteractor;

    void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    public void GetParticleSystem(GameObject obj)
    {
        // ʹ�� GetComponent<ParticleSystem>() ����ȡ����� GameObject �� ParticleSystem ���
        ParticleSystem fireParticleSystem = obj.GetComponent<ParticleSystem>();

        if (fireParticleSystem != null)
        {
            // �����ȡ���� ParticleSystem ��������� ExtinguishFire ����
            fireEffectController.ExtinguishFire(fireParticleSystem);
        }
        
    }

    /*// ���ֱ�λ�����
    public void ExtinguishFireAtController(InputDevice controller)
    {
        // ���XR Ray Interactor
        if (rayInteractor != null)
        {
            // ���߼��
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {  
                GameObject hitObject = hit.collider.gameObject;// ��ȡ���߻��е�����

                ParticleSystem fireParticleSystem = hitObject.GetComponent<ParticleSystem>();// ��ȡ��Ϸ�����ϵĻ�������ϵͳ���

                if (fireParticleSystem != null)
                {
                    fireEffectController.ExtinguishFire(fireParticleSystem);
                }
                else
                {
                    Debug.LogWarning("The hit object does not have a ParticleSystem component.");
                }
            }
        }
        else
        {
            Debug.LogWarning("rayInteractor is not assigned.");
        }*/


        /*// ��ȡ�ֱ���λ�úͷ���
        Vector3 controllerPosition;
        Quaternion controllerRotation;
        controller.TryGetFeatureValue(CommonUsages.devicePosition, out controllerPosition);
        controller.TryGetFeatureValue(CommonUsages.deviceRotation, out controllerRotation);

        //���߳���5m
        Vector3 EndPosition = controllerPosition + controllerRotation * Vector3.forward * 5f;   // ������������Ϊ�ֱ�ǰ5m��λ��
        Ray ray = new Ray(controllerPosition, EndPosition - controllerPosition);    // ���ֱ�λ�÷���һ�����ߵ�Ŀ���

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))    // ������߻���������
        {
            GameObject hitObject = hit.collider.gameObject;    // ��ȡ���е���Ϸ����
 
            ParticleSystem fireParticleSystem = hitObject.GetComponent<ParticleSystem>();   // ��ȡ��Ϸ�����ϵĻ�������ϵͳ���

            fireEffectController.ExtinguishFire(fireParticleSystem);    // ����ExtinguishFire
        }*/
    

}
