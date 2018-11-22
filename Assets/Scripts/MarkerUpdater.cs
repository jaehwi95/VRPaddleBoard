using UnityEngine;
using System.Collections.Generic;
using PhaseSpace;

//
// Class for drawing markers, rigids, and cameras
//

public class MarkerUpdater : MonoBehaviour
{
    public OWLTracker tracker;
    protected List<GameObject> Markers = new List<GameObject>();
    public string device;
    public bool slave;
    public GameObject paddleboard;
    public int scale;
    private int markerNum;
    public GameObject planeObj;
    Plane plane;
    Quaternion orig;
    public float turningRate = 30.0F;
    private Vector3 prevVectorOnPlane;
    private Vector3 vectorOnPlane;
    public float angleMultiplier;
    private List<ResponsiveSmooth> responsiveSmoothList = new List<ResponsiveSmooth>();
    private List<List<Vector3>> averagePos = new List<List<Vector3>>(3);

    public float maxRawX = -1000F;
    public float minRawX = 1000F;
    public float maxRawY = -1000F;
    public float minRawY = 1000F;
    public float maxRawZ = -1000F;
    public float minRawZ = 1000F;
    public int averageLength = 16;
    private Vector3 origUp;

    public bool useSmooth = false;

    public bool stopX = false;
    public bool stopY = false;
    public bool stopZ = false;

    public int origNumPlanes;
    void Start()
    {
        origUp = planeObj.transform.up;
        for(int i = 0; i < 9; i++)
        {
            responsiveSmoothList.Add(new ResponsiveSmooth());
        }
        for(int i = 0; i < 3; i++)
        {
            averagePos.Add(new List<Vector3>(averageLength));   
        }
        orig = planeObj.transform.localRotation;
        print("Original rotation: " + orig.eulerAngles);
        plane = new Plane();
        // connect to device
        //Debug.Log("connected:" + tracker.Connect(device, slave, false));
        //Debug.Log("slave:" + slave);
        markerNum = 3;
        if (tracker.Connect(device, slave, false))
        {
            Debug.Log("connected");
            if (!slave)
            {
                Debug.Log("not slave");
                // create default point tracker
                int n = 128;
                int[] leds = new int[n];
                for (int i = 0; i < n; i++)
                    leds[i] = i;
                tracker.CreatePointTracker(0, leds);
            }

            // start streaming
            tracker.StartStreaming();
        }
    }

    public GameObject MarkerPrefab;

