#shader vertex
#version 330 core
        
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;
out vec4 vertexColor;

void main()
{
    vertexColor = vec4(aColor.x, aColor.y, aColor.z, 1.0);
    gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);
};



#shader fragment
#version 330 core

out vec4 color;
in vec4 vertexColor;

void main()
{
    color = vertexColor; 
};