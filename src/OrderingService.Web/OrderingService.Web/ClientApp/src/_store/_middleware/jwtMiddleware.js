import { refreshToken } from '../auth/actions';

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
};

export function jwt({ dispatch, getState }) {

    return (next) => (action) => {

        // only worry about expiring token for async actions
        if (typeof action === 'function') {

            if (getState().auth && getState().auth.user) {
                console.log(getState().auth.user);
                const token = getState().auth.user.token; 
                // decode jwt so that we know if and when it expires
                var tokenExpiration = parseJwt(token.token).exp;

                console.log(tokenExpiration);
                console.log(new Date().getTime()/1000);

                if (tokenExpiration && tokenExpiration < (new Date().getTime()/1000)) {

                    // make sure we are not already refreshing the token
                    if (!getState().auth.refreshingPromise) {
                        return refreshToken(dispatch, token).then(() => next(action));
                    } else {
                        return getState().auth.refreshingPromise.then(() => next(action));
                    }
                }
            }
        }
        return next(action);
    };
}