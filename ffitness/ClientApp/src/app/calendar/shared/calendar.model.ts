import { Activity } from "../../activities/shared/activity.model"

export class ScheduledActivity {
  id?: number
  activity?: Activity
  startTime: Date
  endTime: Date
}

