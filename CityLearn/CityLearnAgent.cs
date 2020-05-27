using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEngine.UI;
using System.IO;

using System.Diagnostics;
using System.Threading;

public class CityLearnAgent : Agent
{
	private int max_imgs = 128; // Total #frames for training
	int num_feats = 64; // Feature vector dimension: 64 to 4096
	private bool use_visual = false; // true: raw images; false: feature vectors

	string path_imgs;
	string[] files;
	string path_feats;
	string[] lines;
    int target;
	int current;
    int distance;
	int GlobalDistance;
    int num_imgs;
    public bool maskActions = true;
	private bool show_visual;
	private const int NoAction = 0;
	private const int Next = 1;
	private const int Prev = 2;
	GameObject rawImage;
	WWW www_img;
	float[] feats;
	string pathPreFix = "file:///"; 

	CityLearnAcademy academy;

    public override void InitializeAgent()
    {
		// Dataset/traversal for training (raw images)
		//path_imgs = "/media/marvin/DATA/datasets/Oxford_Robotcar/reference/";
		path_imgs = "/media/marvin/DATA/datasets/CityLearn/Nordland/reference/";
		// If using Windows "D:\\datasets/Oxford_Robotcar/reference/"

		// Check images extension
		files = System.IO.Directory.GetFiles(path_imgs, "*.png");

		// Features path
		path_feats = "/media/marvin/DATA/datasets/CityLearn/features/";

		// Read features from traversal
		lines = System.IO.File.ReadAllLines(path_feats + "nr-1k-netvlad-64.csv");

		academy = FindObjectOfType<CityLearnAcademy>();
		num_imgs = (int)academy.num_i;
		if (use_visual) {
			show_visual = true;
		}
		else { 
			show_visual = false;
		}

		Random.InitState(1000);
		agentParameters.maxStep = num_imgs * 4;

        target = Random.Range(0,num_imgs);
		current = Random.Range(0,num_imgs);
		while (current == target)
		{
			target = Random.Range(0,num_imgs);
		}

        distance = GetDistance();
		GlobalDistance = distance;
		rawImage = GameObject.Find ("RawImage");

        if (show_visual == true)
        {
            Sequence(current);
        }
    }

	public override void AgentReset()
	{
		num_imgs = (int)academy.num_i;

        if (num_imgs <= 64) {
			agentParameters.maxStep = num_imgs * 4;
        }
        if (num_imgs == 128) {
			agentParameters.maxStep = num_imgs * 2;
        }
        if (num_imgs > 128) {
            num_imgs = 128;
            agentParameters.maxStep = num_imgs * 1;
        }

        if (current < 0 || current  >= num_imgs){
			current = Random.Range(0,num_imgs);
		}

		if (show_visual == true){
			Sequence(current);			
		}

		target = Random.Range(0,num_imgs);
		while (current == target){
			target = Random.Range(0,num_imgs);
		}
        distance = GetDistance();
		GlobalDistance = distance;
    }

    int GetDistance(){
		return Mathf.Abs(current - target);
    }

    public override void CollectObservations() {
        if (use_visual == false) {
            // Using feature vectors
			AddVectorObs(Feature(current));
			AddVectorObs((float)target/ max_imgs);

			// Uncomment for baseline agent
			//AddVectorObs((float)current/ max_imgs);
			//AddVectorObs((float)target/ max_imgs);
        }
        else {
			// Using raw images
            AddVectorObs((float)target / num_imgs);
        }
        if (maskActions){
			SetMask();
		}
	}

	private void SetMask(){
		var posAgent = (int) current;
		if (posAgent == 0){
			SetActionMask (Prev);
		}
		if (posAgent == num_imgs-1){
			SetActionMask (Next);
		}
	}

    public override void AgentAction(float[] vectorAction, string textAction)
	{
		int action = Mathf.FloorToInt(vectorAction[0]);

		int prev_dist = GetDistance();

        switch (action){
			case NoAction:
				break;
			case Next:
				current += 1;
				break;
			case Prev:
				current -= 1;
				break;
			default:
				return;
		}

        if (show_visual == true){
            Sequence(current);			
		}

		int distanceToTarget = GetDistance();

        if (distanceToTarget == 0){
            Done();
            AddReward(1f);
        }
        if ((distanceToTarget < prev_dist) && (prev_dist <= GlobalDistance)){
			GlobalDistance -= 1;
		}
		else {
            if (num_imgs <= 64) {
                AddReward(-0.25f / num_imgs);
            }
            if (num_imgs == 128) {
                AddReward(-0.5f / num_imgs);
            }
            if (num_imgs > 128) {
                AddReward(-1.0f / num_imgs);
            }
        }
    }

    void Sequence(int id)
	{
		// For the Nordland dataset (only), use:
		www_img = new WWW(pathPreFix + path_imgs + id + ".png");

		// For other datasets:
		//www_img = new WWW(pathPreFix + files[id + 1]);

		rawImage.GetComponent<RawImage>().texture = www_img.texture;
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}

	float[] Feature(int id)
	{
		feats = new float[num_feats];
		string[] data_values = null;

		data_values = lines[id].Split (',');

		for (int i = 0; i < num_feats; i++){
			feats [i] = float.Parse (data_values [i].ToString ());
		}
		return feats;
	}
}
