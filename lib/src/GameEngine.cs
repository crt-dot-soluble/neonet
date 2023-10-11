using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.ES11;

namespace neonet.lib;


/// <summary>
/// Core GameEngine class.
/// </summary>
public abstract class GameEngine
{

    protected string WindowTitle { get; set; }
    protected int InitialWindowWidth { get; set; }
    protected int InitialWindowHeight { get; set; }


    private GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
    private NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;


    /// <summary>
    /// Initializes a new instance of the <see cref="GameEngine"/> class.
    /// </summary>
    /// <param name="windowTitle">The title of the window.</param>
    /// <param name="initialWindowWidth">The initial width of the window</param>
    /// <param name="initialWindowHeight">The initial height of the window</param>
    public GameEngine(string windowTitle, int initialWindowWidth, int initialWindowHeight)
    {
        WindowTitle = windowTitle;
        InitialWindowWidth = initialWindowWidth;
        InitialWindowHeight = initialWindowHeight;

        _nativeWindowSettings.Size = new Vector2i(InitialWindowWidth, InitialWindowHeight);
        _nativeWindowSettings.Title = WindowTitle;
    }


    /// <summary>
    /// Starts the game/client.
    /// </summary>
    public void Run()
    {
        Initialize();
        using GameWindow gameWindow = new GameWindow(_gameWindowSettings, _nativeWindowSettings);
        var gameTime = new GameTime();
        gameWindow.Load += LoadContent;

        gameWindow.UpdateFrame += (FrameEventArgs eventArgs) =>
        {
            var time = eventArgs.Time;
            gameTime.ElapsedTime = TimeSpan.FromMilliseconds(time);
            gameTime.TotalTime += TimeSpan.FromMilliseconds(time);
            Update(gameTime);
        };

        gameWindow.RenderFrame += (FrameEventArgs eventArgs) =>
        {
            Render(gameTime);
            gameWindow.SwapBuffers();
        };


        // Hook into the resize event and handle the user resizing the window, by resizing the ViewPort
        gameWindow.Resize += (ResizeEventArgs eventArgs) =>
        {
            GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y);
        };

        gameWindow.Run();
    }



    protected abstract void Initialize();
    protected abstract void LoadContent();
    protected abstract void Update(GameTime gameTime);
    protected abstract void Render(GameTime gameTime);
}
