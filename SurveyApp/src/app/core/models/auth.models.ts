export interface LoginRequest {
    email: string;
    password: string;
  }
  
  export interface RegisterRequest {
    email: string;
    password: string;
  }
  
  export interface AuthResponse {
    isFailure: boolean;
    error: string;
    isSuccess: boolean;
    value:UserData
  }
  export interface UserData {
    userId: string;
    email: string;
    token: string;
  }
  
