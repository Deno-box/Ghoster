inline float2 modulo(float2 value, float2 scale)
{
    return floor((value%scale+scale)%scale);
}

inline float Unity_SimpleNoise_RandomValue_float (float2 uv)
{
    return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453);
}

inline float Unity_SimpleNnoise_Interpolate_float (float a, float b, float t)
{
    return (1.0-t)*a + (t*b);
}

inline float Unity_SimpleNoise_ValueNoise_float (float2 uv, float Period)
{
    float2 i = floor(uv);
    float2 f = frac(uv);
    f = f * f * (3.0 - 2.0 * f);
    // uv = abs(frac(uv) - 0.5);
    float2 c0 = i + float2(0, 0);
    float2 c1 = i + float2(1, 0);
    float2 c2 = i + float2(0, 1);
    float2 c3 = i + float2(1, 1);
    c0 = modulo(c0, Period);
    c1 = modulo(c1, Period);
    c2 = modulo(c2, Period);
    c3 = modulo(c3, Period);
    float r0 = Unity_SimpleNoise_RandomValue_float(c0);
    float r1 = Unity_SimpleNoise_RandomValue_float(c1);
    float r2 = Unity_SimpleNoise_RandomValue_float(c2);
    float r3 = Unity_SimpleNoise_RandomValue_float(c3);
    float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
    float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
    float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
    return t;
}

inline float2 moduloTypeA(float2 value, float2 scale)
{
    return frac(value/scale)*scale;
}
inline float2 moduloTypeB(float2 value, float2 scale)
{
    return (value % scale + scale) % scale;
}

void TiledSimpleNoise_float(float2 UV, float Scale, out float Out)
{
    Scale = floor(Scale/4);
    float t = 0.0;
    {
        float freq = 1;
        float2 offset = float2(0.75, 0.3);
        float2 uv = frac(UV+offset)*(Scale*freq);
        float period = Scale*freq;
        float amp = 0.5;
        t += Unity_SimpleNoise_ValueNoise_float(uv, period)*amp;
    }
    {
        float freq = 2;
        float2 offset = float2(0.5, 0.8);
        float2 uv = frac(UV+offset)*(Scale*freq);
        float period = Scale*freq;
        float amp = 0.25;
        t += Unity_SimpleNoise_ValueNoise_float(uv, period)*amp;
    }
    {
        float freq = 4;
        float2 offset = float2(0.2, 0.9);
        float2 uv = frac(UV+offset)*(Scale*freq);
        float period = Scale*freq;
        float amp = 0.125;
        t += Unity_SimpleNoise_ValueNoise_float(uv, period)*amp;
    }
    Out = t;
}