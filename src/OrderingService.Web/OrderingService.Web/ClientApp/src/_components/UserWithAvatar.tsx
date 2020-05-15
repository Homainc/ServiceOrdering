import React from 'react';
import { UserDto } from '../WebApiModels';

type UserWithAvatarProps = Readonly<{
    user: UserDto | undefined;
}>;

export const UserWithAvatar = ({ user }: UserWithAvatarProps) => {
    return(
        <span className="mt-n2">
            <img src={(user && user.imageUrl) || 'images/default-user.jpg'} className="rounded-circle mr-1 border border-dark" height="25" width="25" alt="profile avatar"></img>
            {(user && user.firstName) + ' ' + (user && user.lastName)}
        </span>
    );
};