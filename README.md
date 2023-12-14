# Razor Camera Library
A Razor Class Library built using the [Dynamsoft JavaScript Camera Enhancer SDK](https://www.npmjs.com/package/dynamsoft-camera-enhancer), which provides a simple way to integrate camera access into Blazor applications.

## Online Demo
[https://yushulx.me/Razor-Camera-Library/](https://yushulx.me/Razor-Camera-Library/)


## Quick Usage
- Import and initialize the Razor Camera Library.
    
    ```csharp
    @using RazorCameraLibrary
    
    <div id="videoContainer"></div>

    @code {
        private CameraJsInterop? cameraJsInterop;
        private CameraEnhancer? cameraEnhancer;
        
        protected override async Task OnInitializedAsync()
        {
            cameraJsInterop = new CameraJsInterop(JSRuntime);
            await cameraJsInterop.LoadJS();
    
            cameraEnhancer = await cameraJsInterop.CreateCameraEnhancer();
            await cameraEnhancer.SetVideoElement("videoContainer");
        }
    }
    ```

- Get a list of available cameras.

    ```csharp
    List<Camera> cameras = await cameraEnhancer.GetCameras();
    ```
- Set the camera resolution.

    ```csharp
    await cameraEnhancer.SetResolution(640, 480);
    ```
- Open a camera.

    ```csharp
    await cameraEnhancer.OpenCamera(cameras[0]);
    ```
- Acquire a camera frame for image processing.

    ```csharp
    IJSObjectReference canvas = await cameraEnhancer.AcquireCameraFrame();
    ```
- Draw shapes on the camera overlay.

    ```csharp
    await cameraEnhancer.DrawLine(0, 0, 100, 100);
    await cameraEnhancer.DrawText("Hello World", 0, 0);
    ```
- Clear the camera overlay.

    ```csharp
    await cameraEnhancer.ClearOverlay();
    ```

## API

### Camera Class
Represents a camera device with its device ID and label.

### CameraJsInterop Class

- `Task LoadJS()`: Loads and initializes the JavaScript module.
- `Task<CameraEnhancer> CreateCameraEnhancer()`: Creates a new CameraEnhancer instance.

### CameraEnhancer Class 
- `Task<List<Camera>> GetCameras()`: Gets a list of available cameras.
- `Task SetVideoElement(string elementId)`: Sets a div element as the video container.
- `Task OpenCamera(Camera camera)`: Opens a camera.
- `Task CloseCamera()`: Closes the current camera.
- `Task SetResolution(int width, int height)`: Sets the resolution of the camera.
- `Task<IJSObjectReference> AcquireCameraFrame()`: Acquires a frame from the camera and returns a JavaScript canvas object reference.
- `Task DrawLine(int x1, int y1, int x2, int y2)`: Draws a line on the camera overlay.
- `Task DrawText(string text, int x, int y)`: Draws text on the camera overlay.
- `Task ClearOverlay()`: Clears the camera overlay.

## Example
- [Blazor Barcode Scanner](https://github.com/yushulx/Razor-Camera-Library/tree/main/example)
    
    ![Blazor qrcode scanner](https://camo.githubusercontent.com/08a4f5c8b8ebfe043e96180897a684446e115a8338d297f4553d450b091c548a/68747470733a2f2f7777772e64796e616d736f66742e636f6d2f636f6465706f6f6c2f696d672f323032332f31322f72617a6f722d63616d6572612d6c6962726172792d626c617a6f722d71722d7363616e6e65722e706e67)

## Build 

```bash
cd RazorCameraLibrary
dotnet build --configuration Release
```
