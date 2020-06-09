using System.Collections;
using System;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    T[] m_items;
    int m_currentItemCount;

    public Heap(int maxHeapSize)
    {
        m_items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = m_currentItemCount;
        m_items[m_currentItemCount] = item;
        SortUp(item);
        m_currentItemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = m_items[0];
        m_currentItemCount--;
        m_items[0] = m_items[m_currentItemCount];
        m_items[0].HeapIndex = 0;
        sortDown(m_items[0]);
        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public int Count
    {
        get
        {
            return m_currentItemCount;
        }
    }

    public bool Contains(T item)
    {
        return Equals(m_items[item.HeapIndex],item);
    }

    void sortDown(T item)
    {
        while(true)
        {
            int childIndexLeft = item.HeapIndex*2 + 1;
            int childIndexRight = item.HeapIndex*2 +2;
            int swapIndex = 0;

            if (childIndexLeft < m_currentItemCount)
            {
                swapIndex = childIndexLeft;
                if(childIndexRight < m_currentItemCount)
                {
                    if (m_items[childIndexLeft].CompareTo(m_items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }
                if(item.CompareTo(m_items[swapIndex]) < 0)
                {
                    Swap(item,m_items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex-1)/2;

        while(true)
        {
            T parentItem = m_items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item,parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex-1)/2;
        }
    }

    void Swap(T itemA, T itemB)
    {
        m_items[itemA.HeapIndex] = itemB;
        m_items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}