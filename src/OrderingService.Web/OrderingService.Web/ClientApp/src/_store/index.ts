import { combineReducers, createStore, applyMiddleware } from 'redux';
import thunkMiddleware from 'redux-thunk';
import { createLogger } from 'redux-logger';
import { alertReducer } from "./alert/reducers";
import { authReducer } from './auth/reducers';
import { employeeReducer } from './employee/reducers';
import { orderReducer } from './order/reducers';
import { profileReducer } from './profile/reducers';
import { reviewReducer } from './review/reducers';
import { reviewModalReducer } from './reviewModal/reducers';
import { serviceTypeReducer } from './serviceType/reducers';
import { imageReducer } from './image/reducers';
import { jwt } from './_middleware/jwtMiddleware'

const rootReducer = combineReducers({
    alert: alertReducer,
    auth: authReducer,
    employee: employeeReducer,
    order: orderReducer,
    profile: profileReducer,
    review: reviewReducer,
    reviewModal: reviewModalReducer,
    serviceType: serviceTypeReducer,
    image: imageReducer,
});

export type RootState = ReturnType<typeof rootReducer>;

const loggerMiddleware = createLogger();

export default createStore(
    rootReducer,
    applyMiddleware(
        jwt,
        thunkMiddleware,
        loggerMiddleware
    )
);