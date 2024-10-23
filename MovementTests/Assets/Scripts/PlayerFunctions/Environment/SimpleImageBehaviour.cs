using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SimpleImageBehaviour : MonoBehaviour
{
    private Image imageObj;
    public SimpleFloatData dataObj;
    
    private void Start()
    {
        imageObj = GetComponent<Image>(); 
        UpdateWithFloatData();
    }

    public void Update()
    {
        UpdateWithFloatData();
    }
    
    public void UpdateWithFloatData()
    {
        if (dataObj != null)
        {
            float clampedValue = Mathf.Clamp01(dataObj.value);
            Debug.Log("Health Value: " + dataObj.value + ", Clamped Value: " + clampedValue);
            imageObj.fillAmount = clampedValue;
        }
        
    }
}
