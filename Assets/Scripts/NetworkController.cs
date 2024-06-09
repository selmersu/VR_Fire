using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;

public class NetworkController : MonoBehaviour
{
    public GameObject prefabObject; // Ԥ������
    private const string GET_URL = "http://localhost:8081/ship/list";// �ӿڵ�ַ

    void Update()
    {
        // ��Fִ��
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(GetCommand());
        }
    }


    IEnumerator GetCommand()
    {
        UnityWebRequest www = UnityWebRequest.Get(GET_URL);// ����Get�ӿ�

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            Debug.Log("receive json response: " + jsonResponse);

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
                // ��鵱ǰ�����Ƿ���������ֶ�
                if (jsonObject["name"] != null && jsonObject["x"] != null 
                    && jsonObject["y"] != null && jsonObject["z"] != null && jsonObject["r"] != null)
                {
                    // ��ȡname���ƶ��������ֵ
                    string name = jsonObject["name"];
                    float x = jsonObject["x"].AsFloat;
                    float y = jsonObject["y"].AsFloat;
                    float z = jsonObject["z"].AsFloat;
                    float r = jsonObject["r"].AsFloat;

                    // ���� name �������壬��������ھ�ʵ����������--(bug)
                    GameObject obj = GameObject.Find(name);
                    if (obj == null)
                    {
                        obj = Instantiate(prefabObject, new Vector3(x, y, z), Quaternion.identity);
                        obj.name = name;
                    }
                    else//Ԥ���������
                    {
                        // ����λ��
                        obj.transform.position = new Vector3(x, y, z);
                        obj.transform.rotation = Quaternion.Euler(0, r, 0);

                    }
                }
                else
                {
                    Debug.LogError("�ֶ��д��ڿ�ֵ");
                }
            }
        }
        else
        {
            Debug.LogError("����ʧ��");
        }
    }
}
