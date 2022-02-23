using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Paw : MonoBehaviour
{
    public bool lvlComplete { private get; set; }

    public void HidePaw()
    {
        transform.SetParent(null);
        if (!lvlComplete)
            Timing.RunCoroutine(_HidePaw().CancelWith(gameObject));
    }

    IEnumerator<float> _HidePaw()
    {
        yield return Timing.WaitForSeconds(.35f);
        if(!lvlComplete)
            gameObject.SetActive(false);
    }

    List<GameObject> fingers = new List<GameObject>(); 
    public void ShowPaw()
    {
        if (lvlComplete)
        {
            gameObject.SetActive(true);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (Physics.Raycast(transform.GetChild(i).position, -Vector3.up, out RaycastHit hit, 25.0f))
                    if (hit.transform.CompareTag("Fish"))
                        fingers.Add(transform.GetChild(i).gameObject);

                transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < fingers.Count; i++)
                fingers[i].SetActive(true);
        }
    }

    public void SetChildMat(Material pawMat)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<MeshRenderer>().material = pawMat;
    }
}
