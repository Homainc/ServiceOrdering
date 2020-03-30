import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { alert } from './alert.reducer';
import { profile } from './profile.reducer';
import { employee } from './employee.reducer';
import { order } from './order.reducer';

const rootReducer = combineReducers({
    authentication,
    alert,
    profile,
    employee,
    order
});

export default rootReducer;