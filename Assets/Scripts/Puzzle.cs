using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Puzzle : MonoBehaviour
{
    public Grid mGrid;

    public ChangeColor changeColor;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        yield return StartCoroutine(GetSplitShapeArray());

        changeColor.ChangeItemColor(mGrid.gridArray,splitNum);

        for (int i = 0; i < splitNum; i++)
        {
            string str = "";
            for (int j = 0; j < mGrid.gridArray.GetLength(0); j++)
            {
              
                for (int k = 0; k < mGrid.gridArray.GetLength(1); k++)
                {
                    if (mGrid.gridArray[j,k].shapeIndex==i)
                    {
                        str += "[" + mGrid.gridArray[j, k].x + " , " + mGrid.gridArray[j, k].y + "] ,";
                    }
                }
            }
            Debug.Log(str);
        }
    }


    
    private int curNum;
    public int splitNum;
    IEnumerator GetSplitShapeArray()
    {
        curNum = 0;
        int totalNum = mGrid.width * mGrid.height;
        int size = mGrid.width;
        splitNum =size;
        //splitNum = 5;
        //初始化网格
        mGrid.gridArray = new GridItem[mGrid.width, mGrid.height];
        for (int i = 0; i < mGrid.width; i++)
        {
            for (int j = 0; j < mGrid.height; j++)
            {
                int k = i;
                int m = j;
                mGrid.gridArray[i, j] = new GridItem() {x = k, y = m, isUsed = false, shapeIndex = -1};
            }
        }
        //初始化第一个元素
        List<GridItem> seedList = mGrid.GetRandomGridItemArray(splitNum);
        int cycle = 0;
        while (curNum < totalNum)
        {
            int shapeIndex = cycle % splitNum;
            GridItem[] gridArray = Array.FindAll(mGrid.gridArray.ArrayTwo2One(),
                (_grid) => { return _grid.shapeIndex == shapeIndex; });
            if (gridArray.Length == 0)
            {
                GridItem gridItem = seedList[shapeIndex];
                gridItem.isUsed = true; 
                gridItem.shapeIndex = shapeIndex;
                curNum++;
            }
            else if (ShapeExpand(new List<GridItem>(gridArray), shapeIndex))
            {
                curNum++;
            }

            cycle++;

         
            yield return null;
        }
    }

    //找到当前形状所有可扩张点
    bool ShapeExpand(List<GridItem> _temList, int shapeIndex)
    {
        List<GridItem> _gridItemNonFillList = new List<GridItem>();
        foreach (GridItem gridItem in _temList)
        {
            List<GridItem> gridList = GetExpandGrid(gridItem);
            if (gridList.Count > 0)
            {
                _gridItemNonFillList.AddRange(gridList);
            }
        }

        if (_gridItemNonFillList.Count > 0) //随机选择一个点
        {
            int length = _gridItemNonFillList.Count;
            GridItem item = _gridItemNonFillList[Random.Range(0, length - 1)];
            item.isUsed = true;
            item.shapeIndex = shapeIndex;
            //Debug.LogError(item.x + " , " + item.y);s
            return true;
        }
        else
        {
            return false;
        }
    }

    //找到当前点的相邻扩张点
    List<GridItem> GetExpandGrid(GridItem _GridItem)
    {
        List<GridItem> gridList = new List<GridItem>()
        {
            new GridItem() {x = _GridItem.x - 1, y = _GridItem.y},
            new GridItem() {x = _GridItem.x + 1, y = _GridItem.y},
            new GridItem() {x = _GridItem.x, y = _GridItem.y - 1},
            new GridItem() {x = _GridItem.x, y = _GridItem.y + 1},
        };

        for (int i = 0; i < gridList.Count; i++)
        {
            for (int j = 0; j < mGrid.gridArray.GetLength(0); j++)
            {
                for (int k = 0; k < mGrid.gridArray.GetLength(1); k++)
                {
                    if (gridList[i].Equals(mGrid.gridArray[j, k]))
                    {
                        gridList[i] = mGrid.gridArray[j, k];
                    }
                }
            }
        }

        for (int i = gridList.Count - 1; i >= 0; i--)
        {
            if ((gridList[i].x < 0) || (gridList[i].x >= mGrid.width) || (gridList[i].y < 0) ||
                (gridList[i].y >= mGrid.height))
            {
                gridList.RemoveAt(i);
            }
            else if (gridList[i].isUsed)
            {
                gridList.RemoveAt(i);
            }
        }

        //if (gridList.Count==0)s
        //{
        //    Debug.LogWarning( _GridItem.x+", "+_GridItem.y);
        //}
        return gridList;
    }
}

[System.Serializable]
public class Grid
{
    public int width;
    public int height;
    public GridItem[,] gridArray;


    /// <summary>
    /// 得到随机点
    /// </summary>
    /// <returns></returns>
    public List<GridItem> GetRandomGridItemArray(int num)
    {
        List<GridItem> gridItemList = new List<GridItem>();
        foreach (GridItem _item in gridArray.ArrayTwo2One())
        {
            gridItemList.Add(_item);
        }

        List<int> indexList= RandUtil.GetMutexValue(0, gridItemList.Count - 1, num);
        List<GridItem> tempGridItems=new List<GridItem>();
        foreach (var index in indexList)
        {
            tempGridItems.Add(gridItemList[index]);
        }
        return tempGridItems;
    }
}

[System.Serializable]
public class GridItem : IEquatable<GridItem>
{
    public int shapeIndex;
    public int x;
    public int y;
    public bool isUsed;//是否被使用
    //public bool isClosed;//是否周边被封闭

    public bool Equals(GridItem other)
    {
        if (other==null)
        {
            return false;
        }
        return (this.x == other.x) && (this.y == other.y);
    }
}
