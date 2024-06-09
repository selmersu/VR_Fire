using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FireExtinguisherUsage : MonoBehaviour
{
    public InputDevice rightController;
    public InputDevice leftController;
    // 引用火焰灭火控制器
    public FireEffectController fireEffectController;
    public XRRayInteractor rayInteractor;

    void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    public void GetParticleSystem(GameObject obj)
    {
        // 使用 GetComponent<ParticleSystem>() 来获取传入的 GameObject 的 ParticleSystem 组件
        ParticleSystem fireParticleSystem = obj.GetComponent<ParticleSystem>();

        if (fireParticleSystem != null)
        {
            // 如果获取到了 ParticleSystem 组件，调用 ExtinguishFire 方法
            fireEffectController.ExtinguishFire(fireParticleSystem);
        }
        
    }

    /*// 在手柄位置灭火
    public void ExtinguishFireAtController(InputDevice controller)
    {
        // 检查XR Ray Interactor
        if (rayInteractor != null)
        {
            // 射线检测
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {  
                GameObject hitObject = hit.collider.gameObject;// 获取射线击中的物体

                ParticleSystem fireParticleSystem = hitObject.GetComponent<ParticleSystem>();// 获取游戏对象上的火焰粒子系统组件

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


        /*// 获取手柄的位置和方向
        Vector3 controllerPosition;
        Quaternion controllerRotation;
        controller.TryGetFeatureValue(CommonUsages.devicePosition, out controllerPosition);
        controller.TryGetFeatureValue(CommonUsages.deviceRotation, out controllerRotation);

        //射线长度5m
        Vector3 EndPosition = controllerPosition + controllerRotation * Vector3.forward * 5f;   // 将结束点设置为手柄前5m的位置
        Ray ray = new Ray(controllerPosition, EndPosition - controllerPosition);    // 从手柄位置发射一条射线到目标点

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))    // 如果射线击中了物体
        {
            GameObject hitObject = hit.collider.gameObject;    // 获取击中的游戏对象
 
            ParticleSystem fireParticleSystem = hitObject.GetComponent<ParticleSystem>();   // 获取游戏对象上的火焰粒子系统组件

            fireEffectController.ExtinguishFire(fireParticleSystem);    // 调用ExtinguishFire
        }*/
    

}
