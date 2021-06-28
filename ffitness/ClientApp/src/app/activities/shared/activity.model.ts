export class Activity {
  id?: number;
  name?: string;
  type?: string;
  activityPicture?: string;
  difficultyLevel?: number;
  primaryColour?: string;
  secondaryColour?: string;
  trainers?: any;
}

export enum ActivityType {
  Cardio,
  Aerobic,
  Strength,
  Yoga,
  Flexibility,
  Endurance,
  HIIT
}

export const ACTIVITY_TYPES = ['Cardio', 'Aerobic', 'Strength', 'Yoga', 'Flexibility', 'Endurance', 'HIIT'];
