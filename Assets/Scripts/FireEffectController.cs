using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class FireEffectController : MonoBehaviour
{
    
    public XRRayInteractor rayInteractor;

    private ParticleSystem fireParticleSystem;    // ��������ϵͳ���(��RM�л�ȡ)
    private bool isConfigured = false;            // �Ƿ��Ѿ�������ϵͳ����������

    private float extinguishDuration = 5f;    // �����̳�����ʱ��
    private float timer;    // �ڲ���ʱ��

    public void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();    //��ȡXR Ray Interactor���
    }

    // ��ʼ������
    public void ExtinguishFire(ParticleSystem fire)
    {
        fireParticleSystem = fire;

        if (!isConfigured)
        {
            ConfigureParticleSystem();
            isConfigured = true;
        }

        timer = extinguishDuration;     // ��ʼ����ʱ��
        enabled = true;     // ����������
    }

    void Update()
    {
        if (fireParticleSystem == null)
        {
            // ��� fireParticleSystem Ϊ�գ���ִ���κβ�����ֱ�ӷ���
            return;
        }

        // ��ʱ��������ӵķ�������
        var emission = fireParticleSystem.emission;
        var rateOverTime = emission.rateOverTime;
        rateOverTime.constant = Mathf.Lerp(0, 100, timer / extinguishDuration);
        emission.rateOverTime = rateOverTime;

        timer -= Time.deltaTime;    // ���ټ�ʱ��

        // �����ʱ��<=0����ֹͣ����ϵͳ
        if (timer <= 0)
        {
            StopFire();
        }
    }

    // ֹͣ����
    private void StopFire()
    {
        if (fireParticleSystem != null)
        {
            fireParticleSystem.Stop();
            fireParticleSystem = null; // �� fireParticleSystem ����Ϊ null
            isConfigured = false;       // �������ñ�־
        }
        enabled = false; // ���� Update ����
    }

    // ��������ϵͳ
    private void ConfigureParticleSystem()
    {
        var sizeOverLifetime = fireParticleSystem.sizeOverLifetime;
        sizeOverLifetime.enabled = true;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, fireParticleSystem.transform.localScale.magnitude); // ��lifetime��ʼʱ����СΪ��ǰ��С
        curve.AddKey(1.0f, 0.0f); // ��lifetime����ʱ����СΪ0

        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1.0f, curve);

        // ֹͣ����ϵͳ�ķ���
        var emission = fireParticleSystem.emission;
        emission.enabled = false;
    }

    public void GetParticleSystem(GameObject obj)
    {
        //��ȡ����� GameObject �� ParticleSystem ���
        ParticleSystem fireParticleSystem = obj.GetComponent<ParticleSystem>();

        if (fireParticleSystem != null)
        {
            // �����ȡ���� ParticleSystem ��������� ExtinguishFire ����
            ExtinguishFire(fireParticleSystem);
        }
    }

    public void GetBoXCollider(GameObject obj)
    {
        BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            //�����ȡ���� BoxCollider ��������� CloseCollider ����
            CloseCollider(boxCollider);
        }
    }

    // ���ӳٺ���� BoxCollider
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
