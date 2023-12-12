using Microsoft.JSInterop;

namespace RazorCameraLibrary
{
    /// <summary>
    /// Provides JavaScript interop functionalities for camera operations.
    /// </summary>

    public class CameraJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        /// <summary>
        /// Initializes a new instance of the CameraJsInterop class.
        /// </summary>
        /// <param name="jsRuntime">The JS runtime to use for invoking JavaScript functions.</param>
        public CameraJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/RazorCameraLibrary/cameraJsInterop.js").AsTask());
        }

        /// <summary>
        /// Releases unmanaged resources asynchronously.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        /// <summary>
        /// Loads and initializes the JavaScript module.
        /// </summary>
        public async Task LoadJS()
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<object>("init");
        }

        /// <summary>
        /// Creates a new CameraEnhancer instance.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result is a new CameraEnhancer instance.</returns>
        public async Task<CameraEnhancer> CreateCameraEnhancer()
        {
            var module = await moduleTask.Value;
            IJSObjectReference jsObjectReference = await module.InvokeAsync<IJSObjectReference>("createCameraEnhancer");
            CameraEnhancer cameraEnhancer = new CameraEnhancer(module, jsObjectReference);
            return cameraEnhancer;
        }
    }
}