using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;
using System.Linq;
using MidiJack;
using Eppy;

[RequireComponent(typeof(GazeAware))]
public class MoveCube : MonoBehaviour
{
    
    private Queue<Vector2> _queue;
    private int _movingAverageLen = 60;
    private Vector2 _avg;
    // Use this for initialization
    void Start() {
        _queue = new Queue<Vector2>();
        this.transform.position = new Vector3(0f,0f,1f);
    }


    void FixedUpdate() {
        var _g = EyeTracking.GetGazePoint();
        if (_g.IsValid) {
            var knobOne = 43f / 127; //MidiMaster.GetKnob(21, 0);
            var knobTwo = 5f / 127; //MidiMaster.GetKnob(22, 0.5f);

            int actualLen = (int)(1 + 2 * knobTwo * _movingAverageLen);

            var _gp = _g.Viewport;
            _queue.Enqueue(_gp);
            while (_queue.Count > actualLen)
                _queue.Dequeue();

            /* // Exponential Decay
            var _avg = new Vector2(0, 0);
            float _expDecayFactor = 1;
            float _totalFactor = 0;
            foreach (var elem in _queue) {
                _avg += elem * _expDecayFactor;
                _totalFactor += _expDecayFactor;
                _expDecayFactor *= 1 + knobOne;
            }
            _avg /= _totalFactor;
            */

            /*// K-Means Clustering
            if (_queue.Count >= 2) {
                var clusters = KMeansCluster(_queue.ToList(), 2);
                Vector2 _avg = new Vector2();
                foreach (var c in clusters) {
                    _avg += c.Item1 * c.Item2;
                }
                
            }*/

            //Fixing near values
            var _avg = new Vector2(_queue.Average(x => x.x), _queue.Average(x => x.y));
            var _dist = _avg - _gp;
            if(_dist.magnitude < knobOne * 0.1) {
                _gp = _avg;
            }

            this.transform.position += (new Vector3((_avg.x * 2 - 1) * Camera.main.aspect, _avg.y * 2 - 1, 1f) - this.transform.position) / 20;
            //this.transform.position = new Vector3((_avg.x * 2 - 1) * Camera.main.aspect, _avg.y * 2 - 1, Camera.main.nearClipPlane);

            if (EyeTracking.GetUserPresence().IsUserPresent) {
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
            }
            else {
                gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
            }
        }

    }

    //returns the means of data clustering.
    List<Tuple<Vector2, float>> KMeansCluster(List<Vector2> data, int numClusters) {
        // INITIALISATION: pick "k" data points at random
        var means = new List<Vector2>();
        var prevMeans = new List<Vector2>();
        List<List<Vector2>> partitions = new List<List<Vector2>>();

        var _bin = new List<Vector2>(data);
        for (var i = 0; i < numClusters; i++) {
            var _elem = _bin.ElementAt((int)(Random.value * _bin.Count));
            means.Add(_elem);
            _bin.Remove(_elem);
        }

        foreach (var i in means) {
            var l = new List<Vector2>();
            l.Add(i);
            partitions.Add(l);
        }

        while (true) {
            // ASSIGNMENT: assign points to these means
            foreach (var elem in data) {
                int closestMean = 0;
                float minDist = Mathf.Infinity;
                for (var m = 0; m < means.Count; m++) {
                    var dist = (elem - means[m]).magnitude;
                    if (dist < minDist) {
                        minDist = dist;
                        closestMean = m;
                    }
                }
                partitions[closestMean].Add(elem);
            }

            // UPDATE: recalculate means based on these partitions
            means = new List<Vector2>();
            foreach (var p in partitions) {
                var a = new Vector2(p.Average(x => x.x), p.Average(y => y.y));
                means.Add(a);
            }

            // LEAVE: Check if the partition has changed
            if (means.SequenceEqual(prevMeans)) break;
            prevMeans = means;
            partitions = new List<List<Vector2>>();

            foreach (var i in means) {
                var l = new List<Vector2>();
                partitions.Add(l);
            }
        }

        var res = new List<Tuple<Vector2,float>>();
        for(var i=0; i<means.Count; i++) {
            res.Add(new Tuple<Vector2, float>(means[i], (float) partitions[i].Count / data.Count));
        }
        return res;
    }
}
