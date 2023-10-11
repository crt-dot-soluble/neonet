

using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;

// Because of ambiguous types (same class names, under different naemspaces)
// we will macro each to a custom name.
using WINPixelFormat = System.Drawing.Imaging.PixelFormat;
using OGLPixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

using neonet.lib.rendering;

namespace neonet.lib.management.textures;

public static class TextureFactory
{
    public static Texture2D Load(string textureName)
    {
        var handle = GL.GenTexture();
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);

        using var image = new Bitmap(textureName);

        // When the texture is loaded it must be flipped vertically.
        // This is because OpenGL has the origin in the lower left corner.
        // Whereas an image tyically is though of as having it origin in the upper left corner.
        image.RotateFlip(RotateFlipType.RotateNoneFlipY);

        var data = image.LockBits(
            new Rectangle(0, 0, image.Width, image.Height),
            ImageLockMode.ReadOnly,
            WINPixelFormat.Format32bppArgb);

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, OGLPixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        return new Texture2D(handle);
    }
}