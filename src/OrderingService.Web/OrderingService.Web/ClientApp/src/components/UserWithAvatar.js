import React from 'react';

export const UserWithAvatar = ({ user }) => {
    return(
        <>
            <img src={user.imageUrl} className="rounded-circle mr-1 border border-dark" height="25" width="25" alt="profile avatar"></img>
            {user.firstName + ' ' + user.lastName}
        </>
    );
};