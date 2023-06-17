using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private void OnMouseDown()
    {
        Shaormeria.instance.AddIngredientToShaorma(name);
    }
}
