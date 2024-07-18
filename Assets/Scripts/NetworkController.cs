using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;
using UnityEngine.XR;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class NetworkController : MonoBehaviour
{
    public AStarPathFind aStarPathFind;

    public GameObject prefabObject; // Ԥ������
    private const string GET_URL = "http://192.168.64.137:8081/ship/list";// �ӿڵ�ַ

    private InputDevice rightController;
    private InputDevice leftController;


    public TMP_Text debugText;

    private void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void Update()
    {
        // ��Fִ��
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(GetCommand());
        }
        
        if (leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool lTPressed) && lTPressed)
        {
            StartCoroutine(GetCommand());
        }
    }


    public IEnumerator GetCommand()
    {
        UnityWebRequest www = UnityWebRequest.Get(GET_URL);// ����Get�ӿ�

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);

            debugText.text = "Error: " + www.error;
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            Debug.Log("receive json response: " + jsonResponse);

            debugText.text = "Received JSON: " + jsonResponse;

            //�ƶ�Ԥ������
            MoveObjects(jsonResponse);
        }
    }

    void MoveObjects(string jsonResponse)
    {
        // ʹ��SimpleJSON����JSON�ַ���
        JSONArray jsonArray = JSON.Parse(jsonResponse).AsArray;

        // �������Ƿ�ɹ������Ƿ��������>0��
        if (jsonArray != null && jsonArray.Count > 0)
        {
            // ���������е�ÿ������
            foreach (JSONNode jsonObject in jsonArray)
            {
                // ��鵱ǰ�����Ƿ������Ҫ�ֶ�
                if (jsonObject["type"] != null && jsonObject["x"] != null && jsonObject["z"] != null)
                {
                    // ��ȡname���ƶ��������ֵ
                    string name = jsonObject["type"];
                    float x = jsonObject["x"].AsFloat;
                    float y = jsonObject["y"].AsFloat;
                    float z = jsonObject["z"].AsFloat;
                    float r = jsonObject["r"].AsFloat;

                    // ���� name �������壬��������ھ�ʵ����������--(bug)
                    GameObject obj = GameObject.Find(name);
                    if (obj == null)
                    {
                        /*obj = Instantiate(prefabObject, new Vector3(x, y, z), Quaternion.identity);
                        obj.name = name;*/
                        continue;
                    }
                    else//Ԥ���������
                    {
                        // ����λ��
                        obj.transform.position = new Vector3(x, y, z);
                        //obj.transform.rotation = Quaternion.Euler(0, r, 0);
                        

                    }
                }
                else
                {
                    Debug.LogError("�ֶ��д��ڿ�ֵ");
                    debugText.text = "Error: ziduan null";
                }
            }
        }
        else
        {
            Debug.LogError("����ʧ��");
            debugText.text = "Error: failed";
        }
    }
}
