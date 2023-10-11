using neonet.lib;
using neonet.lib.rendering.shader;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace neonet.client;

/// <summary>
/// NeoNet game/client - Extends GameEngine, the core neonet GameEngine class
/// </summary>
internal class RectangleTest : GameEngine
{
    private readonly float[] _vertices =
    {
        // Positions (X, Y, Z)      // Color (RGB)
         0.5f,   0.5f,   0.0f,      0.0f, 0.0f, 1.0f,   // BLUE     - TOP RIGHT
         0.5f,  -0.5f,   0.0f,      0.0f, 1.0f, 0.0f,   // GREEN    - BOTTOM RIGHT      - SHARED
        -0.5f,  -0.5f,   0.0f,      1.0f, 0.0f, 0.0f,   // RED      - BOTTOM LEFT
        -0.5f,   0.5f,   0.0f,      1.0f, 1.0f, 1.0f,   // WHITE    - TOP LEFT          - SHARED
    };


    private uint[] _indices =
    {
        0, 1, 3,    // Triangle 1
        1, 2, 3,    // Triangle 2
    };

    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private int _elementBufferObject;
    private Shader _shader;


    // Call base class GameEngine Contructor to inherit its details
    public RectangleTest(string title, int width, int height) : base(title, width, height)
    {
    }


    // Call base class GameEngine.Initialize()
    protected override void Initialize()
    {
    }


    // Call base class GameEngine.LoadContent()
    protected override void LoadContent()
    {
        var shaderSourceCode = Shader.Parse("./res/shaders/Default.glsl");
        _shader = new Shader(shaderSourceCode, true);

        // Generates a new vertex buffer object and stores it
        _vertexBufferObject = GL.GenBuffer();

        // Bind the newly created VBO to the BufferTarget.ArrayBuffer target. 
        // This means that subsequent OpenGL commands related to buffer operations will operate 
        // on this specific VBO.
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

        // Allocates memory on the GPU for the VBO and fills it with vertex data from the _vertices array.
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        // generates a Vertex Array Object (VAO) using GL.GenVertexArray(). 
        // A VAO is used to store and manage the state associated with vertex attribute arrays.
        _vertexArrayObject = GL.GenVertexArray();

        // binds the VAO to the current OpenGL context. Subsequent calls to OpenGL functions for 
        // vertex attribute pointers and enabling attributes will be associated with this VAO
        GL.BindVertexArray(_vertexArrayObject);

        // Set up the vertex attribute pointer. It tells OpenGL how to interpret the data stored in the VBO.
        // The vertices being the stride 0 to 3 (stride of 3)
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

        // Enable the vertex attribute at index 0.This means that attribute 0 can be used in the vertex shader for rendering.
        GL.EnableVertexAttribArray(0);

        // Set up the vertex attribute pointer, the actual vertices. It tells OpenGL how to interpret the data stored in the VBO.
        // The colors being the stride 4 to 6 (stride of 3)
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

        // Enable the vertex attribute at index 1, the RGB color data. This means that attribute 1 can be used in the vertex shader for rendering.
        GL.EnableVertexAttribArray(1);

        _elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);


    }


    // Call base class GameEngine.Render()
    protected override void Render(GameTime gameTime)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(Color.Black);

        _shader.Use();

        GL.BindVertexArray(_vertexArrayObject);

        // Draw the triangleGL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

        // Total time since the LAST render (the time it takes to redraw the window)
        // Console.WriteLine("Render Time: (ms): " + gameTime.ElapsedTime.TotalMilliseconds);
    }


    // Call base class GameEngine.Update()
    protected override void Update(GameTime gameTime)
    {
        // Total time since the LAST update (the time it takes to complete an update)
        // Console.WriteLine("Update Time: (ms): " + gameTime.ElapsedTime.TotalMilliseconds);
    }
}