Shader "Custom/Mask" {
	Properties{
		_MainTex("Base (RGB), Alpha (A)", 2D) = "white" {}
	}

	SubShader{

		Tags{ "Queue" = "Transparent+10" }

		Pass{
		ZWrite on
		//Offset - 1, -1
		ColorMask 0
		Blend SrcAlpha OneMinusSrcAlpha
		}
	}
}
