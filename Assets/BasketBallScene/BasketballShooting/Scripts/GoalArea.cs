using UnityEngine;
using System.Collections;

public class GoalArea : MonoBehaviour {

	public ParticleSystem psStar;
    public AudioSource ballHit;

    int score = 0;
    
    


    

    void OnTriggerEnter (Collider other) {
       

        if (other.CompareTag("Ball"))
        {
            score++;
            GameManager.UpdateScore(score);
           
           ballHit.Play();
            psStar.Play();
        }

       
    }

}


