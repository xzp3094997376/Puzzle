using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChangeColor : MonoBehaviour
{
    public Transform par;

    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// 初始化资源
    /// </summary>
    /// <param name="gridArr"></param>
     void Init(GridItem[,] gridArr)
    {
        for (int i = 0; i < gridArr.GetLength(0); i++)
        {
            for (int j = 0; j < gridArr.GetLength(1); j++)
            {
                GameObject go= Instantiate(item,par);
                RectTransform rt=go.GetComponent<RectTransform>();
                Vector3 pos = new Vector3(i * rt.rect.width+i*2f, -j * rt.rect.height-j*2f, 0);
                rt.anchoredPosition3D = pos;
                go.name = string.Format("{0},{1}", i, j);
            }
        }
    }

    public void ChangeItemColor(GridItem[,] gridArr,int spliteNum)
    {
        Init(gridArr);

        List<Color> colorList=new List<Color>();
        for (int i = 0; i < spliteNum; i++)
        {
            colorList.Add(RandomColorRGB());
        }

        for (int i = 0; i < spliteNum; i++)
        {
            for (int j = 0; j < gridArr.GetLength(0); j++)
            {

                for (int k = 0; k < gridArr.GetLength(1); k++)
                {
                    if (gridArr[j, k].shapeIndex == i)
                    {
                      string str= string.Format("{0},{1}", j, k);
                      try
                      {
                          par.Find(str).GetComponent<Image>().color = colorList[i];
                        }
                      catch (Exception e)
                      {
                          Debug.Log(str);
                      }
                     
                    }
                }
            }
        }
    }


    private Color RandomColorRGB()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        //float a = Random.Range(0f, 1f);//加透明度
        Color color = new Color(r, g, b);
        //Color color = new Color(r, g, b, a);//有透明通道需要随机的使用该句
        return color;
    }

}
