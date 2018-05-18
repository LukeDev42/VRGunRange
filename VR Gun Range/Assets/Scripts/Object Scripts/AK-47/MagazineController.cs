using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineController : MonoBehaviour
{

    public int magInventory = 30;
    public Text bulletInventory;

    private void Start()
    {
        UpdateMagInventory(0);
    }

    public void UpdateMagInventory(int bulletShot)
    {
        magInventory -= bulletShot;
        bulletInventory.text = "" + magInventory;
    }

}
