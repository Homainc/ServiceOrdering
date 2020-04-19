import { api } from "../_helpers";
import { EmployeeProfileDTO } from '../WebApiModels';

export const employeeService = {
    createEmployeeProfile,
    updateEmployeeProfile,
    deleteEmployeeProfile,
    loadEmployees,
    loadEmployeeProfile
};

async function createEmployeeProfile(profile: EmployeeProfileDTO): Promise<EmployeeProfileDTO> {
    const resp = await api.EmployeeProfile_Create({ employeeProfileDto: profile });
    return resp.body as EmployeeProfileDTO;
}

async function updateEmployeeProfile(profile: EmployeeProfileDTO): Promise<EmployeeProfileDTO> {
    const resp = await api.EmployeeProfile_Update({ id: profile.id, employeeProfileDto: profile });
    return resp.body as EmployeeProfileDTO;
}

async function deleteEmployeeProfile(id: string): Promise<EmployeeProfileDTO> {
    const resp = await api.EmployeeProfile_Delete({ id });
    return resp.body as EmployeeProfileDTO;
}

async function loadEmployees(pageNumber: number): Promise<any> {
    const resp = await api.EmployeeProfile_GetEmployees({ pageNumber });
    return resp.body;
}

async function loadEmployeeProfile(id: string): Promise<EmployeeProfileDTO> {
    const resp = await api.EmployeeProfile_GetEmployeeById({ id });
    return resp.body as EmployeeProfileDTO;
}