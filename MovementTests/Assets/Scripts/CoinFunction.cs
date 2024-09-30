using UnityEngine;
public class CoinFunction : MonoBehaviour

{
    private void Update()
        {
            var x = Mathf.PingPong(t: Time.time, 3);
            var p = new Vector3 (0, x, 0);
            transform.position = p;
            
            transform.Rotate(eulers: new Vector3(0,30, 0) * Time.deltaTime);
          }
}