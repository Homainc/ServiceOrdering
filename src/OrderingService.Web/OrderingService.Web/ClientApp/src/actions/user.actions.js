import { userConstants } from "../constants";
import { userService } from "../services";
import { alertActions } from "./";
import { history } from "../helpers";

export const userActions = {

};

function login(username, password){

}

function logout(){
    userService.logout();
    return { type: userConstants.LOGOUT };
}
