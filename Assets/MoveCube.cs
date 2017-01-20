using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;
using System.Linq;

[RequireComponent(typeof(GazeAware))]
public class MoveCube : MonoBehaviour {

    private GazeAware _gazeAware;
    private Queue<Vector2> _queue;
    private int _movingAverageLen = 20;
    private Vector2 _avg;
    // Use this for initialization
    void Start ()
    {
        _gazeAware = GetComponent<GazeAware>();
        _queue = new Queue<Vector2>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        var _g = EyeTracking.GetGazePoint();
        if (_g.IsValid)
        {
            var _gp = _g.Viewport;
            _queue.Enqueue(_gp);
            if (_queue.Count > _movingAverageLen)
            {
                var _oldgp = _queue.Dequeue();

            }
            var _avg = new Vector2(0, 0);
            var expDecayFactor = 5;
            foreach(var elem in _queue)
            {
                _avg.x += elem.x * expDecayFactor;
                _avg.y += elem.y * expDecayFactor;
            }
            _avg /= expDecayFactor;
            _avg = new Vector2(_queue.Average(x => x.x), _queue.Average(y => y.y));

            this.transform.position = new Vector3((_avg.x * 2 - 1) * Camera.current.aspect, _avg.y * 2 - 1);
            Debug.Log(this.transform.position);
        }
        
    }
}
