﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Paw : MonoBehaviour
{
    public bool lvlComplete { private get; set; }

    public void HidePaw()
    {
        if (!lvlComplete)
            Timing.RunCoroutine(_HidePaw().CancelWith(gameObject));
    }

    IEnumerator<float> _HidePaw()
    {
        yield return Timing.WaitForSeconds(.35f);
        if(!lvlComplete)
            gameObject.SetActive(false);
    }

}
