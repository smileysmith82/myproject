using UnityEngine;

public class TranformController : MonoBehaviour

{
    private void Update()
    {
        var x = Mathf.PingPong(Time.time, 3);
        var p = new Vector3(0,x, 0);
        transform.position = p;
        
        transform.Rotate(new Vector3(0,30,0)* Time.deltaTime);
    }
}
