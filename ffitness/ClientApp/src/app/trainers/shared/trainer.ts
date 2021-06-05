export namespace Trainer {

  export interface TrainerDetails {
    FirstName: string;
    LastName: string;
    Description: string;
    Activities: string;
    ProfilePicture?: File;
  };

  export interface TrainerDefault {
    id: number;
    firstName: string;
    lastName: string;
    description: string;
    activities: string;
    profilePicture?: string;
  }

  export interface TrainerConfirmation {
    status: string;
  };
};
