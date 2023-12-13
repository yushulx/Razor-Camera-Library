using Microsoft.JSInterop;
using System.Text.Json;

namespace RazorCameraLibrary
{
    public class CameraEnhancer : IDisposable
    {
        // Fields to hold JavaScript object references.
        private IJSObjectReference _module;
        private IJSObjectReference _jsObjectReference;
        private List<Camera> _cameras = new List<Camera>();
        private DotNetObjectReference<CameraEnhancer> objRef;
        private bool _disposed = false;

        // Public properties for source dimensions
        public int SourceWidth, SourceHeight;

        /// <summary>
        /// Initializes a new instance of the CameraEnhancer class.
        /// </summary>
        /// <param name="module">A reference to the JavaScript module.</param>
        /// <param name="cameraEnhancer">A reference to the JavaScript CameraEnhancer object.</param>
        public CameraEnhancer(IJSObjectReference module, IJSObjectReference cameraEnhancer)
        {
            _module = module;
            _jsObjectReference = cameraEnhancer;
            objRef = DotNetObjectReference.Create(this);
        }

        /// <summary>
        /// Release unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed == false)
            {
                objRef.Dispose();
                _disposed = true;
            }
        }

        /// <summary>
        /// Destructor for the CameraEnhancer class.
        /// </summary>
        ~CameraEnhancer()
        {
            if (_disposed == false)
                Dispose();
        }

        /// <summary>
        /// Invoked when the size of the video source changes.
        /// </summary>
        /// <param name="width">The new width of the video source.</param>
        /// <param name="height">The new height of the video source.</param>
        [JSInvokable]
        public Task OnSizeChanged(int width, int height)
        {
            SourceWidth = width;
            SourceHeight = height;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Opens a camera.
        /// </summary>
        /// <param name="camera">The camera to be opened.</param>
        public async Task OpenCamera(Camera camera)
        {
            await _module.InvokeVoidAsync("openCamera", _jsObjectReference, camera, objRef, "OnSizeChanged");
        }

        /// <summary>
        /// Closes the current camera.
        /// </summary>
        public async Task CloseCamera()
        {
            await _module.InvokeVoidAsync("closeCamera", _jsObjectReference);
        }

        /// <summary>
        /// Gets a list of available cameras.
        /// </summary>
        /// <returns>A list of available Camera objects.</returns>
        public async Task<List<Camera>> GetCameras()
        {
            _cameras.Clear();
            JsonElement? result = await _module.InvokeAsync<JsonElement>("getCameras", _jsObjectReference);

            if (result != null)
            {
                JsonElement element = result.Value;

                if (element.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in element.EnumerateArray())
                    {
                        Camera camera = new Camera();
                        if (item.TryGetProperty("deviceId", out JsonElement devideIdValue))
                        {
                            string? value = devideIdValue.GetString();
                            if (value != null)
                            {
                                camera.DeviceId = value;
                            }
                        }

                        if (item.TryGetProperty("label", out JsonElement labelValue))
                        {
                            string? value = labelValue.GetString();
                            if (value != null)
                            {
                                camera.Label = value;
                            }
                        }
                        _cameras.Add(camera);
                    }
                }
            }

            return _cameras;
        }

        /// <summary>
        /// Sets a div element as the video container.
        /// </summary>
        /// <param name="elementId">The ID of the div element.</param>
        public async Task SetVideoElement(string elementId)
        {
            await _module.InvokeVoidAsync("setVideoElement", _jsObjectReference, elementId);
        }

        /// <summary>
        /// Acquires a frame from the camera.
        /// </summary>
        /// <returns> A JavaScript canvas object reference.</returns>
        public async Task<IJSObjectReference> AcquireCameraFrame()
        {
            IJSObjectReference jsObjectReference = await _module.InvokeAsync<IJSObjectReference>("acquireCameraFrame", _jsObjectReference);
            return jsObjectReference;
        }

        /// <summary>
        /// Sets the resolution of the camera.
        /// </summary>
        /// <param name="width">The width of the camera.</param>
        /// <param name="height">The height of the camera.</param>
        public async Task SetResolution(int width, int height)
        {
            await _module.InvokeVoidAsync("setResolution", _jsObjectReference, width, height);
        }
    }
}
