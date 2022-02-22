using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class PlayerController : MonoBehaviour
{
    [Header("Paw Position:"), SerializeField]
    Vector3[] pawPos;   //For L and R :D
    Camera cam;
    float distance;
    [Header("Move Speed"), Range(0.0f, 50.0f)]
    public float speed = 12.0f;
    bool leftPaw; //If false => right paw!!! xD
    public Vector3 target { get; set; }
    [Header("Paws:")]
    public Paw[] paws;  //Paws to instantiate... :|
    [SerializeField] List<Paw> pawlist = new List<Paw>();    //Fill this in for the later use! C:<

    bool firstMove;
    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        target = transform.position;
        foreach (Transform p in transform)
        {
            if (p.GetComponent<Paw>() != null)
                pawlist.Add(p.GetComponent<Paw>());
        }
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);
#endif
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Fish"))
                {
                    target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(target, transform.position);
        if (distance > 0)
        {
            if (firstMove)
            {
                firstMove = false;
                for (int i = 0; i < pawlist.Count; i++)
                    pawlist[i].HidePaw();
            }
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        }
    }


    IEnumerator<float> _PawControl()
    {
        while(distance > 0)
        {
            leftPaw = !leftPaw;
            //Create a paw... L or R depending on the previous one xD
            //Remember to hide them :\

            //And don't forget to rotate the paw towards the move dir O.O
            yield return Timing.WaitForSeconds(.05f);
        }
    }
}
