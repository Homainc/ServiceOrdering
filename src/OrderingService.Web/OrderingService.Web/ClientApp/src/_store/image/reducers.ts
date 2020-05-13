import { 
    ImageState, ImageActionTypes, 
    IMAGE_UPLOAD_REQUEST, IMAGE_UPLOAD_SUCCESS, IMAGE_UPLOAD_FAILURE, IMAGE_UPDATE_PROGRESS 
} from "./types";

const initialState: ImageState = {};

export function imageReducer(state: ImageState = initialState, action: ImageActionTypes): ImageState {
    switch(action.type){
        case IMAGE_UPLOAD_REQUEST:
            return { ...state, uploadingProgress: 0 }
        case IMAGE_UPLOAD_SUCCESS:
            return { ...state, uploadingProgress: undefined, url: action.imageUrl };
        case IMAGE_UPLOAD_FAILURE:
            return { ...state, uploadingProgress: undefined };

        case IMAGE_UPDATE_PROGRESS:
            return { ...state, uploadingProgress: action.value };
        
        default:
            return state;
    }
}