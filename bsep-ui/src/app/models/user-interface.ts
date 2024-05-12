import { PackageTypeEnum } from './enums/package-type-enum';
import { UserRoleEnum } from './enums/user-role-enum';
import { UserTypeEnum } from './enums/user-type-enum';

export interface User {
  email: string;
  firstName?: string;
  lastName?: string;
  companyName?: string;
  companyPib?: string;
  address: string;
  city: string;
  country: string;
  phone: string;
  type: UserTypeEnum;
  package: PackageTypeEnum;
  role: UserRoleEnum;
}
