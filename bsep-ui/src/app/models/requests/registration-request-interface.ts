import { PackageTypeEnum } from '../enums/package-type-enum';
import { UserTypeEnum } from '../enums/user-type-enum';

export interface RegistrationRequest {
  email: string;
  password: string;
  passwordAgain: string;
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
}
