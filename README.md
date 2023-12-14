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


## Example
- [Blazor Barcode Scanner](https://github.com/yushulx/Razor-Camera-Library/tree/main/example)
    
    ![Blazor qrcode scanner](https://www.dynamsoft.com/codepool/img/2023/12/razor-camera-library-blazor-qr-scanner.png)

## Build 

```bash
cd RazorCameraLibrary
dotnet build --configuration Release
```
