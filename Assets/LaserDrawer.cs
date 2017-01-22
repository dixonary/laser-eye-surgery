using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;
using System.Linq;
using MidiJack;

[RequireComponent (typeof (MeshFilter))]
public class LaserDrawer : MonoBehaviour {
    
    private Queue<Vector2> _queue;
    private int _movingAverageLen = 60;
    private Vector2 _avg;
    private Vector2 _lerpPoint;

    // Use this for initialization
    void Start() {
        _queue = new Queue<Vector2>();
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);

    }


    void FixedUpdate() {
        var _g = EyeTracking.GetGazePoint();
        if (_g.IsValid) {
            var knobOne = 43f / 127;   //MidiMaster.GetKnob(21, 0);
            var knobTwo = 5f / 127;    //MidiMaster.GetKnob(22, 0.5f);
            var knobThree = 35f / 127; //MidiMaster.GetKnob(23, 0.5f);
            var knobFour = 10f / 127;  //MidiMaster.GetKnob(24, 0.25f);

            int actualLen = (int)(1 + 2 * knobTwo * _movingAverageLen);

            var _gp = _g.Viewport;
            _queue.Enqueue(_gp);
            while (_queue.Count > actualLen)
                _queue.Dequeue();

            //Fixing near values
            _avg = new Vector2(_queue.Average(x => x.x), _queue.Average(x => x.y));
            var _dist = _avg - _gp;
            if (_dist.magnitude < knobOne * 0.1) {
                _gp = _avg;
            }

            if (_lerpPoint == null) {
                _lerpPoint = _avg;
            }
            else {
                _lerpPoint += (_avg - _lerpPoint) / 20;
            }


            // update the mesh!
            var centerPoint = new Vector2(0f, -1f);
            var lerpViewpoint = new Vector2((_lerpPoint.x * 2 - 1)*Camera.main.aspect, _lerpPoint.y*2-1);

            var distance = lerpViewpoint - centerPoint;
            var normDist = distance.normalized;
            var farDiam = 0.1f * knobFour * (1 - lerpViewpoint.y * 0.5f);
            var angle = Mathf.Atan2(distance.y, distance.x)-Mathf.PI/2;

            var leftPoint   = new Vector2(centerPoint.x - (0.1f * knobThree)/Mathf.Max(0.2f,Mathf.Cos(angle)), centerPoint.y);
            var rightPoint  = new Vector2(centerPoint.x + (0.1f * knobThree)/ Mathf.Max(0.2f, Mathf.Cos(angle)), centerPoint.y);


            var farRightPoint = new Vector2(lerpViewpoint.x + normDist.y * farDiam, lerpViewpoint.y - normDist.x*farDiam);
            var farLeftPoint = new Vector2(lerpViewpoint.x - normDist.y * farDiam, lerpViewpoint.y + normDist.x * farDiam);
            //var farLeftPoint = new Vector2((_lerpPoint.x*2 - 1) *Camera.main.aspect - (0.1f * knobFour * (1-_lerpPoint.y*0.5f)), (_lerpPoint.y*2-1));
            //var farRightPoint = new Vector2((_lerpPoint.x*2 - 1 )* Camera.main.aspect + (0.1f * knobFour * (1-_lerpPoint.y*0.5f)), (_lerpPoint.y*2-1));

            Vector2[] vertexArray = { leftPoint, rightPoint, farRightPoint, farLeftPoint };
            Vector3[] vertex3d = vertexArray.Select(x => new Vector3(x.x, x.y, 0f)).ToArray();
            int[] indices = { 1, 0, 2, 2, 0, 3 };
            Mesh msh = new Mesh();
            msh.vertices = vertex3d;
            msh.triangles = indices;
            msh.RecalculateNormals();
            msh.RecalculateBounds();
            
            MeshFilter filter = gameObject.GetComponent<MeshFilter>();
            filter.mesh = msh;
        }

    }
}

