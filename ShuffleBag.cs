using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShuffleBag<T> : ICollection<T>, IList<T>
{
    private List<T> m_Data = new List<T>();
    private int m_Cursor = 0;
    private T m_LastPicked;

    /// <summary>
    /// Get the next value from the ShuffleBag
    /// </summary>
    public T Next()
    {
        if (m_Cursor < 1)
        {
            m_Cursor = m_Data.Count - 1;

            if (m_Data.Count < 1)
            {
                return default(T);
            }

            return m_Data[0];
        }

        int index = Mathf.FloorToInt(Random.value * (m_Cursor + 1));

        T tempPick = m_Data[index];

        m_Data[index] = this.m_Data[this.m_Cursor];
        m_Data[m_Cursor] = tempPick;
        m_Cursor--;

        return tempPick;
    }

    public ShuffleBag(T[] initialValues)
    {
        for (int i = 0; i < initialValues.Length; i++)
        {
            Add(initialValues[i]);
        }
    }

    public ShuffleBag()
    {

    }

    public int IndexOf(T item)
    {
        return m_Data.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        m_Cursor = m_Data.Count;
        m_Data.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        m_Cursor = m_Data.Count - 2;
        m_Data.RemoveAt(index);
    }

    public T this[int index]
    {
        get
        {
            return m_Data[index];
        }
        set
        {
            m_Data[index] = value;
        }
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return m_Data.GetEnumerator();
    }

    public void Add(T item)
    {
        m_Data.Add(item);
        m_Cursor = m_Data.Count - 1;
    }

    public int Count
    {
        get
        {
            return m_Data.Count;
        }
    }

    public void Clear()
    {
        m_Data.Clear();
    }

    public bool Contains(T item)
    {
        return m_Data.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        foreach (T item in m_Data)
        {
            array.SetValue(item, arrayIndex);
            arrayIndex = arrayIndex + 1;
        }
    }

    public bool Remove(T item)
    {
        m_Cursor = m_Data.Count - 2;
        return m_Data.Remove(item);
    }

    public bool IsReadOnly
    {
        get
        {
            return false;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return m_Data.GetEnumerator();
    }
}