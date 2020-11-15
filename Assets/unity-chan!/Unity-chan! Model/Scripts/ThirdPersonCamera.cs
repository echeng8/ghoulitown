//
// Unityちゃん用の三人称カメラ
// 
// 2013/06/07 N.Kobyasahi
// 2020 frolicks 
//

using System;
using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace UnityChan
{
	public class ThirdPersonCamera : MonoBehaviour
	{
		
		public GameObject trackedPlayer; 
		
		public float smooth = 3f;		// カメラモーションのスムーズ化用変数
		Transform standardPos;			// the usual position for the camera, specified by a transform in the game
		Transform frontPos;			// Front Camera locater
		Transform jumpPos;			// Jump Camera locater
	
		// スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
		bool bQuickSwitch = false;	//Change Camera Position Quickly


		private void Start()
		{
			if (PlayerController.localPlayerInstance)
				TrackPlayer(PlayerController.localPlayerInstance.gameObject);
			else 
				PlayerController.OnLocalPlayerInstanceSet.AddListener(TrackPlayer);
		}

		/// <summary>
		/// requires that the player has camPos,  frontPos, and jumpPos transforms as direct children
		/// </summary>
		void TrackPlayer (GameObject obj)
		{

			standardPos = obj.transform.Find("CamPos").transform;
			frontPos =  obj.transform.Find ("FrontPos").transform;
			jumpPos = obj.transform.Find ("JumpPos").transform;

			var transform1 = transform;
			transform1.position = standardPos.position;	
			transform1.forward = standardPos.forward;

			trackedPlayer = obj;
		}
	
		void FixedUpdate ()	// このカメラ切り替えはFixedUpdate()内でないと正常に動かない
		{
			if (!trackedPlayer)
				return;
			
			if (Input.GetButton ("Fire1")) {	// left Ctlr	
				// Change Front Camera
				SetCameraPositionFrontView ();
			} else if (Input.GetButton ("Fire2")) {	//Alt	
				// Change Jump Camera
				SetCameraPositionJumpView ();
			} else {	
				// return the camera to standard position and direction
				SetCameraPositionNormalView ();
			}
		}

		void SetCameraPositionNormalView ()
		{
			if (bQuickSwitch == false) {
				// the camera to standard position and direction
				transform.position = Vector3.Lerp (transform.position, standardPos.position, Time.fixedDeltaTime * smooth);	
				transform.forward = Vector3.Lerp (transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
			} else {
				// the camera to standard position and direction / Quick Change
				transform.position = standardPos.position;	
				transform.forward = standardPos.forward;
				bQuickSwitch = false;
			}
		}
	
		void SetCameraPositionFrontView ()
		{
			// Change Front Camera
			bQuickSwitch = true;
			transform.position = frontPos.position;	
			transform.forward = frontPos.forward;
		}

		void SetCameraPositionJumpView ()
		{
			// Change Jump Camera
			bQuickSwitch = false;
			transform.position = Vector3.Lerp (transform.position, jumpPos.position, Time.fixedDeltaTime * smooth);	
			transform.forward = Vector3.Lerp (transform.forward, jumpPos.forward, Time.fixedDeltaTime * smooth);		
		}
	}
}