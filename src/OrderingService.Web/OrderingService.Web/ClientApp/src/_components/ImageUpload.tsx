import React, { SyntheticEvent, useEffect } from 'react';
import { useField } from 'formik';
import { FormGroup, Label } from 'reactstrap';
import './ImageUpload.css';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import { ImageActionTypes } from '../_store/image/types';
import * as imageActions from '../_store/image/actions';
import { ConnectedProps, connect } from 'react-redux';
import { CircularProgressbar, buildStyles } from 'react-circular-progressbar';
import 'react-circular-progressbar/dist/styles.css';


const mapState =  (state: RootState) => ({
    uploadingProgress: state.image.uploadingProgress,
    imageUrl: state.image.url
});
  
const mapDispatch = (
    dispatch: ThunkDispatch<RootState, undefined, ImageActionTypes>
) => ({
    uploadImage: (file: File) => dispatch(imageActions.upload(file))
});
  
const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type ImageUploadProps = PropsFromRedux & Readonly<{ 
    id: string;
    name: string;
    disabled: boolean; 
}>;

const ImageUpload = ({ uploadingProgress, imageUrl, uploadImage, ...props }: ImageUploadProps) => {
    const [, meta, helpers] = useField(props);
    const { setValue } = helpers;

    const handleFileChange = ({ target }: SyntheticEvent) => {
        const input = target as HTMLInputElement;
        if(input.files)
            uploadImage(input.files[0]);
    }

    useEffect(() => {
        setValue(imageUrl);
    }, [imageUrl]);

    const isUploading = uploadingProgress !== undefined;
    return (
        <FormGroup>
            <Label htmlFor={props.id && props.name}>
                <div 
                    className='justify-content-center position-absolute text-light rounded' 
                    style={{ height: 150, width: 150, backgroundColor: '#00000070', display: isUploading? 'flex': 'none'}}>
                        <div className="align-self-center" style={{ height: 50, width: 50 }}>
                            <CircularProgressbar
                                strokeWidth={50}
                                styles={buildStyles({
                                    pathColor: '#DDD',
                                    strokeLinecap: 'butt',
                                    trailColor: 'rgba(0,0,0,0)'
                                })} 
                                value={uploadingProgress || 0}/>
                        </div>
                </div>
                <img 
                    src={meta.value || 'images/default-user.jpg'} 
                    height="150" width="150"
                    className={'rounded '+ (props.disabled? '': 'img-upload')} 
                    alt="Profile"/>
            </Label>
            <input className="collapse" type="file" {...props} accept="image/x-png, image/jpeg"
                onChange={handleFileChange}/>
        </FormGroup>
    );
};

const connectedImageUpload = connector(ImageUpload);
export { connectedImageUpload as ImageUpload };