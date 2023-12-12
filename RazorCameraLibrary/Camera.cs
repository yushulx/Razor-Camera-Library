namespace RazorCameraLibrary
{
    /// <summary>
    /// Represents a camera device with its device ID and label.
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Gets or sets the unique device identifier for the camera.
        /// </summary>
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the label of the camera.
        /// </summary>
        public string Label { get; set; } = string.Empty;
    }
}
