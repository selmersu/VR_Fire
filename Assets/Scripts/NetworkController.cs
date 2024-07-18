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

    public GameObject prefabObject; // 预设物体
    private const string GET_URL = "http://192.168.64.137:8081/ship/list";// 接口地址

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
        // 按F执行
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
        UnityWebRequest www = UnityWebRequest.Get(GET_URL);// 调用Get接口

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

            //移动预设物体
            MoveObjects(jsonResponse);
        }
    }

    void MoveObjects(string jsonResponse)
    {
        // 使用SimpleJSON解析JSON字符串
        JSONArray jsonArray = JSON.Parse(jsonResponse).AsArray;

        // 检查解析是否成功并且是否包含对象>0个
        if (jsonArray != null && jsonArray.Count > 0)
        {
            // 遍历数组中的每个对象
            foreach (JSONNode jsonObject in jsonArray)
            {
                // 检查当前对象是否包含必要字段
                if (jsonObject["type"] != null && jsonObject["x"] != null && jsonObject["z"] != null)
                {
                    // 获取name和移动后的坐标值
                    string name = jsonObject["type"];
                    float x = jsonObject["x"].AsFloat;
                    float y = jsonObject["y"].AsFloat;
                    float z = jsonObject["z"].AsFloat;
                    float r = jsonObject["r"].AsFloat;

                    // 根据 name 查找物体，如果不存在就实例化新物体--(bug)
                    GameObject obj = GameObject.Find(name);
                    if (obj == null)
                    {
                        /*obj = Instantiate(prefabObject, new Vector3(x, y, z), Quaternion.identity);
                        obj.name = name;*/
                        continue;
                    }
                    else//预设物体存在
                    {
                        // 更新位置
                        obj.transform.position = new Vector3(x, y, z);
                        //obj.transform.rotation = Quaternion.Euler(0, r, 0);
                        

                    }
                }
                else
                {
                    Debug.LogError("字段中存在空值");
                    debugText.text = "Error: ziduan null";
                }
            }
        }
        else
        {
            Debug.LogError("解析失败");
            debugText.text = "Error: failed";
        }
    }
}
