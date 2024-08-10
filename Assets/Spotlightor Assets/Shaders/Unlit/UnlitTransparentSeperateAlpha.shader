Shader "Unlit/Transparent Seperate Alpha" 
{
    Properties 
    {
        _Color ("Color Tint", Color) = (1,1,1,1)    
        _MainTex ("Base (RGB)", 2D) = "white"
        _AlphaTex ("Alpha(A)", 2D) = "white"
    }

    Category 
    {
        Lighting Off
        //ZWrite Off
        ZWrite On  // uncomment if you have problems like the sprite disappear in some rotations.
        Cull back
        Blend SrcAlpha OneMinusSrcAlpha
                //AlphaTest Greater 0.0001  // uncomment if you have problems like the sprites or 3d text have white quads instead of alpha pixels.
        Tags {Queue=Transparent}

        SubShader 
        {

             Pass 
             {
                        SetTexture [_MainTex] 
                        {
                    ConstantColor [_Color]
                   Combine Texture * constant
                   
                }
                SetTexture [_AlphaTex]
                {
                 ConstantColor [_Color]
                 //Combine Texture, constant
                Combine Previous, Texture* constant
                
                }
            }
        }
    }
}