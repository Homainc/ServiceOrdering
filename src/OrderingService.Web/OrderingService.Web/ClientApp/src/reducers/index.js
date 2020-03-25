import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { alert } from './alert.reducer';
import { profile } from './profile.reducer';
import { employee } from './employee.reducer';

const rootReducer = combineReducers({
    authentication,
    alert,
    profile,
    employee
});

export default rootReducer;