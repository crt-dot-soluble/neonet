#shader vertex
#version 330 core

layout (location = 0) in vec3 aPos;

// vec2 - texture is a 2d element (X, Y)
layout (location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main()
{   
    texCoord = aTexCoord;
    gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);
}



#shader fragment
#version 330 core

out vec4 color;
in vec2 texCoord;

// sampler2D is case sensitive // See OpenTk/OpenGL Docs
uniform sampler2D texture0;

void main()
{
    color = texture(texture0, texCoord); 
}