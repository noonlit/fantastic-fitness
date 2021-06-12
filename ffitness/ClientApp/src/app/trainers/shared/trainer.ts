import { Activity } from "../../activities/shared/activity.model";

export namespace Trainer {

  export interface TrainerRequest {
    FirstName: string;
    LastName: string;
    Description: string;
    Activities: Number[];
    ProfilePicture?: File;
  };

  export interface TrainerResponse {
    id: number;
    firstName: string;
    lastName: string;
    description: string;
    activities: Activity[];
    profilePicture?: string;
  }

  export interface TrainerConfirmation {
    status: string;
  };
};
