


using OpenTK.Graphics.ES11;

namespace neonet.lib.rendering;

public class Texture2D : IDisposable
{
    public int Handle { get; private set; }
    private bool _disposed;


    public Texture2D(int handle)
    {
        Handle = handle;
    }

    ~Texture2D()
    {
        Dispose(false);
    }

    public void Use()
    {
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, Handle); // Bind the texture to this texture unit (0)
    }

    public void Dispose(bool disposing)
    {
        if (!disposing)
        {
            GL.DeleteTexture(Handle);
            _disposed = true;
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}