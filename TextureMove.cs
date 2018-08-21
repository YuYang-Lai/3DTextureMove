using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureMove : MonoBehaviour
{
	public float MoveSpeed = 0.5f;
	[System.Serializable]
	public struct TextureParameter
	{
		public bool CanMove;
		public Texture TattoTexture;
		public Vector2 RecordOffsetPosition;
		public Vector2 MaxPosition, MinPosition;
		public Color InitializationColor;
		public Color RecordColor;
		[Range(0, 360)]
		public int UVRotation;
	}
	[System.Serializable]
	public struct BodyPart
	{
		public TextureParameter[] mTextureParameter;
	}
	public BodyPart[] mBodyPart;
	private int ControllingPart = 0;
	private int ControllingTexture = 0;
	private Material ControllingMaterial = null;


	void Start()
	{

	}

	void Update ()
	{
		if (MoveBool)
		{
			float Rotation = mBodyPart[ControllingPart].mTextureParameter[ControllingTexture].UVRotation;
			if (UpButton)
			{

			}
			if (DButton)
			{
				Rotation += 180;
			}
			if (LButton)
			{
				Rotation += 270;
			}
			if (RButton)
			{
				Rotation += 90;
			}
			Vector2 MoveTmp = new Vector2(- MoveSpeed * Mathf.Sin(Rotation * Mathf.Deg2Rad) * Time.deltaTime,
										  - MoveSpeed * Mathf.Cos(Rotation * Mathf.Deg2Rad) * Time.deltaTime);
			MovingTexture(MoveTmp);
		}
	}

	private void MovingTexture(Vector2 MoveTmp)
	{
		Vector2 LastPosition = ControllingMaterial.mainTextureOffset;

		ControllingMaterial.mainTextureOffset += MoveTmp;
		
		if (ControllingMaterial.mainTextureOffset.x >= mBodyPart[ControllingPart].mTextureParameter[ControllingTexture].MaxPosition.x)
		{
			ControllingMaterial.mainTextureOffset = LastPosition;
		}
		else if (ControllingMaterial.mainTextureOffset.x <= mBodyPart[ControllingPart].mTextureParameter[ControllingTexture].MinPosition.x)
		{
			ControllingMaterial.mainTextureOffset = LastPosition;
		}
		else if (ControllingMaterial.mainTextureOffset.y >= mBodyPart[ControllingPart].mTextureParameter[ControllingTexture].MaxPosition.y)
		{
			ControllingMaterial.mainTextureOffset = LastPosition;
		}
		else if (ControllingMaterial.mainTextureOffset.y <= mBodyPart[ControllingPart].mTextureParameter[ControllingTexture].MinPosition.y)
		{
			ControllingMaterial.mainTextureOffset = LastPosition;
		}
	}
	
	public void TextureMoveHDown(int Btn)
	{
		switch(Btn)
		{
			case 1:
				UpButton = true;
				break;
			case 2:
				DButton = true;
				break;
			case 3:
				LButton = true;
				break;
			case 4:
				RButton = true;
				break;
		}
		MoveBool = true;
	}
	public void TextureMoveUp()
	{
		UpButton = false;DButton = false;LButton = false;RButton = false;
		MoveBool = false;
	}

	public void SwicthControllingTattoo(int DetaNumber, int ArrayNumber, Material TargetMaterial)
	{
		if (ControllingMaterial != null)
		{
			mBodyPart[ControllingPart].mTextureParameter[ControllingTexture].RecordColor = ControllingMaterial.color;
			mBodyPart[ControllingPart].mTextureParameter[ControllingTexture].RecordOffsetPosition = ControllingMaterial.mainTextureOffset;
		}

		ControllingMaterial = TargetMaterial;
		ControllingPart = DetaNumber;
		ControllingTexture = ArrayNumber;

		TargetMaterial.mainTexture = mBodyPart[DetaNumber].mTextureParameter[ArrayNumber].TattoTexture;
		TargetMaterial.color = mBodyPart[DetaNumber].mTextureParameter[ArrayNumber].RecordColor;
		TargetMaterial.mainTextureOffset = mBodyPart[DetaNumber].mTextureParameter[ArrayNumber].RecordOffsetPosition;

		HmoveTmp = TargetMaterial.mainTextureOffset.x;
		VmoveTmp = TargetMaterial.mainTextureOffset.y;
	}

	public void Initialization()
	{
		ControllingMaterial.color = mBodyPart[ControllingPart].mTextureParameter[ControllingTexture].InitializationColor;
		ControllingMaterial.mainTextureOffset = Vector2.zero;
	}

	public void InitializationAll()
	{
		Initialization();
		for (int BodyPartCount = 0; BodyPartCount < mBodyPart.Length; BodyPartCount++)
		{
			for (int ElementCount = 0; ElementCount < mBodyPart[BodyPartCount].mTextureParameter.Length; ElementCount++)
			{
				mBodyPart[BodyPartCount].mTextureParameter[ElementCount].RecordColor = mBodyPart[BodyPartCount].mTextureParameter[ElementCount].InitializationColor;
				mBodyPart[BodyPartCount].mTextureParameter[ElementCount].RecordOffsetPosition = Vector2.zero;
			}
		}
	}

	private float HmoveTmp = 0.0f;
	private float VmoveTmp = 0.0f;
	private bool UpButton, DButton, LButton, RButton;
	private bool MoveBool;
}
