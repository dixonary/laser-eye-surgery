using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(LineRenderer))]
public class Wound : MonoBehaviour {

    public string partName;
    //MeshFilter _meshFilter;
    LineRenderer _line;
    float _baseLineWidth = 0.01f;
    float _maxSplitsPerUnitLength = 2f;
    float _maxWobbleAmount = 1f;
    float _minimumWoundLength = 2f;

    Dictionary<string, List<Vector3>> _baseLines = new Dictionary<string, List<Vector3>>()
    {
        { "leg", new List<Vector3>()
            {
                new Vector3(-3f, 2.8f),
                new Vector3(1.5f, -1.5f)
            }
        },
        { "arm", new List<Vector3>
            {
                new Vector3(-0.5f, -2f),
                new Vector3(-2.4f, 0.7f),
                new Vector3(2.8f,3.3f)
            }
        }
    };

    // temp
    List<Vector3> lastShorten;

    void Start () {
        //_meshFilter = GetComponent<MeshFilter>();
        _line = GetComponent<LineRenderer>();

        _line.numPositions = _baseLines[partName].Count;
        _line.startColor = _line.endColor = Color.black;
        _line.SetPositions(wobblify(shorten(_baseLines[partName])).ToArray());
        _line.widthMultiplier = _baseLineWidth;
        _line.useWorldSpace = false;
	}

    // Return a random shortening of a linestrip
    List<Vector3> shorten(List<Vector3> lineStrip)
    {
        var result = new List<Vector3>();
        System.Random r = new System.Random();

        // calculate length of linestrip
        float totalLength = 0;
        float[] lengths = new float[lineStrip.Count - 1];
        for (int i = 0; i < lineStrip.Count - 1; ++i)
        {
            lengths[i] = (lineStrip[i + 1] - lineStrip[i]).magnitude;
            totalLength += lengths[i];
        }

        // find new start and end points
        var newLength = (totalLength - _minimumWoundLength) * (float)r.NextDouble() + _minimumWoundLength;
        float totalStart = (totalLength - newLength) * (float)r.NextDouble();
        var totalEnd = totalStart + newLength;

        float lengthStart = 0;
        for (int i = 0; i < lineStrip.Count - 1; ++i)
        {
            var lengthEnd = lengthStart + lengths[i];
            var p = lineStrip[i]; var q = lineStrip[i + 1];
            if (lengthStart < totalStart && lengthEnd >= totalStart)
            {
                // start point is on this edge
                var ratio = (totalStart - lengthStart) / lengths[i];
                result.Add(p + (q - p) * ratio);
            }
            if (lengthStart >= totalStart && lengthStart <= totalEnd)
            {
                // a mid-point starts this edge
                result.Add(p);
            }
            if (lengthStart < totalEnd && lengthEnd >= totalEnd)
            {
                // end point is on this edge
                var ratio = (totalEnd - lengthStart) / lengths[i];
                result.Add(p + (q - p) * ratio);
            }
            lengthStart = lengthEnd;
        }
        return result;
    }

    // Randomly split and move edges of a line strip
    List<Vector3> wobblify(List<Vector3> lineStrip)
    {
        var result = new List<Vector3>();
        System.Random r = new System.Random();

        Debug.Log("Num Edges: " + (lineStrip.Count-1).ToString());
        for (int i = 0; i < lineStrip.Count - 1; ++i)
        {
            var p = lineStrip[i]; var q = lineStrip[i + 1];
            var l = q - p;
            var length = l.magnitude;
            var perp = new Vector3(-l.y, l.x).normalized;

            int numPoints = 2 + r.Next(0,(int)Math.Round(length*_maxSplitsPerUnitLength));
            float pointSpacingRatio = 1f / (numPoints - 1f);

            Debug.Log("Num Points: " + numPoints.ToString());
            // add first to n-1th point
            for (int j = 0; j < numPoints - 1; ++j)
            {
                var pos = p + (pointSpacingRatio * (j + 0.5f*((float)r.NextDouble() - 0.5f))) * l;
                pos += perp * _maxWobbleAmount * (float)r.NextDouble();
                result.Add(pos);
            }
            
        }
        // hack to stop results with 1 point
        if (result.Count == 1)
        {
            result.Add(lineStrip[lineStrip.Count - 1]);
        }

        return result;
    }

    Mesh properHorrorshow(List<Vector3> s)
    {
        return new Mesh();
    }
	
	void Update () {
        _line.widthMultiplier = _baseLineWidth * transform.localScale.x;

        /* Wound generation testing */
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var result = shorten(_baseLines[partName]);
            _line.numPositions = result.Count;
            _line.SetPositions(result.ToArray());
            lastShorten = result; // temp
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && lastShorten != null)
        {
            var result = wobblify(lastShorten);
            _line.numPositions = result.Count;
            _line.SetPositions(result.ToArray());
        }
        */
	}
    
}