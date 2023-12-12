using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetUIController : MonoBehaviour
{
    public Image mentalImage, happinessImage, energyImage;

    public static PetUIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("More than one PetUIController in the Scene");

    }

    public void UpdateImages(int mental, int happiness, int energy)
    {
        mentalImage.fillAmount = (float)mental / 100;
        happinessImage.fillAmount = (float)happiness / 100;
        energyImage.fillAmount = (float)energy / 100;
    }

}
