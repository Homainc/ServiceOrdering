import { api } from '../_helpers';
import { UserDTO, EmployeeProfileDTO } from '../WebApiModels';

export const userService = {
    login,
    logout,
    signUp,
    updateAuthUser,
    updateAuthEmployeeProfile
};

async function login(email: string, password: string): Promise<UserDTO> {
    const resp = await api.Account_Auth({ userDto: { email, password }});
    const user = resp.body as UserDTO;
    localStorage.setItem('user', JSON.stringify(user));
    api.setRequestHeadersHandler(h => ({ ...h, 'Authorization': 'Bearer ' + user.token }));
    return user;
}

function logout(): void {
    localStorage.removeItem('user');
    api.setRequestHeadersHandler(h => h);
}

function updateAuthUser(user : UserDTO): void {
    let oldUser = JSON.parse(localStorage.getItem('user') as string);
    user.token = oldUser.token;
    localStorage.setItem('user', JSON.stringify(user));
}

function updateAuthEmployeeProfile(employeeProfile : EmployeeProfileDTO | undefined): void {
    let user = JSON.parse(localStorage.getItem('user') as string);
    user.employeeProfile = employeeProfile;
    localStorage.setItem('user', JSON.stringify(user));
}

async function signUp(user : UserDTO): Promise<UserDTO> {
    const resp = await api.Account_SignUp({ userDto: user });
    user = resp.body as UserDTO;
    localStorage.setItem('user', JSON.stringify(user));
    api.setRequestHeadersHandler(h => ({ ...h, 'Authorization': 'Bearer ' + user.token }));
    return user;
}