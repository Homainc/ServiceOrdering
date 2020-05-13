export const IMAGE_UPLOAD_REQUEST = 'image/upload (request)';
export const IMAGE_UPLOAD_SUCCESS = 'image/upload (success)';
export const IMAGE_UPLOAD_FAILURE = 'image/upload (failure)';

export const IMAGE_UPDATE_PROGRESS = 'image/update_progress';

interface ImageRequestAction {
    type: typeof IMAGE_UPLOAD_REQUEST;
};

interface ImageUploadSuccessAction {
    type: typeof IMAGE_UPLOAD_SUCCESS;
    imageUrl: string;
};

interface ImageUpdateProgressAction {
    type: typeof IMAGE_UPDATE_PROGRESS;
    value: number;
};

interface ImageFailureAction {
    type: typeof IMAGE_UPLOAD_FAILURE;
    error: string;
}

export type ImageActionTypes = 
    ImageRequestAction | 
    ImageUploadSuccessAction |
    ImageUpdateProgressAction | 
    ImageFailureAction;

export interface ImageState {
    uploadingProgress?: number;
    url?: string;
};