    void Update()
    {
        PhaseSpace.Marker[] markers = tracker.GetMarkers();

        while (Markers.Count < markers.Length)
        {
            print(System.String.Format("new marker: {0}", Markers.Count));
            GameObject obj = null;
            Markers.Add(obj);
        }
        List<Marker> validMarkers = new List<Marker>();
        for (int i = 0; i < Markers.Count; i++)
        {
            if (markers[i].position != Vector3.zero)
            {
                validMarkers.Add(markers[i]);
            }
        }
        int index = 1;
        if (validMarkers.Count >= 3)
        {
            if (validMarkers[index].position.x < minRawX)
            {
                minRawX = validMarkers[index].position.x;

            }
            if (validMarkers[index].position.x > maxRawX)
            {
                maxRawX = validMarkers[index].position.x;
            }
            if (validMarkers[index].position.y < minRawY)
            {
                minRawY = validMarkers[index].position.y;
            }
            if (validMarkers[index].position.y > maxRawY)
            {
                maxRawY = validMarkers[index].position.y;
            }
            if (validMarkers[index].position.z < minRawZ)
            {
                minRawZ = validMarkers[index].position.z;
            }
            if (validMarkers[index].position.z > maxRawZ)
            {
                maxRawZ = validMarkers[index].position.z;
            }

            //plane = getAveragePlane(validMarkers);
            //float multiplier = 90;
            //int ind = 0;
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        float rawValue = validMarkers[i].position[j];
            //        bool negative = rawValue < 0;
            //        rawValue *= multiplier;
            //        if (negative)
            //        {
            //            rawValue *= -1;
            //        }
            //        print("rawValue(" + ind + "): " + rawValue);
            //        responsiveSmoothList[ind].update(rawValue);
            //        float finalValue = responsiveSmoothList[ind].getValue();

            //        print("finalValue(" + ind + "): " + finalValue);
            //        finalValue /= multiplier;
            //        if(negative)
            //        {
            //            finalValue *= -1;
            //        }
            //        validMarkers[i].position[j] = finalValue;
            //        ind += 1;
            //    }

            //}

            plane.Set3Points(validMarkers[0].position, validMarkers[1].position, validMarkers[2].position);
            Vector3 normal = plane.normal;
            if (normal.y < 0)
            {
                plane.Flip();
            }
            prevVectorOnPlane = vectorOnPlane;
            vectorOnPlane = validMarkers[1].position - validMarkers[2].position;
            //normal = planeObj.transform.InverseTransformDirection(normal);
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, origUp) * Quaternion.FromToRotation(transform.forward, normal);
            
            //targetRotation.z = 0;
            //Quaternion targetRotation = Quaternion.LookRotation(normal) * orig;
            ////planeObj.transform.localRotation = Quaternion.Lerp(Quaternion.identity, targetRotation, rot);
            ////smoothing:
            ////planeObj.transform.localRotation = targetRotation;
            Vector3 euler = targetRotation.eulerAngles;
            euler.x = angleMultiplier * euler.x;
            euler.y = angleMultiplier * euler.y;
            euler.z = angleMultiplier * euler.z;
            //print(euler.x);
            //print(euler.y);
            ////print(euler.z);
            targetRotation = Quaternion.Euler(euler);
            if (useSmooth)
            {
                planeObj.transform.localRotation = Quaternion.RotateTowards(planeObj.transform.localRotation, targetRotation, turningRate * Time.deltaTime);
            }
            else
            {
                planeObj.transform.localRotation = targetRotation;
            }

        }
    }
    private Plane getAveragePlane(List<Marker> markerList)
    {
        int n = markerList.Count;
        int numPossibilities = (int)PermutationsAndCombinations.nCr(n, 3);
        int numPlanes = Mathf.Min(numPossibilities, origNumPlanes);
        return new Plane();
    }
    public class ResponsiveSmooth
    {   
        public bool edgeSnapEnable = false;
        public bool sleepEnable = false;
        public float activityThreshold = 0.1F;
        public float analogResolution = 1024F;
        public float smoothValue;
        float errorEMA = 0;
        bool sleeping = false;
        public float snapMultiplier = 0.01F;
        float prevResponsiveValue;
        float responsiveValue;
        bool responsiveValueHasChanged;
        public float getValue() { return responsiveValue; }
        public void update(float rawValue)
        {
            prevResponsiveValue = responsiveValue;
            responsiveValue = getResponsiveValue(rawValue);
            responsiveValueHasChanged = responsiveValue != prevResponsiveValue;
        }
        public int getResponsiveValue(float newValue)
        {
            // if sleep and edge snap are enabled and the new value is very close to an edge, drag it a little closer to the edges
            // This'll make it easier to pull the output values right to the extremes without sleeping,
            // and it'll make movements right near the edge appear larger, making it easier to wake up
            if (sleepEnable && edgeSnapEnable)
            {
                if (newValue < activityThreshold)
                {
                    newValue = (newValue * 2) - activityThreshold;
                }
                else if (newValue > analogResolution - activityThreshold)
                {
                    newValue = (newValue * 2) - analogResolution + activityThreshold;
                }
            }

            // get difference between new input value and current smooth value
            float diff = Mathf.Abs(newValue - smoothValue);

            // measure the difference between the new value and current value
            // and use another exponential moving average to work out what
            // the current margin of error is
            errorEMA += ((newValue - smoothValue) - errorEMA) * 0.4F;

            // if sleep has been enabled, sleep when the amount of error is below the activity threshold
            if (sleepEnable)
            {
                // recalculate sleeping status
                sleeping = Mathf.Abs(errorEMA) < activityThreshold;
            }

            // if we're allowed to sleep, and we're sleeping
            // then don't update responsiveValue this loop
            // just output the existing responsiveValue
            if (sleepEnable && sleeping)
            {
                return (int)smoothValue;
            }

            // use a 'snap curve' function, where we pass in the diff (x) and get back a number from 0-1.
            // We want small values of x to result in an output close to zero, so when the smooth value is close to the input value
            // it'll smooth out noise aggressively by responding slowly to sudden changes.
            // We want a small increase in x to result in a much higher output value, so medium and large movements are snappy and responsive,
            // and aren't made sluggish by unnecessarily filtering out noise. A hyperbola (f(x) = 1/x) curve is used.
            // First x has an offset of 1 applied, so x = 0 now results in a value of 1 from the hyperbola function.
            // High values of x tend toward 0, but we want an output that begins at 0 and tends toward 1, so 1-y flips this up the right way.
            // Finally the result is multiplied by 2 and capped at a maximum of one, which means that at a certain point all larger movements are maximally snappy

            // then multiply the input by SNAP_MULTIPLER so input values fit the snap curve better.
            float snap = snapCurve(diff * snapMultiplier);

            // when sleep is enabled, the emphasis is stopping on a responsiveValue quickly, and it's less about easing into position.
            // If sleep is enabled, add a small amount to snap so it'll tend to snap into a more accurate position before sleeping starts.
            if (sleepEnable)
            {
                snap *= 0.5F + 0.5F;
            }

            // calculate the exponential moving average based on the snap
            smoothValue += (newValue - smoothValue) * snap;

            // ensure output is in bounds
            if (smoothValue < 0.0F)
            {
                smoothValue = 0.0F;
            }
            else if (smoothValue > analogResolution - 1)
            {
                smoothValue = analogResolution - 1;
            }

            // expected output is an integer
            return (int)smoothValue;
        }

        public float snapCurve(float x)
        {
            float y = 1.0F / (x + 1.0F);
            y = (1.0F - y) * 2.0F;
            if (y > 1.0F)
            {
                return 1.0F;
            }
            return y;
        }

        public void setSnapMultiplier(float newMultiplier)
        {
            if (newMultiplier > 1.0F)
            {
                newMultiplier = 1.0F;
            }
            if (newMultiplier < 0.0F)
            {
                newMultiplier = 0.0F;
            }
            snapMultiplier = newMultiplier;
        }
    }

    public static class PermutationsAndCombinations
    {
        public static long nCr(int n, int r)
        {
            // naive: return Factorial(n) / (Factorial(r) * Factorial(n - r));
            return nPr(n, r) / Factorial(r);
        }

        public static long nPr(int n, int r)
        {
            // naive: return Factorial(n) / Factorial(n - r);
            return FactorialDivision(n, n - r);
        }

        private static long FactorialDivision(int topFactorial, int divisorFactorial)
        {
            long result = 1;
            for (int i = topFactorial; i > divisorFactorial; i--)
                result *= i;
            return result;
        }

        private static long Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }
    }
}
