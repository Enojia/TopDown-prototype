using System;

[Serializable]
public class IntRange  //class to return a random int between 2 numbers
{
    public int m_Min;
    public int m_Max;
	
    public IntRange(int min, int max)
    {
        m_Min = min;
        m_Max = max;
    }

    public int Random
    {
        get
        {
            return UnityEngine.Random.Range(m_Min, m_Max);
        }
    }
}
