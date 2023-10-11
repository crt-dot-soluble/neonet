
using OpenTK.Graphics.OpenGL;


namespace neonet.lib.rendering.shader;


/// <summary>
/// Represents a GPU shader object.
/// </summary>
public class Shader
{

    /// <summary>
    /// The Handle, or Program Id, which identifies this shader.
    /// </summary>
    public int Handle { get; private set; }

    /// <summary>
    /// Whether or not the shader has been compiled.
    /// </summary>
    public bool IsCompiled { get; private set; }

    /// <summary>
    /// The source code of the shader.
    /// </summary>
    public ShaderSourceCode SourceCode { get; private set; }


    /// <summary>
    /// Represents the type of shader that is being parsed.
    /// NONE = -1, VERTEX = 0, FRAGMENT = 1
    /// If NONE is returned, then the shader file is invalid.
    /// </summary>
    public enum Type
    {
        NONE = -1, VERTEX = 0, FRAGMENT = 1
    }


    /// <summary>
    /// A structure for mananging the source code of shaders. 
    /// Contains a member for vertex, as well as member. 
    /// </summary>
    public struct ShaderSourceCode
    {
        public string Vertex;
        public string Fragment;
    }


    /// <summary>
    /// Initializes a shader object with the given ShaderSourceCode object.
    /// </summary>
    /// <param name="shaderSourceCode">The source code to be used for the shader.</param>
    /// <param name="shouldCompile">Whether or not the shader should be compiled immediately.</param>
    public Shader(ShaderSourceCode shaderSourceCode, bool shouldCompile = false)
    {
        SourceCode = shaderSourceCode;
        if (shouldCompile)
        {
            if (CompileShader())
            {
                IsCompiled = true;
                Console.WriteLine("Shader Compiled Successfully.");
            }
            else
            {
                // TODO: Handle Error with GL
                Console.WriteLine("Shader Compilation Failed.");
            }
        }
    }


    /// <summary>
    /// Parses a shader file and returns the parsed shader source code as a ShaderSourceCode structure
    /// </summary>
    /// <param name="path">The location of the shader file</param>
    public static ShaderSourceCode Parse(string path)
    {
        var shaderSourceCode = new ShaderSourceCode();
        var shaderType = Type.NONE;


        // Be careful
        foreach (string s in File.ReadAllLines(path))
        {
            if (s.StartsWith("#shader"))
            {
                if (s.Contains("vertex"))
                {
                    shaderType = Type.VERTEX;
                }
                else if (s.Contains("fragment"))
                {
                    shaderType = Type.FRAGMENT;
                }

            }
            else
            {
                switch (shaderType)
                {
                    case Type.VERTEX:
                        shaderSourceCode.Vertex += s + Environment.NewLine;
                        break;
                    case Type.FRAGMENT:
                        shaderSourceCode.Fragment += s + Environment.NewLine;
                        break;
                    case Type.NONE:
                        Console.WriteLine($"Invalid shader file: {path}");
                        break;
                }
            }
        }
        return shaderSourceCode;
    }


    /// <summary>
    /// Compiles the shader from its source code.
    /// </summary>
    /// <returns>Whether or not the shader compilation was successful.</returns>
    public bool CompileShader()
    {
        if (IsCompiled)
        {
            Console.WriteLine("Shader already compiled");
            return true;
        }

        if (!SourceCode.Equals(null))
        {
            // VertexShader compilation 
            var vertexShaderId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderId, SourceCode.Vertex);
            GL.CompileShader(vertexShaderId);
            GL.GetShader(vertexShaderId, ShaderParameter.CompileStatus, out var vertexShaderCompilationCode);
            if (vertexShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine("Vertex Shader Compilation Failed\n");
                Console.WriteLine(GL.GetShaderInfoLog(vertexShaderId));
                return false;
            }

            // FragmentShader compilation
            var fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderId, SourceCode.Fragment);
            GL.CompileShader(fragmentShaderId);
            GL.GetShader(fragmentShaderId, ShaderParameter.CompileStatus, out var fragmentShaderCompilationCode);
            if (fragmentShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine("Fragment Shader Compilation Failed\n");
                Console.WriteLine(GL.GetShaderInfoLog(fragmentShaderId));
                return false;
            }

            // Shader program creation
            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShaderId);
            GL.AttachShader(Handle, fragmentShaderId);

            // Shader program linking
            GL.LinkProgram(Handle);

            // Detach shaders - no longer required once the shader program has been compiled onto the GPU
            GL.DetachShader(Handle, vertexShaderId);
            GL.DetachShader(Handle, fragmentShaderId);

            // Delete the now detached shaders
            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);
        }

        return true;
    }


    /// <summary>
    /// Tells the GPU to use this shader.
    /// </summary>
    public void Use()
    {
        if (!IsCompiled)
        {
            Console.WriteLine("Shader.Use() called on uncompiled shader");
            return;
        }

        GL.UseProgram(Handle);
    }
}