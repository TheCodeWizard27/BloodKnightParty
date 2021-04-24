float4x4 World;
float4x4 View;
float4x4 Projection;

#define AmbientColor float4(1,1,1,1)
#define AmbientIntensity 0.1
#define zNear 0.1
#define zFar 100


struct VertexShaderInput {
	float4 Position : POSITION;
};
struct VertexShaderOutput {
	float4 Position : POSITION;
    float4 DepthInfo : TEXCOORD0;
};

float LinearizeDepth(float depth)
{
    float z = depth * 2.0 - 1.0; // back to NDC 
    return (2.0 * zNear * zFar) / (zFar + zNear - z * (zFar - zNear));
}

VertexShaderOutput VertexShaderFunction(VertexShaderInput input) {
	VertexShaderOutput output;
	
	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
    //output.DepthInfo = float4(0, 0, input.z, 0);
    output.DepthInfo = output.Position;
	
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR 
{	
    float depth = LinearizeDepth(input.DepthInfo.z / input.DepthInfo.w) / zFar;
    float depthColor = 1.0 - (depth / 1.0);
    return float4(depthColor, depthColor, depthColor, 1.0);
    //return float4(1.0, 1.0, 1.0, 1.0);
}

technique Ambient
{
	pass Pass1 {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}