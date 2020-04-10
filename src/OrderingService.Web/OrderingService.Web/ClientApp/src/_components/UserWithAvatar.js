import React from 'react';

export const UserWithAvatar = ({ user }) => {
    return(
        <span className="mt-n2">
            <img src={(user && user.imageUrl) || 'images/default-user.jpg'} className="rounded-circle mr-1 border border-dark" height="25" width="25" alt="profile avatar"></img>
            {(user && user.firstName) + ' ' + (user && user.lastName)}
        </span>
    );
};