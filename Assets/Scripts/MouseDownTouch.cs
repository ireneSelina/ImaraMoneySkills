using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDownTouch : MonoBehaviour
{
    private GameObject lastHitObject;

    private void Update()
    {
        var hit = new RaycastHit2D();


        Debug.Log("MouseDownTouch is called");


        for (int i = 0; i < Input.touchCount; ++i)
        {

            Debug.Log("MouseDownTouch  i < Input.touchCount");


            // Construct a ray from the current touch coordinates
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

            //test2
            
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                var hitObject = hit.transform.gameObject;
                lastHitObject = hitObject;

                hitObject.SendMessage("OnPointerDown");

                Debug.Log("Touch: OnPointerDown successful");

            }
            //test2

            if (Physics2D.Raycast(ray.origin, hit.point))  // ray, out hit))
            {

                var hitObject = hit.transform.gameObject;

                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {

                    lastHitObject = hitObject;

                    hitObject.SendMessage("OnPointerDown");

                    Debug.Log("Touch: OnPointerDown successful");

                }

                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {

                    if (lastHitObject == hitObject)
                    {

                        hitObject.SendMessage("OnPointerUpAsButton");

                        Debug.Log("Touch: OnPointerUpAsButton successful");

                    }

                    hitObject.SendMessage("OnPointerUp");

                    Debug.Log("Touch: OnPointerUp successful");

                    lastHitObject = null;
                }
            }
            else 
            {

                Debug.Log("No objects clicked");

            }

            //if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            //{
            //    // Construct a ray from the current touch coordinates.
            //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        hit.transform.gameObject.SendMessage("OnMouseDown");
            //    }
            //}

            ////test1
            //var hitObject = hit.transform.gameObject;
            //if (Input.GetTouch(i).phase == TouchPhase.Began)
            //{
            //    lastHitObject = hitObject;
            //    hitObject.SendMessage("OnPointerDown");
            //}
            //if (Input.GetTouch(i).phase == TouchPhase.Ended)
            //{
            //    if (lastHitObject == hitObject)
            //    {
            //        hitObject.SendMessage("OnPointerUpAsButton");
            //    }
            //    hitObject.SendMessage("OnPointerUp");
            //    lastHitObject = null;
            //}
            //endof Test1
        }
    }
}
