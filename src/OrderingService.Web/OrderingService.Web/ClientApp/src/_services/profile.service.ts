import { api } from "../_helpers";
import { UserDTO } from "../WebApiModels";

export const profileService = {
    loadProfile,
    updateProfile,
};

async function loadProfile(): Promise<UserDTO> {
    const resp = await api.Account_GetProfile({});
    return resp.body as UserDTO;
}

async function updateProfile(profile: UserDTO): Promise<UserDTO> {
    const resp = await api.Account_UpdateProfile({ id: profile.id as string, userDto: profile });
    return resp.body as UserDTO;
}