using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HSValeu : MonoBehaviour
{
    [SerializeField] Slider sliderRed, sliderBlue, sliderGreen;
    [SerializeField] TMP_InputField textRed, textBlue, textGreen;
    [SerializeField] Image mostrarColor;
    private Color colorValue;

    void Start()
    {
        sliderRed.value = 1;
        sliderBlue.value = 1;
        sliderGreen.value = 1;
    }

    public void OnRedChange()
    {
        //Aqui muda o valor do Hue;
        textRed.text = sliderRed.value.ToString("F2");
    }

    public void OnBlueChange()
    {
        //Aqui altera o valor da Saturacao
        textBlue.text = sliderBlue.value.ToString("F2");
    }

    public void OnGreenChange()
    {
        //Aqui altera o valor do Value
        textGreen.text = sliderGreen.value.ToString("F2");
    }

    void FixedUpdate()
    {
        colorValue = new Color(sliderRed.value, sliderGreen.value, sliderBlue.value);

        mostrarColor.color = colorValue;

        Painting.colorPaint = colorValue;
    }
}
