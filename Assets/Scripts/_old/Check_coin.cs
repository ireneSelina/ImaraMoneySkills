using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Check_coin : MonoBehaviour {

    public GameObject oneShilling, fiveShillings, tenShillings, twentyShillings, fortyShillings, fiftyCents, 
                    jarOneShillings,jarTenShillings,jarTwentyShillings,jarFortyShillings,jarFiveShillings,jarFiftyCents;

    Vector3 oneShillingIni, fiveShillingsIni, tenShillingsIni, twentyShillingsIni, fortyShillingsIni, fiftyCentsIni;

    string str = "";
    public string word;
 
    public GameObject questionToHide,questionToShow;
    
    bool oneCorrect, twoCorrect, threeCorrect, forCorrect, fiveCorrect= false;

    Vector3 iniScaleoneShilling, iniScalefiveShillings, iniScaletenShillings, iniScaletwentyShillings, iniScalefortyShillings, iniScalefiftyCents;

    public AudioSource source;
    public AudioClip[] correct;
    public AudioClip incorrect;
    public AudioClip buttonDrop;
    public AudioClip reload;

    void Start()
    {

        oneShillingIni    = oneShilling.transform.position;
        fiveShillingsIni    = fiveShillings.transform.position;
        tenShillingsIni  = tenShillings.transform.position;
        twentyShillingsIni    = twentyShillings.transform.position;
        fortyShillingsIni   = fortyShillings.transform.position;
        fiftyCentsIni    = fiftyCents.transform.position;

        iniScaleoneShilling    = oneShilling.transform.localScale;
        iniScalefiveShillings    = fiveShillings.transform.localScale;
        iniScaletenShillings  = tenShillings.transform.localScale;
        iniScaletwentyShillings    = twentyShillings.transform.localScale;
        iniScalefortyShillings   = fortyShillings.transform.localScale;
        iniScalefiftyCents    = fiftyCents.transform.localScale;

       // blockPanel.SetActive(false);

    }




    //*****************************************Drag

    public void DragoneShilling()
    {
        oneShilling.transform.position = Input.mousePosition;
    }
    public void DragfiveShillings()
    {
        fiveShillings.transform.position = Input.mousePosition;
    }
    public void DragtenShillings()
    {
        tenShillings.transform.position = Input.mousePosition;
    }
    public void DragtwentyShillings()
    {
        twentyShillings.transform.position = Input.mousePosition;
    }
    public void DragfortyShillings()
    {
        fortyShillings.transform.position = Input.mousePosition;
    }
    public void DragfiftyCents()
    {
        fiftyCents.transform.position = Input.mousePosition;
    }


    //****************************************Drop


    public void DroponeShilling()
    {
        float Distance = Vector3.Distance(oneShilling.transform.position, jarOneShillings.transform.position);
        float Distance2 = Vector3.Distance(oneShilling.transform.position, jarTenShillings.transform.position);
        float Distance3 = Vector3.Distance(oneShilling.transform.position, jarTwentyShillings.transform.position);
        float Distance4 = Vector3.Distance(oneShilling.transform.position, jarFortyShillings.transform.position);
        float Distance5 = Vector3.Distance(oneShilling.transform.position, jarFiveShillings.transform.position);

        if (Distance<50 && oneCorrect==false)
        {
            //oneShilling.transform.localScale = jarOneShillings.transform.localScale;
            oneShilling.transform.position = jarOneShillings.transform.position;
            oneCorrect = true;
            jarOneShillings.name = oneShilling.name;
            source.clip = buttonDrop;
            source.Play();

        }
        else if(Distance2<50 && twoCorrect==false)
        {
            //oneShilling.transform.localScale = jarTenShillings.transform.localScale;
            oneShilling.transform.position = jarTenShillings.transform.position;
            twoCorrect = true;
            jarTenShillings.name = oneShilling.name;
            source.clip = buttonDrop;
            source.Play();

        }
        else if (Distance3 < 50 && threeCorrect==false)
        {
            //oneShilling.transform.localScale = jarTwentyShillings.transform.localScale;
            oneShilling.transform.position = jarTwentyShillings.transform.position;
            threeCorrect = true;
            jarTwentyShillings.name = oneShilling.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance4 < 50 && forCorrect==false)
        {
            //oneShilling.transform.localScale = jarFortyShillings.transform.localScale;
            oneShilling.transform.position = jarFortyShillings.transform.position;
            forCorrect = true;
            jarFortyShillings.name = oneShilling.name;
            source.clip = buttonDrop;
            source.Play();
        }

        else if (Distance5 < 50 && fiveCorrect == false)
        {
            //oneShilling.transform.localScale = jarFiveShillings.transform.localScale;
            oneShilling.transform.position = jarFiveShillings.transform.position;
            fiveCorrect = true;
            jarFiveShillings.name = oneShilling.name;
            source.clip = buttonDrop;
            source.Play();
        }
 

        else 
        {
            oneShilling.transform.position = oneShillingIni;
            source.clip = reload;
            source.Play();
        }
   
    }



    public void DropfiveShillings()
    {
        float Distance = Vector3.Distance(fiveShillings.transform.position, jarOneShillings.transform.position);
        float Distance2 = Vector3.Distance(fiveShillings.transform.position, jarTenShillings.transform.position);
        float Distance3 = Vector3.Distance(fiveShillings.transform.position, jarTwentyShillings.transform.position);
        float Distance4 = Vector3.Distance(fiveShillings.transform.position, jarFortyShillings.transform.position);
        float Distance5 = Vector3.Distance(fiveShillings.transform.position, jarFiveShillings.transform.position);
        if (Distance < 50 && oneCorrect==false)
        {
            //fiveShillings.transform.localScale = jarOneShillings.transform.localScale;
            fiveShillings.transform.position = jarOneShillings.transform.position;
            oneCorrect = true;
            jarOneShillings.name = fiveShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance2 < 50&&twoCorrect==false)
        {
            //fiveShillings.transform.localScale = jarTenShillings.transform.localScale;
            fiveShillings.transform.position = jarTenShillings.transform.position;
            twoCorrect = true;
            jarTenShillings.name = fiveShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance3 < 50 && threeCorrect==false)
        {
            //fiveShillings.transform.localScale = jarTwentyShillings.transform.localScale;
            fiveShillings.transform.position = jarTwentyShillings.transform.position;
            threeCorrect = true;
            jarTwentyShillings.name = fiveShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance4 < 50 && forCorrect==false)
        {
            //fiveShillings.transform.localScale = jarFortyShillings.transform.localScale;
            fiveShillings.transform.position = jarFortyShillings.transform.position;
            forCorrect = true;
            jarFortyShillings.name = fiveShillings.name;
            source.clip = buttonDrop;
            source.Play();

        }

        else if (Distance5 < 50 && fiveCorrect == false)
        {
            //fiveShillings.transform.localScale = jarFiveShillings.transform.localScale;
            fiveShillings.transform.position = jarFiveShillings.transform.position;
            fiveCorrect = true;
            jarFiveShillings.name = fiveShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }

        else
        {
            fiveShillings.transform.position = fiveShillingsIni;
            source.clip = reload;
            source.Play();
        }

    }


    public void DroptenShillings()
    {
        float Distance = Vector3.Distance(tenShillings.transform.position, jarOneShillings.transform.position);
        float Distance2 = Vector3.Distance(tenShillings.transform.position, jarTenShillings.transform.position);
        float Distance3 = Vector3.Distance(tenShillings.transform.position, jarTwentyShillings.transform.position);
        float Distance4 = Vector3.Distance(tenShillings.transform.position, jarFortyShillings.transform.position);
        float Distance5 = Vector3.Distance(tenShillings.transform.position, jarFiveShillings.transform.position);
        if (Distance < 50 && oneCorrect == false)
        {
            //tenShillings.transform.localScale = jarOneShillings.transform.localScale;
            tenShillings.transform.position = jarOneShillings.transform.position;
            oneCorrect = true;
            jarOneShillings.name = tenShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance2 < 50 && twoCorrect == false)
        {
            //tenShillings.transform.localScale = jarTenShillings.transform.localScale;
            tenShillings.transform.position = jarTenShillings.transform.position;
            twoCorrect = true;
            jarTenShillings.name = tenShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance3 < 50 && threeCorrect == false)
        {
            //tenShillings.transform.localScale = jarTwentyShillings.transform.localScale;
            tenShillings.transform.position = jarTwentyShillings.transform.position;
            threeCorrect = true;
            jarTwentyShillings.name = tenShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance4 < 50 && forCorrect == false)
        {
            //tenShillings.transform.localScale = jarFortyShillings.transform.localScale;
            tenShillings.transform.position = jarFortyShillings.transform.position;
            forCorrect = true;
            jarFortyShillings.name = tenShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance5 < 50 && fiveCorrect == false)
        {
            //tenShillings.transform.localScale = jarFiveShillings.transform.localScale;
            tenShillings.transform.position = jarFiveShillings.transform.position;
            fiveCorrect = true;
            jarFiveShillings.name = tenShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }

        else
        {
            tenShillings.transform.position = tenShillingsIni;
            source.clip = reload;
            source.Play();
        }

    }


    public void DroptwentyShillings()
    {
        float Distance = Vector3.Distance(twentyShillings.transform.position, jarOneShillings.transform.position);
        float Distance2 = Vector3.Distance(twentyShillings.transform.position, jarTenShillings.transform.position);
        float Distance3 = Vector3.Distance(twentyShillings.transform.position, jarTwentyShillings.transform.position);
        float Distance4 = Vector3.Distance(twentyShillings.transform.position, jarFortyShillings.transform.position);
        float Distance5 = Vector3.Distance(twentyShillings.transform.position, jarFiveShillings.transform.position);
        if (Distance < 50 && oneCorrect == false)
        {
            //twentyShillings.transform.localScale = jarOneShillings.transform.localScale;
            twentyShillings.transform.position = jarOneShillings.transform.position;
            oneCorrect = true;
            jarOneShillings.name = twentyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance2 < 50 && twoCorrect == false)
        {
            //twentyShillings.transform.localScale = jarTenShillings.transform.localScale;
            twentyShillings.transform.position = jarTenShillings.transform.position;
            twoCorrect = true;
            jarTenShillings.name = twentyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance3 < 50 && threeCorrect == false)
        {
            //twentyShillings.transform.localScale = jarTwentyShillings.transform.localScale;
            twentyShillings.transform.position = jarTwentyShillings.transform.position;
            threeCorrect = true;
            jarTwentyShillings.name = twentyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance4 < 50 && forCorrect == false)
        {
            //twentyShillings.transform.localScale = jarFortyShillings.transform.localScale;
            twentyShillings.transform.position = jarFortyShillings.transform.position;
            forCorrect = true;
            jarFortyShillings.name = twentyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }

        else if (Distance5 < 50 && fiveCorrect == false)
        {
            //twentyShillings.transform.localScale = jarFiveShillings.transform.localScale;
            twentyShillings.transform.position = jarFiveShillings.transform.position;
            fiveCorrect = true;
            jarFiveShillings.name = twentyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }

        else
        {
            twentyShillings.transform.position = twentyShillingsIni;
            source.clip = reload;
            source.Play();
        }

    }


    public void DropfortyShillings()
    {
        float Distance = Vector3.Distance(fortyShillings.transform.position, jarOneShillings.transform.position);
        float Distance2 = Vector3.Distance(fortyShillings.transform.position, jarTenShillings.transform.position);
        float Distance3 = Vector3.Distance(fortyShillings.transform.position, jarTwentyShillings.transform.position);
        float Distance4 = Vector3.Distance(fortyShillings.transform.position, jarFortyShillings.transform.position);
        float Distance5 = Vector3.Distance(fortyShillings.transform.position, jarFiveShillings.transform.position);
        if (Distance < 50 && oneCorrect == false)
        {
            //fortyShillings.transform.localScale = jarOneShillings.transform.localScale;
            fortyShillings.transform.position = jarOneShillings.transform.position;
            oneCorrect = true;
            jarOneShillings.name = fortyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance2 < 50 && twoCorrect == false)
        {
            //fortyShillings.transform.localScale = jarTenShillings.transform.localScale;
            fortyShillings.transform.position = jarTenShillings.transform.position;
            twoCorrect = true;
            jarTenShillings.name = fortyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance3 < 50 && threeCorrect == false)
        {
            //fortyShillings.transform.localScale = jarTwentyShillings.transform.localScale;
            fortyShillings.transform.position = jarTwentyShillings.transform.position;
            threeCorrect = true;
            jarTwentyShillings.name = fortyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance4 < 50 && forCorrect == false)
        {
            //fortyShillings.transform.localScale = jarFortyShillings.transform.localScale;
            fortyShillings.transform.position = jarFortyShillings.transform.position;
            forCorrect = true;
            jarFortyShillings.name = fortyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }

        else if (Distance5 < 50 && fiveCorrect == false)
        {
            //fortyShillings.transform.localScale = jarFiveShillings.transform.localScale;
            fortyShillings.transform.position = jarFiveShillings.transform.position;
            fiveCorrect = true;
            jarFiveShillings.name = fortyShillings.name;
            source.clip = buttonDrop;
            source.Play();
        }

        else
        {
            fortyShillings.transform.position = fortyShillingsIni;
            source.clip = reload;
            source.Play();
        }

    }


    public void DropfiftyCents()
    {
        float Distance = Vector3.Distance(fiftyCents.transform.position, jarOneShillings.transform.position);
        float Distance2 = Vector3.Distance(fiftyCents.transform.position, jarTenShillings.transform.position);
        float Distance3 = Vector3.Distance(fiftyCents.transform.position, jarTwentyShillings.transform.position);
        float Distance4 = Vector3.Distance(fiftyCents.transform.position, jarFortyShillings.transform.position);
        float Distance5 = Vector3.Distance(fiftyCents.transform.position, jarFiveShillings.transform.position);
        if (Distance < 50 && oneCorrect == false)
        {
            //fiftyCents.transform.localScale = jarOneShillings.transform.localScale;
            fiftyCents.transform.position = jarOneShillings.transform.position;
            oneCorrect = true;
            jarOneShillings.name = fiftyCents.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance2 < 50 && twoCorrect == false)
        {
            //fiftyCents.transform.localScale = jarTenShillings.transform.localScale;
            fiftyCents.transform.position = jarTenShillings.transform.position;
            twoCorrect = true;
            jarTenShillings.name = fiftyCents.name;
            source.clip = buttonDrop;
            source.Play();
        }
        else if (Distance3 < 50 && threeCorrect == false)
        {
            //fiftyCents.transform.localScale = jarTwentyShillings.transform.localScale;
            fiftyCents.transform.position = jarTwentyShillings.transform.position;
            threeCorrect = true;
            source.clip = buttonDrop;
            source.Play();
            jarTwentyShillings.name = fiftyCents.name;
        }
        else if (Distance4 < 50 && forCorrect == false)
        {
            //fiftyCents.transform.localScale = jarFortyShillings.transform.localScale;
            fiftyCents.transform.position = jarFortyShillings.transform.position;
            forCorrect = true;
            jarFortyShillings.name = fiftyCents.name;
            source.clip = buttonDrop;
            source.Play();
        }

        else if (Distance5 < 50 && fiveCorrect == false)
        {
            //fiftyCents.transform.localScale = jarFiveShillings.transform.localScale;
            fiftyCents.transform.position = jarFiveShillings.transform.position;
            fiveCorrect = true;
            jarFiveShillings.name = fiftyCents.name;
            source.clip = buttonDrop;
            source.Play();
        }

        else
        {
            fiftyCents.transform.position = fiftyCentsIni;
            source.clip = reload;
            source.Play();
        }

    }


    //Button

    public void Check()
    {
 

        str = jarFiftyCents.name + jarFiveShillings.name + jarFortyShillings.name + 
            jarTwentyShillings.name + jarTenShillings.name + jarOneShillings.name;

        if(word==str)
        {
            
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            StartCoroutine(LoadNextPanel());
            
        }
        else
        {
            source.clip = incorrect;
            source.Play();
            
        }
 
    }

    public void Reload()
    {
        str = "";

        source.clip = reload;
        source.Play();
        
        oneCorrect = false;
        twoCorrect = false;
        threeCorrect = false;
        forCorrect  = false;

        jarOneShillings.name ="sh1";
        jarTenShillings.name = "sh10";
        jarTwentyShillings.name ="sh20";
        jarFortyShillings.name = "sh40";
        jarFiveShillings.name="sh5";
        jarFiftyCents.name = "50ct";


        oneShilling.transform.position = oneShillingIni;
        fiveShillings.transform.position = fiveShillingsIni;
        tenShillings.transform.position = tenShillingsIni;
        twentyShillings.transform.position = twentyShillingsIni;
        fortyShillings.transform.position = fortyShillingsIni;
        fiftyCents.transform.position = fiftyCentsIni;

        oneShilling.transform.localScale = iniScaleoneShilling;
        fiveShillings.transform.localScale = iniScalefiveShillings;
        tenShillings.transform.localScale = iniScaletenShillings;
        twentyShillings.transform.localScale = iniScaletwentyShillings;
        fortyShillings.transform.localScale = iniScalefortyShillings;
        fiftyCents.transform.localScale = iniScalefiftyCents;
       
    }

    IEnumerator LoadNextPanel()
    {
        yield return new WaitForSeconds(3f);
        questionToHide.SetActive(false);
        questionToShow.SetActive(true);
    }

}
