import { Activity } from "../../activities/shared/activity.model"

export class ScheduledActivity {
  id?: number
  description?: string
  activity?: Activity
  startTime: Date
  endTime: Date
  trainer?: any
}

