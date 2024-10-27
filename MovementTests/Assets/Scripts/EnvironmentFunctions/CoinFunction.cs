using UnityEngine;
public class CoinFunction : MonoBehaviour

{
    Vector3 startposition;

    void Start()
    {
        startposition = transform.position;
    }
    private void Update()
        {
            //var x = Mathf.PingPong(t: Time.time, 3);
            //var p = new Vector3 (0, x, 0);
            //transform.position = startposition + p;
            
            transform.Rotate(eulers: new Vector3(0,30, 0) * Time.deltaTime);
          }
}