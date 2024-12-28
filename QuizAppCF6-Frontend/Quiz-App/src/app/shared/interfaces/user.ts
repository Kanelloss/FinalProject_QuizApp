export interface User {
  id?: number;
  username: string;
  email: string;
  password: string; 
  UserRole: 'Admin' | 'User'; // Admin ή User
}

export interface Credentials {
  username: string;
  password: string;
}

export interface LoggedInUser {
  id: number;
  username: string;
  email: string;
  role: string;
}


