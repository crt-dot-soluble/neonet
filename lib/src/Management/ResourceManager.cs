using neonet.lib.management.textures;
using neonet.lib.rendering;

namespace neonet.lib.management;


/// <summary>
/// Utility class for accessing and managings resources such as sprites.
/// Only one ResourceManager instance should exist at a time, and access to the
/// instance is thread-safe, proctected by a lock statement.
/// </summary>
public sealed class ResourceManager
{
    /// <summary>
    /// The instance of the ResourceManager.
    /// </summary>
    private static ResourceManager? _instance = null;
    /// <summary>
    /// ResourceMananger lock object prevents other threads from attempting to access the same resource concurrently.
    /// </summary>
    private static readonly object _lock = new object();
    private IDictionary<string, Texture2D> _textureCache = new Dictionary<string, Texture2D>();


    /// <summary>
    /// Retrieves a thread-safe instance of ResourceManager
    /// </summary>
    public static ResourceManager Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ResourceManager();
                }
                return _instance;
            }
        }
    }


    /// <summary>
    /// Loads a texture from a file into the ResourceManager.
    /// </summary>
    /// <param name="textureName">The name of the texture. i.e. "character"</param>
    /// <returns></returns>
    public Texture2D LoadTexture(string textureName)
    {
        _textureCache.TryGetValue(textureName, out var value);
        if (value != null)
        {
            return value;
        }

        value = TextureFactory.Load(textureName);
        _textureCache.Add(textureName, value);

        return value;
    }
}