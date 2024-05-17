import {UserRoleEnum} from "../enums/user-role-enum";

export interface RoleChangeRequest{
  email: string,
  newRole: UserRoleEnum
}
