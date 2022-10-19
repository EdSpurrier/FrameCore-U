using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RuntimeConnector : MonoBehaviour
{
    public bool autoConnect = true;

    [Title("All Connected Event")]
    public FrameCoreEvent allConnectedEvent;

    [System.Serializable]
    public class FollowerConnector
    {
        public Follower follower;

        public bool connectFollower = false;
        [ShowIf("connectFollower")]
        public string followerName = "";
        public bool connectTarget = false;
        [ShowIf("connectTarget")]
        public string targetName = "";

        public FrameCoreEvent onConnectedEvent;
        
        public bool connected = false;
    }

    public List<FollowerConnector> followerConnectors;



    [Title("System")]
    public bool allConnected = false;

    // Start is called before the first frame update
    void Start()
    {
        if (autoConnect)
        {
            Connect();
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (autoConnect)
        {
            Connect();
        };
    }



    public void Connect()
    {
        if (allConnected)
        {
            return;
        };

        allConnected = true;

        foreach (FollowerConnector followerConnectors in followerConnectors)
        {
            if (followerConnectors.connected)
            {
                continue;
            };

            if (followerConnectors.connectTarget)
            {
                GameObject targetObject = GameObject.Find(followerConnectors.targetName);
                if (targetObject)
                {
                    followerConnectors.follower.target = targetObject.transform;
                    followerConnectors.connectTarget = false;
                };
            };

            if (followerConnectors.connectFollower)
            {
                GameObject followerObject = GameObject.Find(followerConnectors.followerName);
                if (followerObject)
                {
                    followerConnectors.follower.follower = followerObject.transform;
                    followerConnectors.connectFollower = false;
                };
            };

            if (!followerConnectors.connectTarget && !followerConnectors.connectFollower)
            {
                followerConnectors.connected = true;
                followerConnectors.follower.Init();
                followerConnectors.onConnectedEvent.Activate();
            };


            if (!followerConnectors.connected)
            {
                allConnected = false;
            };
        };



        if (allConnected)
        {
            allConnectedEvent.Activate();
            this.enabled = false;
        };
    }


}
