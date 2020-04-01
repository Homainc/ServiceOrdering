import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { alert } from './alert.reducer';
import { profile } from './profile.reducer';
import { employee } from './employee.reducer';
import { order } from './order.reducer';
import { review } from './review.reducers';
import { reviewModal } from './reviewModal.reducers';

const rootReducer = combineReducers({
    authentication,
    alert,
    profile,
    employee,
    order,
    review,
    reviewModal
});

export default rootReducer;