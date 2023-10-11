### neonet
An OpenGL based game-engine with an implementation in progress.

#### Current Features:
 - Window Creation
 - Shader Compilation
 - Geometry Rendering
 - Texture Rendering

#### Current State:
As of right now neonet.client as well as neonet.lib stand as a client and library pair that show off
the current implemented features.

### Examples
The neonet.client namespace contains three test implementations
- TriangleTest
- RectangleTest
- TextureTest
Each of which show off the engines ability to render geometry and make use of GPU shaders.

### Compiling and Executing the Code
- Clone the repo: 
  `git clone https://github.com/crt-dot-soluble/neonet.git`
- From the terminal Navigate to the directory you cloned the repository into and run the dotnet build command.
  `dotnet build`
- You can now execute the assembly from the binary file located in client/bin/ or run the following command.
  `dotnet run`
