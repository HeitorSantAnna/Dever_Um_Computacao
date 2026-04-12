using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Troca : MonoBehaviour
{
    [SerializeField] List<GameObject> ChangeOBJ = new List<GameObject>();

    [SerializeField] List<Color> ChangeImg = new List<Color>();

    [SerializeField] Image image;

    [SerializeField] int mostrarO = 0;
    [SerializeField] int mostrarI = 0;

    private void Start()
    {
        for(int i = 0; i < Painting.colors.Count; i++)
        {
            if (Painting.colors[i].a == 1)
            {
                ChangeImg.Add(Painting.colors[i]);
            }
        }

        for(int i = 0; i < ChangeOBJ.Count; i++)
        {
            if (i != mostrarO)
            {
                ChangeOBJ[i].SetActive(false);
            }
            else
            {
                ChangeOBJ[i].SetActive(true);
            }
        }
    }

    public void ChangeTDown()
    {
        mostrarO--;

        if (mostrarO < 0)
        {
            mostrarO = ChangeOBJ.Count - 1;
        }

        for (int i = 0; i < ChangeOBJ.Count; i++)
        {
            if (i != mostrarO)
            {
                ChangeOBJ[i].SetActive(false);
            }
            else
            {
                ChangeOBJ[i].SetActive(true);
            }
        }
    }

    public void ChangeTUp()
    {
        mostrarO++;

        if (mostrarO >= ChangeOBJ.Count)
        {
            mostrarO = 0;
        }

        for (int i = 0; i < ChangeOBJ.Count; i++)
        {
            if (i != mostrarO)
            {
                ChangeOBJ[i].SetActive(false);
            }
            else
            {
                ChangeOBJ[i].SetActive(true);
            }
        }
    }

    public void ChangeIDown()
    {
        mostrarI--;

        if (mostrarI < 0)
        {
            mostrarI = ChangeImg.Count - 1;
        }

        image.color = ChangeImg[mostrarI];
    }

    public void ChangeIUp()
    {
        mostrarI++;

        if (mostrarI >= ChangeImg.Count)
        {
            mostrarI = 0;
        }

        image.color = ChangeImg[mostrarI];
    }

    public void Submit()
    {
        Criar.OBJtroca.Add(ChangeOBJ[mostrarO]);
        Criar.Colortroca.Add(ChangeImg[mostrarI]);
    }
}
