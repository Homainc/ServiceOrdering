import config from '../config';
import { Cloudinary } from 'cloudinary-core';

const cl = new Cloudinary({ cloud_name: config.cloudinary.cloudName, secure: true });
export { cl as cloudinary };

export function uploadFile(
    file: File, 
    onSuccess?: (url: string) => void, 
    onProgressUpdated?: (progress: number) => void, 
    onFailure?: (err: string) => void
) {
    var xhr = new XMLHttpRequest();
    var fd = new FormData();
    xhr.open('POST', config.cloudinary.uploadUrl, true);
    xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
  
    // Reset the upload progress bar
    //document.getElementById('progress').style.width = 0;
    
    // Update progress (can be used to show progress indicator)
    if(onProgressUpdated){
        xhr.upload.addEventListener("progress", function(e) {
            var progress = Math.round((e.loaded * 100.0) / e.total);
            onProgressUpdated(progress);
        });
    }
  
    xhr.onreadystatechange = function(e) {
        if (xhr.readyState === 4 && xhr.status === 200) {
            // File uploaded successfully
            var response = JSON.parse(xhr.responseText);
            // https://res.cloudinary.com/cloudName/image/upload/v1483481128/public_id.jpg

            if(onSuccess)
                onSuccess(response.public_id);
        }
        else if(xhr.readyState === 4 && xhr.status === 400) {
            // Error was occured
            const message = JSON.parse(xhr.responseText).error.message as string;
            if(onFailure)
                onFailure(message);
        }
    };
  
    fd.append('tags', 'temporary');
    fd.append('upload_preset', config.cloudinary.uploadPreset);
    fd.append('tags', 'browser_upload'); // Optional - add tag for image admin in Cloudinary
    fd.append('file', file);
    xhr.send(fd);
  }

