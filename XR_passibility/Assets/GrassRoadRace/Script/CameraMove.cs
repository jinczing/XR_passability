
using UnityEngine;
using System.Collections;
using System.IO;

public class CameraMove : MonoBehaviour {

	public float moveSpeed;
	public GameObject mainCamera;
    public GameObject joyStick;
    //public GameObject stick;
    public GameObject ring;
    public GameObject ringP;
    public GameObject hand;

    private bool isMove = false;
    public int counter = 0;
    private bool isStick = false;

    private Quaternion defaultRotation;

    private bool[] data;
    private float[] ringData;

    public int resolution;
    public float handSize;
    public bool feedback;
    public bool isRight;
    public float proportion;

    private float[] scale;

	// Use this for initialization
	void Start () {
        Random.InitState((int)System.DateTime.Now.Ticks);
        scale = new float[9] {0.55f, 0.7f, 0.85f, 1f, 1.15f, 1.3f, 1.45f, 1.60f, 1.75f };
        
		mainCamera.transform.localPosition = new Vector3 ( 0, 0, 0 );
		mainCamera.transform.localRotation = Quaternion.Euler (18, 180, 0);
        isMove = true;
        counter = 0;
        data = new bool[27]; // 3 hand sizes, 3 resolutions, 10 rings, 2 judgement
        ringData = new float[27];
        //joyStick.SetActive(false);
        //defaultRotation = stick.transform.rotation;
        
        for(int i=0; i<3; i++)
        {
            //int ran = Random.Range(0, scale.Length);
            for(int k=0; k<50; k++)
            {
                int ran = Random.Range(0, scale.Length);
                int ran2 = Random.Range(0, scale.Length);

                float temp = scale[ran];
                scale[ran] = scale[ran2];
                scale[ran2] = temp;

            }
            
            for(int j=0; j<9; j++)
            {
                ringData[9 * i + j] = scale[j];
                GameObject newRing = Instantiate(ring);
                ring.transform.position = new Vector3(-0.250645f * (isRight? 1:-1), 2.438345f, -5 * (9*i + j) + -10);
                ring.transform.localScale = new Vector3(scale[j] * proportion * 0.008f, scale[j] * proportion * 0.008f, scale[j] * proportion * 0.008f);
                
                
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        MoveObj();
        if(Input.GetKeyDown(KeyCode.H))
        {
            hand.SetActive(!hand.activeSelf);
        }
    }

	void FixedUpdate()
	{
		
		
		if (Input.GetKeyDown (KeyCode.A)) {
			ChangeView01();
		}
		
		if (Input.GetKeyDown (KeyCode.S)) {
			ChangeView02();
		}
	}

    public void Move()
    {
        isMove = true;
        counter++;
    }

    public void genRing(float scale = 0.063f)
    {
        GameObject newRing = Instantiate(ring);
        newRing.transform.position = mainCamera.transform.position + new Vector3(-0.2f, -0.3f, -0.4f);
        newRing.transform.localScale = new Vector3(scale, scale, scale);
        
    }

    public void genRingP(float scale = 0.063f)
    {
        GameObject newRing = Instantiate(ringP);
        newRing.transform.position = mainCamera.transform.position + new Vector3(-0.2f, -0.3f, -0.4f);
        newRing.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void genStick()
    {
        GameObject newStick = Instantiate(joyStick);
        joyStick.transform.SetParent(this.transform);
        newStick.transform.position = mainCamera.transform.position + new Vector3(-0.2f, -0.2f, -0.2f);
        
    }


    void MoveObj() {		
			

        if(!isMove)
        {

            if (Input.GetKeyDown(KeyCode.Y))
            {
                data[counter] = true;
                isMove = true;
                counter++;
                print("Y");
            } else if (Input.GetKeyDown(KeyCode.N))
            {
                data[counter] = false;
                isMove = true;
                counter++;
                print("N");
            }

            //transform.Translate(0f, 0f, 0f);
            //joyStick.SetActive(true);
            

            //isStick = true;
            
            

            //while(newStick.transform.localEulerAngles.x <= 20 && newStick.transform.localEulerAngles.x >= -20)
            //{
            //    continue;
            //}
            
            //isMove = true;
                //newStick.transform.localEulerAngles = new Vector3(0, 0, 0);
                
                //stick.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //joyStick.SetActive(false);
                //newStick.transform.rotation = defaultRotation;
            //counter++;
            





        } else
        {
            float moveAmount = Time.smoothDeltaTime * moveSpeed;
            transform.Translate(0f, 0f, moveAmount);

            if (mainCamera.transform.position.z <= (-10 - 5 * counter + 0.55))
            {
                //this.genRing(Random.Range(0.18f, 0.28f)); // the standard size of hand is 0.23 (scale)
                //this.genRing(Random.Range(0.006f, 0.0066f)); // 0.0059 is the minimum
                //genStick();
                //print("stop");
                if (mainCamera.transform.position.z <= -140)
                {
                    string path = Application.dataPath + "/data.txt";
                    //string output = handSize + ", " + resolution + ", " + "feedback" + ", ";
                    string output = "";
                    
                    for(int i=0; i<27; i++)
                    {
                        output += ringData[i] + ", ";
                        
                    }
                    print(output);
                
                //output += "\n\n";
                    for (int i = 0; i < 27; i++)
                    {
                        output += data[i];
                        if (i == 4) {
                            output += ". ";
                        }
                        else
                        {
                            output += ", ";
                        }
                    
                    }
                    print(output);
                    print(path);
                    if (!File.Exists(path))
                        File.WriteAllText(path, output);
                    else
                        File.AppendAllText(path, output);
                }
                isMove = false;

            }
                
        }
	}
    // 59 585 58 575 57
    // 595 6 605 61 615


	void ChangeView01() {
		transform.position = new Vector3 (0, 2, 10);
		// x:0, y:1, z:52
		mainCamera.transform.localPosition = new Vector3 ( -8, 2, 0 );
		mainCamera.transform.localRotation = Quaternion.Euler (14, 90, 0);
	}

	void ChangeView02() {
		transform.position = new Vector3 (0, 2, 10);
		// x:0, y:1, z:52
		mainCamera.transform.localPosition = new Vector3 ( 0, 0, 0 );
		mainCamera.transform.localRotation = Quaternion.Euler ( 19, 180, 0 );
		moveSpeed = -20f;
		
	}
}























