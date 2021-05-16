import { Activity } from "../activities/activity.model"

export class ScheduledActivity {
  id?: number
  activity?: Activity
  startTime: Date
  endTime: Date
}

