import React from 'react';
import { UserDto } from '../WebApiModels';
import { cloudinary } from '../_helpers';

type UserWithAvatarProps = Readonly<{
    user: UserDto | undefined;
}>;

export const UserWithAvatar = ({ user }: UserWithAvatarProps) => {
    return(
        <span className="mt-n2">
            <img src={cloudinary.url(user?.imagePublicId || 'default-employee', { height: 25, width: 25, crop: 'scale' })} 
                className="rounded-circle mr-1 border border-dark" 
                height="25" width="25" 
                alt="profile avatar"></img>
            {(user && user.firstName) + ' ' + (user && user.lastName)}
        </span>
    );
};