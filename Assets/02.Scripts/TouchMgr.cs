using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TouchMgr : MonoBehaviour
{
    private Camera ARCamera;
    public GameObject placeObject;
    private bool isExist = false;
    void Start()
    {
        ARCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (isExist == false)
        {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            TrackableHit hit;

            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (touch.phase == TouchPhase.Began
                && Frame.Raycast(touch.position.x
                                , touch.position.y
                                , raycastFilter
                                , out hit))
            {
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                GameObject obj = Instantiate(placeObject
                                            , hit.Pose.position
                                            , Quaternion.identity
                                            , anchor.transform);

                var rot = Quaternion.LookRotation(ARCamera.transform.position
                                                    - hit.Pose.position);

                obj.transform.rotation = Quaternion.Euler(ARCamera.transform.position.x
                                                            , rot.eulerAngles.y
                                                            , ARCamera.transform.position.z);

                isExist = true;
            }
        }
    }
}
