float4x4 World;
float4x4 View;
float4x4 Projection;

float4 ProjectionParams;
float4 ScreenParams;

sampler2D DitherPattern;
float4 DitherPatternSize;

#define Empty float4(0,0,0,0.0)
#define Black float4(0,0,0,1)
#define White float4(1.0,1.0,1.0,1.0)
#define zNear ProjectionParams.y
#define zFar ProjectionParams.z

struct VertexShaderInput {
	float4 Position : POSITION;
};
struct VertexShaderOutput {
	float4 Position : POSITION;
    float4 DepthInfo : TEXCOORD0;
    float4 ScreenPos : TEXCOORD1;
};

float LinearizeDepth(float depth)
{
    float z = depth * 2.0 - 1.0;
    return (2.0 * zNear * zFar) / (zFar + zNear - z * (zFar - zNear));
}
float4 ComputeScreenPos(float4 pos)
{
    float4 screenPos = pos * 0.5;
    
    screenPos.xy = float2(screenPos.x, screenPos.y * ProjectionParams.x) + screenPos.w;
    screenPos.zw = pos.zw;
    
    return screenPos;
}

VertexShaderOutput VertexShaderFunction(VertexShaderInput input) {
	VertexShaderOutput output;
	
	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
    
    output.DepthInfo = output.Position;
    output.ScreenPos = ComputeScreenPos(output.Position);
	
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR 
{	
    
    float depth = LinearizeDepth(input.DepthInfo.z / input.DepthInfo.w) / zFar;
    float depthColor = 1.0 - (depth / 1.0);
    
    float2 screenPos = input.ScreenPos.xy / input.ScreenPos.w;
    float2 pos = (screenPos.xy * ScreenParams.xy * DitherPatternSize.xy) / trunc((ScreenParams.xy / DitherPatternSize.xy));
    pos = pos - trunc(pos);
    pos.y /= ScreenParams.x / ScreenParams.y;
    
    float ditherValue = tex2D(DitherPattern, pos).r;
    float ditherColor = step(ditherValue, depthColor);
    
    //return float4(ditherValue, ditherValue, ditherValue, 1.0);
    clip(depthColor - ditherValue);
    return lerp(Empty, Black, ditherColor);
}

technique Ambient
{
	pass Pass1 {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}