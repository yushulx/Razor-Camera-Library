export function init() {
    return new Promise((resolve, reject) => {
        let script = document.createElement('script');
        script.type = 'text/javascript';
        script.src = '_content/RazorCameraLibrary/dce.js';
        script.onload = async () => {
            resolve();
        };
        script.onerror = () => {
            reject();
        };
        document.head.appendChild(script);
    });
}

export async function createCameraEnhancer() {
    if (!Dynamsoft) return;

    try {
        let cameraEnhancer = await Dynamsoft.DCE.CameraEnhancer.createInstance();
        return cameraEnhancer;
    }
    catch (ex) {
        console.error(ex);
    }
    return null;
}

export async function getCameras(cameraEnhancer) {
    if (!Dynamsoft) return;

    try {
        return await cameraEnhancer.getAllCameras();
    }
    catch (ex) {
        console.error(ex);
    }
}

export async function openCamera(cameraEnhancer, cameraInfo, dotNetHelper, callback) {
    if (!Dynamsoft) return;

    try {
        await cameraEnhancer.selectCamera(cameraInfo);
        cameraEnhancer.onPlayed = function () {
            let resolution = cameraEnhancer.getResolution();
            dotNetHelper.invokeMethodAsync(callback, resolution[0], resolution[1]);
        }
        await cameraEnhancer.open();
    }
    catch (ex) {
        console.error(ex);
    }
}

export async function closeCamera(cameraEnhancer) {
    if (!Dynamsoft) return;

    try {
        await cameraEnhancer.close();
    }
    catch (ex) {
        console.error(ex);
    }
}

export async function setVideoElement(cameraEnhancer, elementId) {
    if (!Dynamsoft) return;

    try {
        let element = document.getElementById(elementId);
        element.className = "dce-video-container";
        await cameraEnhancer.setUIElement(element);
    }
    catch (ex) {
        console.error(ex);
    }
}

export function acquireCameraFrame(cameraEnhancer) {
    if (!Dynamsoft) return;

    try {
        let img = cameraEnhancer.getFrame().toCanvas();
        return img;
    }
    catch (ex) {
        console.error(ex);
    }
}
