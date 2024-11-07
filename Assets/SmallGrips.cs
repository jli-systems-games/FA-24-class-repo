using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGrips 
{
    Vector3 origin;
    Vector3 direction;
    float slope;
    float intercept;
    //struct
    public SmallGrips(Vector3 origin, Vector3 endPoint)
    {
        this.origin = origin;
        this.direction = endPoint;
        this.slope = (this.origin.y - this.direction.y) / (this.origin.x - this.direction.x);
        this.intercept = this.origin.y - (slope * this.origin.x);


    }


    //Generate an array of the pair locations for 
    public IList<float> GenerateChildrenX()
    {
        List<float> _X = new List<float>();
        
        float y = GenerateChildrenY();
        while (_X.Count < 2)
        {
            //float range = this.direction.y - this.origin.y;
            float _x = (y -this.intercept) / this.slope;
            _x = Random.Range(_x -2f, _x + 2f);

            if (!_X.Contains(_x))
            {
                _X.Add(_x);
            }
        }
        return _X;
    }
    public float GenerateChildrenY()
    {   
        
        float _y = this.origin.y - 2f;
        return _y;

    }

}
