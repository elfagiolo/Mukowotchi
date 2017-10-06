using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Medikament : Therapie
{
    private List<int> counts;
    public List<int> Counts
    {
        get
        {
            return counts;
        }
    }

	private int imageIndex;

	private float r;
	private float g;
	private float b;

	public Color Color
	{
		get 
		{
			return new Color (r, g, b);
		}
		set 
		{
			r = value.r;
			g = value.g;
			b = value.b;
		}
	}

    public Medikament()
    {
        weekdays = new bool[7];
        times = new List<float>();
        counts = new List<int>();
    }

    public Medikament(string _name, string _description, bool[] _weekdays, List<float> _times, int _imageIndex, Color _color, List<int> _counts):this()
    {
        therapieName = _name;
        description = _description;
        _weekdays.CopyTo(weekdays, 0);
        times.AddRange(_times);
		imageIndex = _imageIndex;
		Color = _color;
        counts.AddRange(_counts);
    }

    public void AddCount(int count)
    {
        counts.Add(count);
    }

    public void RemoveCountAt(int index)
    {
        counts.RemoveAt(index);
    }
}
