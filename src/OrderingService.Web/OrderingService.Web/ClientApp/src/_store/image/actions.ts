import { ImageActionTypes, IMAGE_UPLOAD_REQUEST, IMAGE_UPLOAD_SUCCESS, IMAGE_UPLOAD_FAILURE, IMAGE_UPDATE_PROGRESS } from "./types";
import { ThunkAction } from 'redux-thunk';
import { RootState } from '../';
import { uploadFile } from '../../_helpers';
import { AlertActionTypes } from "../alert/types";
import * as alertActions from '../alert/actions'; 

const updateUploadProgress = (value: number): ImageActionTypes => 
    ({ type: IMAGE_UPDATE_PROGRESS,  value });

export function upload(
    file: File
): ThunkAction<void, RootState, undefined, ImageActionTypes | AlertActionTypes> {
    return async dispatch => {
        dispatch(request());

        uploadFile(file, 
            url => dispatch(success(url)), 
            progress => dispatch(updateUploadProgress(progress)), 
            (err) => {
                dispatch(failure(err))
                dispatch(alertActions.error(err));
            })
    };

    function request(): ImageActionTypes {
        return { type: IMAGE_UPLOAD_REQUEST };
    }
    function success(url: string): ImageActionTypes {
        return { type: IMAGE_UPLOAD_SUCCESS, imageUrl: url };
    }
    function failure(error: string): ImageActionTypes {
        return { type: IMAGE_UPLOAD_FAILURE, error };
    }
}