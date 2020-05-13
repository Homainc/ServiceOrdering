import config from '../config';

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

            console.log(`fileuploadprogress data.loaded: ${e.loaded},
            data.total: ${e.total}`);
        });
    }
  
    xhr.onreadystatechange = function(e) {
        if (xhr.readyState === 4 && xhr.status === 200) {
            // File uploaded successfully
            var response = JSON.parse(xhr.responseText);
            // https://res.cloudinary.com/cloudName/image/upload/v1483481128/public_id.jpg
            var url = response.secure_url;
        
            // Create a thumbnail of the uploaded image, with 150px width
            var tokens = url.split('/');
            tokens.splice(-2, 0, 'w_150,c_scale');
            url = tokens.join('/');

            if(onSuccess)
                onSuccess(url);
        }
        else if(xhr.readyState === 4 && xhr.status === 400) {
            // Error was occured
            const message = JSON.parse(xhr.responseText).error.message as string;
            if(onFailure)
                onFailure(message);
        }
    };
  
    fd.append('upload_preset', config.cloudinary.uploadPreset);
    fd.append('tags', 'browser_upload'); // Optional - add tag for image admin in Cloudinary
    fd.append('file', file);
    xhr.send(fd);
  }

