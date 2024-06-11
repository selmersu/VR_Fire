using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class FireEffectController : MonoBehaviour
{
    
    public XRRayInteractor rayInteractor;

    private ParticleSystem fireParticleSystem;    // 火焰粒子系统组件(从RM中获取)
    private bool isConfigured = false;            // 是否已经对粒子系统进行了配置

    private float extinguishDuration = 5f;    // 灭火过程持续的时间
    private float timer;    // 内部计时器

    public void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();    //获取XR Ray Interactor组件
    }

    // 开始灭火过程
    public void ExtinguishFire(ParticleSystem fire)
    {
        fireParticleSystem = fire;

        if (!isConfigured)
        {
            ConfigureParticleSystem();
            isConfigured = true;
        }

        timer = extinguishDuration;     // 初始化计时器
        enabled = true;     // 启动灭火过程
    }

    void Update()
    {
        if (fireParticleSystem == null)
        {
            // 如果 fireParticleSystem 为空，不执行任何操作，直接返回
            return;
        }

        // 随时间减少粒子的发射速率
        var emission = fireParticleSystem.emission;
        var rateOverTime = emission.rateOverTime;
        rateOverTime.constant = Mathf.Lerp(0, 100, timer / extinguishDuration);
        emission.rateOverTime = rateOverTime;

        timer -= Time.deltaTime;    // 减少计时器

        // 如果计时器<=0，则停止粒子系统
        if (timer <= 0)
        {
            StopFire();
        }
    }

    // 停止火焰
    private void StopFire()
    {
        if (fireParticleSystem != null)
        {
            fireParticleSystem.Stop();
            fireParticleSystem = null; // 将 fireParticleSystem 设置为 null
            isConfigured = false;       // 重置配置标志
        }
        enabled = false; // 禁用 Update 方法
    }

    // 配置粒子系统
    private void ConfigureParticleSystem()
    {
        var sizeOverLifetime = fireParticleSystem.sizeOverLifetime;
        sizeOverLifetime.enabled = true;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, fireParticleSystem.transform.localScale.magnitude); // 在lifetime开始时，大小为当前大小
        curve.AddKey(1.0f, 0.0f); // 在lifetime结束时，大小为0

        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1.0f, curve);

        // 停止粒子系统的发射
        var emission = fireParticleSystem.emission;
        emission.enabled = false;
    }

    public void GetParticleSystem(GameObject obj)
    {
        //获取传入的 GameObject 的 ParticleSystem 组件
        ParticleSystem fireParticleSystem = obj.GetComponent<ParticleSystem>();

        if (fireParticleSystem != null)
        {
            // 如果获取到了 ParticleSystem 组件，调用 ExtinguishFire 方法
            ExtinguishFire(fireParticleSystem);
        }
    }

    public void GetBoXCollider(GameObject obj)
    {
        BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            //如果获取到了 BoxCollider 组件，调用 CloseCollider 方法
            CloseCollider(boxCollider);
        }
    }

    // 在延迟后禁用 BoxCollider
    private IEnumerator DisableColliderAfterDelay(BoxCollider boxCollider, float delay)
    {
        yield return new WaitForSeconds(delay);
        boxCollider.enabled = false;
    }
    public void CloseCollider(BoxCollider boxCollider)
    {
        StartCoroutine(DisableColliderAfterDelay(boxCollider, 4f));
    }
}
